using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Ответ запроса пользователей
	/// </summary>
	public class GetUserResponse
	{
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Эл. почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Активен да/нет
        /// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Профайл пользователя
		/// </summary>
		public UserProfile Profile { get; set; }

        public class UserProfile
		{
            /// <summary>
            /// Id
            /// </summary>
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
            public string City { get; set; }

            /// <summary>
            /// Адрес
            /// </summary>
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
        }
    }
}
