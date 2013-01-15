using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Anythink.Mvc.Infrastructure.Authentication.Domain
{
	/// <summary>
	/// Gets profile information about the current user
	/// </summary>
	public class UserProfile 
	{
		/// <summary>
		/// The UserId for this user, which maps to the Membership provider
		/// </summary>
		[Key]
		public Guid UserId { get; set; }

		/// <summary>
		/// The full name of the user
		/// </summary>
		[Display(Name="Full Name")]
		[StringLength(150)]
		public string FullName { get; set; }

		/// <summary>
		/// In image for the user
		/// </summary>
		public byte[] Image { get; set; }

		/// <summary>
		/// Gets whether or not this user has a password (they won't if they've registered with OpenId)
		/// </summary>
		[HiddenInput(DisplayValue=false)]
		public bool HasPassword { get; set; }

		/// <summary>
		/// A collection of OpenIds that are associated with this user
		/// </summary>
		public virtual ICollection<OpenId> OpenIds { get; set; }

		/// <summary>
		/// A collection of Authorization Tokens for this user
		/// </summary>
		public virtual ICollection<AuthorizationToken> AuthorizationTokens { get; set; }
	}
}
