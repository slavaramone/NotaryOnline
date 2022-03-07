using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Web.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLib.Extensions;

namespace NotaryOnline.Web.Controllers
{
	[Authorize(Roles = "Admin,Customer")]
	public class OrderController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IJsonServiceClient _client;

		public OrderController(IJsonServiceClient client, IMapper mapper)
		{
			_mapper = mapper;
			_client = client;
		}

		public async Task<IActionResult> Index(Guid orderId)
		{
			var orderResult = await _client.ApiAsync(new GetOrderRequest { Id = orderId });

			var descs = SharedLib.Extensions.EnumExtensions.GetDescriptions(typeof(OrderStatus));
			var selectList = new SelectList(descs);
			foreach (var selectListItem in selectList)
			{
				if (selectListItem.Text.Equals(orderResult.Response[0].Status.GetDescription()))
				{
					selectListItem.Selected = true;
				}
			}

			if (orderResult.Response is not null && orderResult.Response.Any())
			{
				var model = _mapper.Map<OrderModel>(orderResult.Response[0]);

				if (orderResult.Response[0].UserId.HasValue)
				{
					var user = await _client.ApiAsync(new GetUserRequest { Ids = new List<Guid> { orderResult.Response[0].UserId.Value } });
					model.CreatorName = user.Response[0].Name;
					model.CreatorPhone = user.Response[0].Phone;
				}
			
				TempData["orderId"] = model.Id;
				model.SelectListStatus = selectList;
				return View(model);
			}
			else if (orderResult.Error is not null)
			{
				throw new ArgumentException(orderResult.Error?.Message);
			}
			throw new ArgumentException("Ошибка получения данных заказа");
		}

		public async Task<IActionResult> Create()
		{
			var result = await _client.ApiAsync(new CreateOrderRequest
			{
				UserId = new Guid(HttpContext.User.Identity.Name)
			});

			return RedirectToAction("Index", "Order", new { OrderId = result.Response.Id });
		}

		[HttpPost]
		public IActionResult Upload(IFormFile files)
		{
			if (Request.Form.Files.Any() && Request.Form.Files[0].Length > 0)
			{
				var doc = Request.Form.Files[0];
				Guid orderId = (Guid)TempData["orderId"];
				var request = new UploadDocumentRequest
				{
					DocumentName = doc.FileName,
					OrderId = orderId,
					UserId = new Guid(HttpContext.User.Identity.Name)
				};
				var docFiles = new List<UploadFile> { new UploadFile(doc.FileName, doc.OpenReadStream()) };
				var uploadResult = _client.PostFilesWithRequest<UploadDocumentResponse>(request, docFiles);

				if (uploadResult is null)
				{
					throw new ArgumentException("Ошибка загрузки документа");
				}
				return RedirectToAction("Index", "Order", new { orderId });
			}
			throw new ArgumentException("Нет файлов для загрузки");
		}

		public async Task<IActionResult> File([FromQuery] string documentId)
		{
			var resp = await _client.ApiAsync(new GetDocumentRequest { DocumentId = documentId });

			return File(resp.Response.Content, "application/octet-stream", resp.Response.DocumentName);
		}

		[HttpPost]
		public async Task Status([FromForm] Guid orderId, [FromForm] string value)
		{
			var status = SharedLib.Extensions.EnumExtensions.GetValueFromDescription<OrderStatus>(value);
			await _client.ApiAsync(new UpdateOrderRequest 
			{
				OrderId = orderId,
				Status = status
			});
		}
	}
}
