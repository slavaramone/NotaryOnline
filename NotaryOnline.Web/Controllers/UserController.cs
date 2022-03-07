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
	public class UserController : Controller
	{
		private readonly IJsonServiceClient _client;
		private readonly IMapper _mapper;

		public UserController(IJsonServiceClient client, IMapper mapper)
		{
			_client = client;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(Guid userId)
		{
			List<GetUserResponse> users;
			var userResult = await _client.ApiAsync(new GetUserRequest { Ids = new List<Guid> { userId } });
			if (userId == Guid.Empty)
			{
				users = new List<GetUserResponse>() { new GetUserResponse() };
			}
			else
			{				
				users = userResult.Response;
			}

			var roles = SharedLib.Extensions.EnumExtensions.GetDescriptions(typeof(UserRole));
			var selectList = new SelectList(roles);
			int index = 1;
			foreach (var selectListItem in selectList)
			{
				selectListItem.Value = index.ToString();
				if (selectListItem.Text.Equals(users[0].Role.GetDescription()))
				{
					selectListItem.Selected = true;
				}
				index++;
			}

			if (users is not null && users.Any())
			{
				var model = _mapper.Map<UserModel>(users[0]);
				if (userId == Guid.Empty)
				{
					model.CreatedDate = DateTime.Now;
				}
				TempData["userId"] = model.Id;
				model.SelectListStatus = selectList;
				return View(model);
			}
			else if (userResult.Error is not null)
			{
				throw new ArgumentException(userResult.Error?.Message);
			}
			throw new ArgumentException("Ошибка получения данных пользователя");
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserModel userModel)
		{
			var req = _mapper.Map<UpdateUserRequest>(userModel);
			if (req.CreatedDate == DateTime.MinValue)
			{
				req.CreatedDate = DateTime.Now;
			}

			await _client.ApiAsync(req);
			return RedirectToAction("Users", "Admin");
		}
	}
}
