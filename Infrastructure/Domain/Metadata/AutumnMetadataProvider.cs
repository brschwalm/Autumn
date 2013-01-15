using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Autumn.Mvc.Infrastructure.Domain.Metadata
{
	/// <summary>
	/// This class will apply metadata to a Model based on the metadata attributes that are applied to properties on that model.
	/// that metadata can then be used by the Display and Editor templates to make decisions and automate certain activities.
	/// See the DefinitionAttribute for an example of how this can be used.
	/// </summary>
	public class AutumnMetadataProvider : DataAnnotationsModelMetadataProvider
	{
		/// <summary>
		/// Examines the attributes of an object, looking for custom attributes, and adds the appropriate metadata
		/// if custom attributes are found.
		/// </summary>
		/// <param name="attributes"></param>
		/// <param name="containerType"></param>
		/// <param name="modelAccessor"></param>
		/// <param name="modelType"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
			var additionalValues = attributes.OfType<HtmlPropertyAttribute>();

			if (additionalValues != null && additionalValues.Count() > 0)
			{
				HtmlPropertiesDictionary items = new HtmlPropertiesDictionary(additionalValues);
				metadata.AdditionalValues.Add("HtmlAttributes", items);
			}

			//Metadata for Definitions
			var defType = attributes.OfType<DefinitionTypeAttribute>().FirstOrDefault();
			if (defType != null)
			{
				metadata.AdditionalValues.Add("DefinitionItemType", defType.ItemType);
				if (!string.IsNullOrWhiteSpace(defType.Code))
				{
					metadata.AdditionalValues.Add("DefinitionItemCode", defType.Code);
				}
			}

			return metadata;
		}
	}
}
