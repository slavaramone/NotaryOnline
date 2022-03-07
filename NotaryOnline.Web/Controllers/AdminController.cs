using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Web.Models;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotaryOnline.Web.Controllers
{
	[Authorize(Roles = "Admin,Translator,NotaryAssistant")]
	public class AdminController : Controller
	{
		private readonly IJsonServiceClient _client;
		private readonly IMapper _mapper;

		public AdminController(IJsonServiceClient client, IMapper mapper)
		{
			_client = client;
			_mapper = mapper;
		}

		public async Task<IActionResult> Orders(OrderStatus? status)
		{
			status = status ?? OrderStatus.New;

			ViewData["UserName"] = GetUserName();
			ViewData["Status"] = status;
			ViewData["Page"] = "Orders";

			var orders = await _client.ApiAsync(new GetOrderRequest { Status = status });
			var userIds = orders.Response.Where(x => x.UserId.HasValue).Select(x => x.UserId.Value).ToList();
			var users = await _client.ApiAsync(new GetUserRequest { Ids = userIds });

			var models = _mapper.Map<List<OrderModel>>(orders.Response);
			foreach (var model in models)
			{
				var order = orders.Response.Find(x => x.Id == model.Id);
				var user = users.Response.Find(x => x.Id == order.UserId);
				if (user.Profile is not null)
				{
					model.City = user.Profile.City ?? "не указан";
				}
				else
				{
					model.City = "не указан";
				}
				model.CreatorName = user.Name;
			}

			return View(models);
		}

		public async Task<IActionResult> Users()
		{
			ViewData["UserName"] = GetUserName();
			ViewData["Page"] = "Users";

			var users = await _client.ApiAsync(new GetUserRequest());

			var model = _mapper.Map<List<UserModel>>(users.Response);

			return View(model);
		}

		private string GetUserName()
		{
			var emailClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
			return emailClaim is null ? string.Empty : emailClaim.Value;
		}
	}
}
