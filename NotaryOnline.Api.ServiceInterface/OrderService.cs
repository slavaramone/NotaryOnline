using AutoMapper;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.Api.ServiceInterface
{
	/// <summary>
	/// API сервис заказов
	/// </summary>
	public class OrderService : Service
	{		
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepo;
		private readonly IOrderDocumentRepository _orderDocumentRepo;

		public OrderService(IMapper mapper, IOrderRepository orderRepo, IOrderDocumentRepository orderDocumentRepo)
		{
			_mapper = mapper ?? throw new ArgumentNullException();
			_orderRepo = orderRepo ?? throw new ArgumentNullException();
			_orderDocumentRepo = orderDocumentRepo ?? throw new ArgumentNullException();
		}

		/// <summary>
		/// Создание заказа
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Post(CreateOrderRequest request)
		{
			var order = _mapper.Map<Order>(request);			
			await _orderRepo.Add(order);

			var response = _mapper.Map<CreateOrderResponse>(order);
			return response;
		}

		/// <summary>
		/// Привязка документа к заказу
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task Post(AddDocumentToOrderRequest request)
		{
			var orderDocument = _mapper.Map<OrderDocument>(request);
			await _orderDocumentRepo.Add(orderDocument);
		}

		/// <summary>
		/// Получение заказа
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Any(GetOrderRequest request)
		{
			if (request.Id.HasValue)
			{
				var order = await _orderRepo.Get(request.Id.Value);
				var orderDocuments = await _orderDocumentRepo.Get(request.Id.Value);

				var response = _mapper.Map<GetOrderResponse>(order);
				response.Documents = _mapper.Map<List<GetOrderResponse.OrderDocument>>(orderDocuments);

				return new List<GetOrderResponse> { response };
			}
			else
			{
				var response = new List<GetOrderResponse>();
				List<Order> orders;
				if (request.Status.HasValue)
				{
					orders = await _orderRepo.Get(request.Status.Value);
				}
				else
				{
					orders = await _orderRepo.GetAll();
				}
								
				foreach (var order in orders)
				{
					var orderDocuments = await _orderDocumentRepo.Get(order.Id);

					var responseItem = _mapper.Map<GetOrderResponse>(order);
					responseItem.Documents = _mapper.Map<List<GetOrderResponse.OrderDocument>>(orderDocuments);
					response.Add(responseItem);
				}
				return response;
			}
		}

		public async Task Put(UpdateOrderRequest req)
		{
			var order = await _orderRepo.Get(req.OrderId);
			order.Status = req.Status;
			await _orderRepo.Update(order);
		}
	}
}
