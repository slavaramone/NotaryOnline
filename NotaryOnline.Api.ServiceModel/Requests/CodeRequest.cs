using ServiceStack;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос кода для получения токена
	/// </summary>
	[Route("/auth/code")]
	public class CodeRequest : IReturn<CodeResponse>
	{
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Универсальный идентификатор мобилбного устройства
		/// </summary>
		public string DeviceUid { get; set; }
    }
}
