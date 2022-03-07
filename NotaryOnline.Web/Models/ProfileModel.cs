using System;

namespace NotaryOnline.Web.Models
{
	public class ProfileModel
	{
		public int Id { get; set; }

		public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string DocNumber { get; set; }

        public string DocIssueDepartment { get; set; }

        public string DocIssueCode { get; set; }

        public string DocIssueCountry { get; set; }

        public DateTime? DocIssueDate { get; set; }

        public DateTime? DocValidTo { get; set; }
    }
}
