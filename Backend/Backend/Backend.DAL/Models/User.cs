using Microsoft.EntityFrameworkCore;

namespace Backend.DAL.Models
{
    [Index(nameof(User.Login), IsUnique = true)]
    public class User : EntityBase
    {
        public string Email { get; set; }

        public string Login { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///     Пол
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///     Возраст
        /// </summary>
        public int Age { get; set; }
    }
}