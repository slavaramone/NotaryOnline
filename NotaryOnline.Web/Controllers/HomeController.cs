using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Web.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotaryOnline.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IJsonServiceClient _client;

		public HomeController(IJsonServiceClient client)
		{
			_client = client;
		}

		public IActionResult Index()
		{
			if (HttpContext.User is not null)
			{
				return RedirectToAction("Orders", "Admin");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var result = await _client.ApiAsync(new LoginRequest { Email = model.Email, Password = model.Password });
			if (result.Succeeded)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, result.Response.Id.ToString()),
					new Claim(ClaimTypes.Email, result.Response.Email),
					new Claim(ClaimTypes.Role, result.Response.Role.ToString()),
				};
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var authProperties = new AuthenticationProperties
				{
					IsPersistent = true
				};
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

				return RedirectToAction("Orders", "Admin");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}			
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
