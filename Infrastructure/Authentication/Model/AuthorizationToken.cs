using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anythink.Mvc.Infrastructure.Authentication.Domain
{
	/// <summary>
	/// Model used to store authorization information for OAuth
	/// </summary>
	public class AuthorizationToken
	{
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// The user this token is for
		/// </summary>
		public Guid UserId { get; set; }
		
		/// <summary>
		/// The endpoint where this token is used (e.g. linked in)
		/// </summary>
		public string Endpoint { get; set; }

		/// <summary>
		/// The Token
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// The token secret
		/// </summary>
		public string Secret { get; set; }
	}
}
