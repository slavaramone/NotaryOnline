using AutoMapper;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.DataAccess.Filters;
using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack;
using SharedLib.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaryOnline.Api.ServiceInterface
{
	/// <summary>
	/// API сервис пользователей
	/// </summary>
	public class UserService : Service
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepo;
		private readonly IOrderRepository _orderRepo;
		private readonly IOrderDocumentRepository _orderDocumentRepo;
		private readonly IUserProfileRepository _userProfileRepo;

		public UserService(IMapper mapper, IUserRepository userRepo, IOrderRepository orderRepo, IOrderDocumentRepository orderDocumentRepo, IUserProfileRepository userProfileRepo)
		{
			_mapper = mapper ?? throw new ArgumentNullException();
			_userRepo = userRepo ?? throw new ArgumentNullException();
			_orderRepo = orderRepo ?? throw new ArgumentNullException();
			_orderDocumentRepo = orderDocumentRepo ?? throw new ArgumentNullException();
			_userProfileRepo = userProfileRepo ?? throw new ArgumentNullException();
		}

		/// <summary>
		/// Получение пользователя
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Get(GetUserRequest request)
		{
			if (request.Ids is null || !request.Ids.Any())
			{
				return await _userRepo.GetAll();
			}
			else
			{
				var users = await _userRepo.Get(request.Ids);
				var userIds = users.Select(x => x.Id).ToList();
				var userProfiles = await _userProfileRepo.Get(userIds);

				var response = _mapper.Map<List<GetUserResponse>>(users);
				foreach (var item in response)
				{
					var userProfile = userProfiles.Find(x => x.UserId == item.Id);
					if (userProfile is not null)
					{
						item.Profile = _mapper.Map<GetUserResponse.UserProfile>(userProfile);
					}
				}
				return response;
			}
		}

		/// <summary>
		/// Получение заказов пользователя
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Get(GetUserOrdersRequest request)
		{
			var orders = await _orderRepo.GetUserOrders(request.Id);

			var response = new List<GetOrderResponse>();
			foreach (var order in orders)
			{
				var orderDocuments = await _orderDocumentRepo.Get(order.Id);

				var responseItem = _mapper.Map<GetOrderResponse>(order);
				responseItem.Documents = _mapper.Map<List<GetOrderResponse.OrderDocument>>(orderDocuments);
				response.Add(responseItem);
			}
			return response;
		}

		public async Task Put(UpdateUserRequest request)
		{
			var user = _mapper.Map<User>(request);
			if (request.Id == Guid.Empty)
			{
				user.PasswordSalt = Convert.ToBase64String(Crypto.GenerateSalt());
				user.PasswordHash = Crypto.HashPassword(request.Password, user.PasswordSalt);
				await _userRepo.Add(user);
			}
			else
			{
				var existingUser = await _userRepo.Get(request.Id);

				user.Id = request.Id;
				user.PasswordHash = existingUser.PasswordHash;
				user.PasswordSalt = existingUser.PasswordSalt;
				user.DeviceUid = existingUser.DeviceUid;

				user.UpdatedDate = DateTime.UtcNow;

				await _userRepo.Update(user);
			}

			if (request.Profile is not null)
			{
				var userProfile = _mapper.Map<UserProfile>(request.Profile);
				if (userProfile.Id == default(int))
				{
					await _userProfileRepo.Add(userProfile);
				}
				else
				{
					await _userProfileRepo.Update(userProfile);
				}
			}			
		}
	}
}
