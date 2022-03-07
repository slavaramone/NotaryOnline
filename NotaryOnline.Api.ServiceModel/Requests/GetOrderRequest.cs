using ServiceStack;
using System;
using System.Collections.Generic;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос получения заказа
	/// </summary>
	[Route("/order")]
	public class GetOrderRequest : IReturn<List<GetOrderResponse>>
	{
		/// <summary>
		/// Id заказа
		/// </summary>
		public Guid? Id { get; set; }


		public OrderStatus? Status { get; set; }
	}
}
