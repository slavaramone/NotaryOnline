using AutoMapper;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Entities;

namespace NotaryOnline.Api
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, LoginResponse>();

			CreateMap<CreateOrderRequest, Order>()
				.ForMember(d => d.Id, e => e.MapFrom(s => Guid.NewGuid()))
				.ForMember(d => d.Status, e => e.MapFrom(s => OrderStatus.New));

			CreateMap<Order, CreateOrderResponse>();

			CreateMap<UploadDocumentRequest, OrderDocument>()
				.ForMember(d => d.DocumentId, e => e.Ignore());

			CreateMap<AddDocumentToOrderRequest, OrderDocument>();

			CreateMap<OrderDocument, GetOrderResponse.OrderDocument>()
				.ForMember(d => d.Name, e => e.MapFrom(s => s.DocumentName));

			CreateMap<Order, GetOrderResponse>();

			CreateMap<User, GetUserResponse>();

			CreateMap<UserProfile, GetUserResponse.UserProfile>();

			CreateMap<CodeRequest, User>()
				.ForMember(d => d.Id, e => e.MapFrom(s => Guid.NewGuid()))
				.ForMember(d => d.Name, e => e.MapFrom(s => s.Name))
				.ForMember(d => d.DeviceUid, e => e.MapFrom(s => s.DeviceUid))
				.ForMember(d => d.IsActive, e => e.MapFrom(s => true))
				.ForMember(d => d.Role, e => e.Ignore());

			CreateMap<UpdateUserRequest, User>()
				.ForMember(d => d.Id, e => e.MapFrom(s => Guid.NewGuid()))
				.ForMember(d => d.PasswordSalt, e => e.Ignore())
				.ForMember(d => d.PasswordHash, e => e.Ignore())
				.ForMember(d => d.DeviceUid, e => e.Ignore());

			CreateMap<UpdateUserRequest.UserProfile, UserProfile>();
		}
	}
}
