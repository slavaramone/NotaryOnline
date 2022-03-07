using ServiceStack;
using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос создания заказа
	/// </summary>
	[Route("/order/document/add")]
	public class AddDocumentToOrderRequest
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid? UserId { get; set; }

		/// <summary>
		/// Id заказа
		/// </summary>
		public Guid OrderId { get; set; }

		/// <summary>
		/// Id документа
		/// </summary>
		public Guid DocumentId { get; set; }

		/// <summary>
		/// Имя документа
		/// </summary>
		public Guid DocumentName { get; set; }

		/// <summary>
		/// Описание документа
		/// </summary>
		public Guid DocumentDescription { get; set; }
	}
}
