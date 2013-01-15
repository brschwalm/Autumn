using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anythink.Mvc.Infrastructure.Authentication.Domain
{
	/// <summary>
	/// Model to hold information about the open id links to a user
	/// </summary>
	public class OpenId
	{
		[Key]
		public string ProviderKey { get; set; }
		public string FriendlyName { get; set; }
		public Guid? UserId { get; set; }
	}
}
