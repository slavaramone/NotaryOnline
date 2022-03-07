using NotaryOnline.Api.ServiceModel;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using System;

namespace NotaryOnline.Entities
{
	[Alias("Users")]
    public class User : IEntity<Guid>
    {
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Эл. почта
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Активен/неактивен
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Соль пароля
        /// </summary>
        [Required]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Универсальный идентификатор устройства
        /// </summary>
        [StringLength(255)]
        public string DeviceUid { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Default(OrmLiteVariables.SystemUtc)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата обновления данных
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
