using ServiceStack;
using System;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос создания заказа
	/// </summary>
	[Route("/order")]
	public class CreateOrderRequest : IReturn<CreateOrderResponse>
	{
		public Guid? UserId { get; set; }
	}
}
