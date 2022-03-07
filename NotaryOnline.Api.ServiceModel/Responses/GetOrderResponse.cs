using System;
using System.Collections.Generic;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Ответ запроса заказа
	/// </summary>
	public class GetOrderResponse
	{
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Срок выполнения
		/// </summary>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Дата выезда
		/// </summary>
		public DateTime? CompletedDate { get; set; }

		/// <summary>
		/// Статус заказа
		/// </summary>
		public OrderStatus Status { get; set; }

		/// <summary>
		/// Id пользователя создавшего заказ
		/// </summary>
		public Guid? UserId { get; set; }

		/// <summary>
		/// Документы заказа
		/// </summary
		public List<OrderDocument> Documents { get; set; }

		/// <summary>
		/// Ответ запроса списка документов заказа
		/// </summary>
		public class OrderDocument
		{
			/// <summary>
			/// Id документа
			/// </summary>
			public string DocumentId { get; set; }

			/// <summary>
			/// Дата создания документа
			/// </summary>
			public DateTime CreatedDate { get; set; }

			/// <summary>
			/// Название файла
			/// </summary>
			public string Name { get; set; }
		}
	}
}
