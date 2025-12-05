using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
	public class SessionState
	{
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }

        public string mobile { get; set; }
		public int empid { get; set; }
		public string empname { get; set; }
		public int userroleid { get; set; }
		public string? otp { get; set; }
	}
}
