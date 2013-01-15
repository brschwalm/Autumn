using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Autumn.Mvc.Infrastructure.Domain
{
	/// <summary>
	/// Base Class for domain models.  Includes critical fields for tracking the model and
	/// identifying the ownership of it.
	/// </summary>
	public abstract class AutumnModel : IValidatableObject, IAutumnModel
	{
		/// <summary>
		/// Id of this model
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// User that owns this model
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public Guid OwnerId { get; set; }

		/// <summary>
		/// Date this model was created
		/// </summary>
		[DataType(DataType.Date)]
		[HiddenInput(DisplayValue = false)]
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Date this model was last updated
		/// </summary>
		[DataType(DataType.Date)]
		[HiddenInput(DisplayValue = false)]
		public DateTime DateLastUpdated { get; set; }

        /// <summary>
        /// Date this model was deactivated
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? DateDeactivated { get; set; }

        /// <summary>
        /// Flag to indicate whether or not the model is active.
        /// </summary>
        public bool IsActive { get; set; }

		/// <summary>
		/// Type of model
		/// </summary>
		[ScaffoldColumn(false)]
		[NotMapped]
		public virtual string ModelType { get { return "model"; } }

		/// <summary>
		/// Merges this model with another instance of the same type.  Not implemented
		/// in the base class.
		/// </summary>
		/// <param name="other"></param>
		public virtual void Merge(AutumnModel other)
		{
			if (other.GetType() != this.GetType())
				throw new ArgumentException("Cannot merge item of a different type.");
			if (this.Id > 0 && other.Id != this.Id)
				throw new ArgumentException("Cannot merge item with a different id.");

			//If a derived class wants this to work, they'll need to implement it.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Allows for centralized intialize functionality
		/// </summary>
		public virtual void Initialize(DateTime createdDate, Guid? ownerId) 
		{
			this.IsActive = true;
			this.DateCreated = createdDate;
			this.DateLastUpdated = createdDate;
			this.OwnerId = ownerId.HasValue ? ownerId.Value : this.OwnerId;
			this.InitializeChildren(createdDate, ownerId.HasValue ? ownerId.Value : this.OwnerId);
		}

		/// <summary>
		/// Initializes the children of this object
		/// </summary>
		/// <param name="createdDate"></param>
		protected virtual void InitializeChildren(DateTime createdDate, Guid? ownerId) { }

		#region IValidatableObject Members

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			return new Collection<ValidationResult>();
		}

		#endregion
	}
}
