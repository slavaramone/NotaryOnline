using ServiceStack;
using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос загрузки документа
	/// </summary>
	[Route("/document/upload")]
	public class UploadDocumentRequest : IReturn<UploadDocumentResponse>
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Id заказа
		/// </summary>
		public Guid OrderId { get; set; }

		/// <summary>
		/// Описание документа
		/// </summary>
		public string DocumentName { get; set; }

		/// <summary>
		/// Описание документа
		/// </summary>
		public string DocumentDescription { get; set; }
	}
}
