using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anythink.Mvc.Infrastructure.Authentication.Domain
{
	public class LogOnModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }

		[Display(Name = "OpenID")]
		public string OpenID { get; set; }
	}

}
