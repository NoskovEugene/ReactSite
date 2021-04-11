using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Common.Extensions.StringExtensions
{
    public static class StringExtension
    {
        public static string GetMD5Hash(this string input)
        {
            var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(input);
            return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", string.Empty);
        }
    }
}