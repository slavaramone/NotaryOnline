using AutoMapper;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Web.Models;

namespace NotaryOnline.Web
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetOrderResponse.OrderDocument, OrderModel.OrderDocumentModel>();

			CreateMap<GetOrderResponse, OrderModel>()
				.ForMember(d => d.CreatedDate, e => e.MapFrom(s => s.CreatedDate.ToShortDateString()))
				.ForMember(d => d.DueDate, e => e.MapFrom(s => s.DueDate.HasValue ? s.DueDate.Value.ToShortDateString() : "не ограничен"))
				.ForMember(d => d.CompletedDate, e => e.MapFrom(s => s.CompletedDate.HasValue ? s.CompletedDate.Value.ToShortDateString() : string.Empty));

			CreateMap<GetUserResponse.UserProfile, ProfileModel>();

			CreateMap<GetUserResponse, UserModel>();

			CreateMap<ProfileModel, UpdateUserRequest.UserProfile>();

			CreateMap<UserModel, UpdateUserRequest>();
		}
	}
}
