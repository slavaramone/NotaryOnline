using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Ответ авторизации пользователя
	/// </summary>
	public class LoginResponse
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
    }
}
