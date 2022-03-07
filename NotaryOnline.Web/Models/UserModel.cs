using Microsoft.AspNetCore.Mvc.Rendering;
using NotaryOnline.Api.ServiceModel;
using System;

namespace NotaryOnline.Web.Models
{
	public class UserModel
	{
		public Guid Id { get; set; }

        public string Name { get; set; }

        public UserRole Role { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public string Password { get; set; }

        public SelectList SelectListStatus { get; set; }

        public ProfileModel Profile { get; set; }
    }
}
