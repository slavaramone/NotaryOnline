using Microsoft.AspNetCore.Mvc.Rendering;
using NotaryOnline.Api.ServiceModel;
using System;
using System.Collections.Generic;

namespace NotaryOnline.Web.Models
{
	public class OrderModel
	{
		public Guid Id { get; set; }

		public string CreatedDate { get; set; }

		public string DueDate { get; set; }

		public string CompletedDate { get; set; }

		public OrderStatus Status { get; set; }

		public string City { get; set; }

		public string CreatorName { get; set; }

		public string CreatorPhone { get; set; }

		public List<OrderDocumentModel> Documents { get; set; }

		public SelectList SelectListStatus { get; set; }

		public class OrderDocumentModel
		{
			public string DocumentId { get; set; }

			public string Name { get; set; }
		}
	}
}
