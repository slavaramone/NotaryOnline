using ServiceStack;
using System;

namespace NotaryOnline.Api.ServiceModel
{
	[Route("/order", "PUT")]
	public class UpdateOrderRequest : IReturnVoid
	{
		public Guid OrderId { get; set; }

		public OrderStatus Status { get; set; }
	}
}
