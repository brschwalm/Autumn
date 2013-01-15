using System;

namespace Autumn.Mvc.Infrastructure.Domain
{
	/// <summary>
	/// Interface that describes a model in the Autumn MVC Framework
	/// </summary>
	public interface IAutumnModel
	{
		/// <summary>
		/// The type of model
		/// </summary>
		string ModelType { get; }

		/// <summary>
		/// The Id of this model entity
		/// </summary>
		int Id { get; }

		/// <summary>
		/// The user that owns this model
		/// </summary>
		Guid OwnerId { get; }

		/// <summary>
		/// The date this model was created
		/// </summary>
		DateTime DateCreated { get; }

		/// <summary>
		/// The date this model was last updated
		/// </summary>
		DateTime DateLastUpdated { get; }

		/// <summary>
		/// Whether or not this model is active
		/// </summary>
		bool IsActive { get; }

		/// <summary>
		/// The date this model was deactivated
		/// </summary>
		DateTime? DateDeactivated { get; }
	}
}
