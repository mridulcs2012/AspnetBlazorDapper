using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class AdminModel
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Enter a username")]
		[StringLength(50, ErrorMessage = "That username is too long")]
		//[Required]
		//[StringLength(50)]
		public string? Username { get; set; }

		//[Required(ErrorMessage = "Enter a Email Address")]
        //[EmailAddress]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
		public string? Email { get; set; }

		//[Required(ErrorMessage = "Enter a Email Address")]
		//[StringLength(15, ErrorMessage = "Mobile Number")]
		public string? Mobile { get; set; }
		
        public string? Password { get; set; }
        public string? ConPassword { get; set; }
        public bool Remember { get; set; }
		public bool IsUpdate { get; set; } = false;

		[Range(1, int.MaxValue, ErrorMessage = "Please Select Location")]
		public int EmployeeID { get; set; }
        public string Name { get; set; }
        public Employee Employee_R { get; set; }
		public bool Registered { get; set; }
		public int UserRoleId { get; set; }
		public string OTP { get; set; }
    }
}
