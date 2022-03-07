using ServiceStack;
using System;
using System.Collections.Generic;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос получения заказов пользователя
	/// </summary>
	[Route("/user/{Id}/orders")]
	public class GetUserOrdersRequest : IReturn<List<GetOrderResponse>>
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid Id { get; set; }
	}
}
