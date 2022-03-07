using NotaryOnline.Api.ServiceModel;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;

namespace NotaryOnline.Entities
{
	[Alias("Orders")]
	public class Order : IEntity<Guid>
	{
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		[Default(OrmLiteVariables.SystemUtc)]
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Срок выполнения
		/// </summary>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Дата закрытия
		/// </summary>
		public DateTime? CompletedDate { get; set; }

		/// <summary>
		/// Статус заказа
		/// </summary>
		public OrderStatus Status { get; set; }

		/// <summary>
		/// Id пользователя создавшего заказ
		/// </summary>
		[ForeignKey(typeof(User))]
		public Guid? UserId { get; set; }

		/// <summary>
		/// Пользователь
		/// </summary
		[Reference]
		public User User { get; set; }

		/// <summary>
		/// Документы заказа
		/// </summary
		[Reference]
		public List<OrderDocument> OrderDocuments { get; set; }
	}
}
