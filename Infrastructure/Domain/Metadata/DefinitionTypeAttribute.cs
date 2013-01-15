using System;

namespace Autumn.Mvc.Infrastructure.Domain.Metadata
{
	/// <summary>
	/// This attribute can be applied to a property of a Model and used in conjunction with the appropriate MetadataProvider
	/// and the appropriate Display and Editor template to automatically present the definition information.  For example,
	/// with the display template, it can be used to automatically display the Name of the item, or with the editor
	/// template, it can be used to automatically populate a combo box of available options based on the definition type
	/// and optionally code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class DefinitionTypeAttribute : Attribute
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public DefinitionTypeAttribute() { }

		/// <summary>
		/// Constructor.  Takes the type of definition item.
		/// </summary>
		/// <param name="itemType">The type of definition item.</param>
		public DefinitionTypeAttribute(string itemType)
		{
			this.ItemType = itemType;
		}

		/// <summary>
		/// Constructor.  Takes the type of definition item and the code.
		/// </summary>
		/// <param name="itemType">The type of definition item.</param>
		/// <param name="code">The code for the definition item.</param>
		public DefinitionTypeAttribute(string itemType, string code)
			: this(itemType)
		{
			this.Code = code;
		}

		/// <summary>
		/// The item type of the definition
		/// </summary>
		public string ItemType { get; set; }

		/// <summary>
		/// The code of the definition
		/// </summary>
		public string Code { get; set; }
	}
}
