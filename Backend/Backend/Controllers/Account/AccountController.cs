using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Backend.Common.Configurations.AppConfiguration;
using Backend.Common.Dtos;
using Backend.Common.Extensions.ConfigurationExtensions;
using Backend.Common.Extensions.StringExtensions;
using Backend.DAL.Models;
using Backend.DAL.Repositories;
using Backend.ViewModels.Account;
using Backend.ViewModels.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers.Account
{
    [Route("/api/v1/account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IApplicationConfiguration Configuration { get; set; }

        private IRepositoryBase<User> UserRepository { get; set; }

        private IMapper Mapper { get; set; }

        public AccountController(IRepositoryBase<User> userRepository,
                                 IMapper mapper,
                                 IApplicationConfiguration configuration)
        {
            UserRepository = userRepository;
            Mapper = mapper;
            Configuration = configuration;
        }


        [ActionName("authenticate")]
        [HttpPost]
        public SuccessResponse<UserInfoDto> Authenticate([FromBody] AuthenticateInfo info)
        {
            info.Password = info.Password.GetMD5Hash().ToLower();
            var user = UserRepository.Query(x => x.Login == info.Login && x.PasswordHash == info.Password)
                                     .FirstOrDefault();
            if (user != null)
            {
                var dto = Mapper.Map<UserDto>(user);
                var token = CreateToken(dto);
                var output = new JwtSecurityTokenHandler().WriteToken(token);
                var cookieOptions = GetCookieOptions();
                HttpContext.Response.Cookies.Append(Configuration.CookieConfiguration.CookieName, output,
                                                    cookieOptions);
                return new SuccessResponse<UserInfoDto>()
                {
                    Success = true,
                    Payload = Mapper.Map<UserInfoDto>(user)
                };
            }
            else
            {
                return new SuccessResponse<UserInfoDto>()
                {
                    Success = false
                };
            }
        }

        [HttpGet]
        [ActionName("checkAuth")]
        public SuccessResponse<UserInfoDto> CheckAuthentication()
        {
            if (HttpContext.Request.Cookies.TryGetValue(Configuration.CookieConfiguration.CookieName, out var cookie))
            {
                if (string.IsNullOrEmpty(cookie))
                {
                    return new SuccessResponse<UserInfoDto>()
                    {
                        Success = false
                    };
                }

                cookie = cookie.Replace("Bearer ", string.Empty);
                var token = new JwtSecurityTokenHandler().ReadJwtToken(cookie);
                var login = token.Claims.First(x => x.Type == "login").Value;
                var user = UserRepository.Query(x => x.Login == login).First();
                return new SuccessResponse<UserInfoDto>()
                {
                    Success = true,
                    Payload = Mapper.Map<UserInfoDto>(user)
                };
            }

            return new SuccessResponse<UserInfoDto>()
            {
                Success = false
            };
        }

        [HttpPost]
        [ActionName("logout")]
        [Authorize]
        public SuccessResponse<string> Logout()
        {
            HttpContext.Response.Cookies.Delete(Configuration.CookieConfiguration.CookieName, new CookieOptions()
            {
                Secure = true,
                SameSite = SameSiteMode.None
            });
            return new SuccessResponse<string>()
            {
                Success = true,
                Payload = "logout complete"
            };
        }

        private CookieOptions GetCookieOptions()
        {
            var cookieConfig = Configuration.CookieConfiguration;
            return new CookieOptions()
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(cookieConfig.MaxAgeDays),
                SameSite = SameSiteMode.None,
                Secure = cookieConfig.Secure
            };
        }

        private List<Claim> GetClaims(UserDto user)
        {
            return new()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("login", user.Login),
                new Claim(JwtRegisteredClaimNames.Gender, user.Gender)
            };
        }
        
        private JwtSecurityToken CreateToken(UserDto user)
        {
            var jwtConfig = Configuration.JwtConfiguration;
            var claims = GetClaims(user);
            var now = DateTime.Now;
            var identity = new ClaimsIdentity(claims);
            return new JwtSecurityToken(jwtConfig.Issuer,
                                        jwtConfig.Audience,
                                        claims,
                                        now,
                                        now.AddDays(jwtConfig.LifeTimeDays),
                                        new SigningCredentials(jwtConfig.GetSymmetricSecurityKey(),
                                                               SecurityAlgorithms.HmacSha256));
        }
    }
}