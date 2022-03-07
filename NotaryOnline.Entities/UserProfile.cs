using ServiceStack.DataAnnotations;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;

namespace NotaryOnline.Entities
{
	[Alias("UserProfiles")]
	public class UserProfile : IEntity<int>
	{
        /// <summary>
        /// Id
        /// </summary>
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [StringLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [StringLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// Номер документа удост. личн.
        /// </summary>
        public string DocNumber { get; set; }

        /// <summary>
        /// Кем выдан ДУЛ
        /// </summary>
        public string DocIssueDepartment { get; set; }

        /// <summary>
        /// Код ДУЛ
        /// </summary>
        public string DocIssueCode { get; set; }

        /// <summary>
        /// Страна выдавшая ДУЛ
        /// </summary>
        public string DocIssueCountry { get; set; }

        /// <summary>
        /// Дата выдачи ДУЛ
        /// </summary>
        public DateTime? DocIssueDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DocValidTo { get; set; }

        /// <summary>
        /// Id профайла
        /// </summary>
        [ForeignKey(typeof(User))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Профайл
        /// </summary>
        [Reference]
        public User User { get; set; }
    }
}
