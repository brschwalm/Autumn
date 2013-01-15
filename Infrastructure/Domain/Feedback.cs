using System;
using System.ComponentModel.DataAnnotations;

namespace Autumn.Mvc.Infrastructure.Domain
{
	/// <summary>
	/// A feedback model that can be used to capture feedback about an application
	/// </summary>
	public class Feedback
	{
		/// <summary>
		/// The Id of this feedback
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// The user who sent this feedback, if they were authenticated
		/// </summary>
		public Guid? OwnerId { get; set; }

		/// <summary>
		/// The date this feedback was created
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// The subject of the feedback
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// The body of the feedback
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The email address of the user who gave the feedback
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// The name of the user who gave the feedback
		/// </summary>
		public string Name { get; set; }

	}
}
