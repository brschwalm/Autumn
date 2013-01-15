using System.ComponentModel.DataAnnotations;

namespace TravelBucket.Domain
{
	/// <summary>
	/// Represents a Definition or Enumerated Type in the system
	/// </summary>
	public class Definition
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Definition() { }

		/// <summary>
		/// Constructor that initializes a definition
		/// </summary>
		/// <param name="itemType">The Type of Definition this is</param>
		/// <param name="code">The Code that identifies this item</param>
		/// <param name="name">The Name of this item</param>
		public Definition(string itemType, string code, string name)
		{
			this.ItemType = itemType;
			this.Code = code;
			this.Name = name;
		}

		/// <summary>
		/// The unique identifier for this item
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the Type of item this is
		/// </summary>
		[StringLength(25)]
		public string ItemType { get; set; }

		/// <summary>
		/// Gets or sets whether this is the Default item for its type.
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>
		/// Gets or sets the Code that identifies this item
		/// </summary>
		[StringLength(25)]
		public string Code { get; set; }

		/// <summary>
		/// Gets or sets the sub-code to identify this item
		/// </summary>
		[StringLength(25)]
		public string SubCode { get; set; }

		/// <summary>
		/// Gets or sets the Name of this item
		/// </summary>
		[StringLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a Description for this item
		/// </summary>
		[StringLength(500)]
		public string Description { get; set; }
	}
}
