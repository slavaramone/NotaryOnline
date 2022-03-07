using NotaryOnline.Api.ServiceModel;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using System;

namespace NotaryOnline.Entities
{
	[Alias("OrderDocuments")]
	public class OrderDocument : IEntity<int>
	{
		/// <summary>
		/// Id
		/// </summary>
		[AutoIncrement]
		public int Id { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		[Default(OrmLiteVariables.SystemUtc)]
		public DateTime UploadDate { get; set; }

		/// <summary>
		/// Id заказа
		/// </summary>
		[ForeignKey(typeof(Order))]
		public Guid OrderId { get; set; }

		/// <summary>
		/// Заказ
		/// </summary>
		[Reference]
		public Order Order { get; set; }

		/// <summary>
		/// Id пользователя загрузившего документ
		/// </summary>
		[ForeignKey(typeof(User))]
		public Guid? UserId { get; set; }

		/// <summary>
		/// Пользователь загрузивший документ
		/// </summary
		[Reference]
		public User User { get; set; }

		/// <summary>
		/// Id документа
		/// </summary>
		public string DocumentId { get; set; }

		/// <summary>
		/// Тип документа
		/// </summary>
		public DocumentType DocumentType { get; set; }

		/// <summary>
		/// Название документа
		/// </summary>
		public string DocumentName { get; set; }

		/// <summary>
		/// Описание документа
		/// </summary>
		public string DocumentDescription { get; set; }
	}
}
