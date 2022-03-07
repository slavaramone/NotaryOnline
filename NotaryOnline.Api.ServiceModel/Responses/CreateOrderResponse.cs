using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Ответ запроса создания заказа
	/// </summary>
	public class CreateOrderResponse
	{
		/// <summary>
		/// Id созданного заказа
		/// </summary>
		public Guid Id { get; set; }
	}
}
