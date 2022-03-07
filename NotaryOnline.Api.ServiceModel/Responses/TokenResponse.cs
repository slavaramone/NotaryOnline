using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Ответ авторизации пользователя
	/// </summary>
	public class TokenResponse
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Роль пользовтеля
		/// </summary>
		public UserRole UserRole { get; set; }

		/// <summary>
		/// Bearer token
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Время когда токен станет недействительным
		/// </summary>
		public DateTime ExpirationDateTime { get; set; }
	}
}
