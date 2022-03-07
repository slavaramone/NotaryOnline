using ServiceStack;
using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос токена по коду
	/// </summary>
	[Route("/auth/token")]
	public class TokenRequest : IReturn<TokenResponse>
	{
		/// <summary>
		/// Id сессии
		/// </summary>
		public Guid SessionId { get; set; }

		/// <summary>
		/// Код подтверждения
		/// </summary>
		public string Code { get; set; }
	}
}
