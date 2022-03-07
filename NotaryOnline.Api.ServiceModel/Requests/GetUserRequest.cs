using ServiceStack;
using System;
using System.Collections.Generic;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос получения пользователей
	/// </summary>
	[Route("/user", "GET")]
	public class GetUserRequest : IReturn<List<GetUserResponse>>
	{
		/// <summary>
		/// Id пользователей
		/// </summary>
		public List<Guid> Ids { get; set; }
	}
}
