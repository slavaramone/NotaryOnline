using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Ответ запрос кода для получения токена
	/// </summary>
	/// </summary>
	public class CodeResponse
	{
		/// <summary>
		/// Id сессии
		/// </summary>
		public Guid SessionId { get; set; }
	}
}
