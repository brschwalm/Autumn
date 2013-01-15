using System;
using System.Collections.Generic;

namespace Autumn.Mvc.Infrastructure.Domain.Metadata
{
	/// <summary>
	/// Used to add html properties from the Model classes themselves.  For example, can identify the
	/// field that gets initial focus on a model.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
	public class HtmlPropertyAttribute : Attribute
	{
		public HtmlPropertyAttribute() { }

		public HtmlPropertyAttribute(string name, object value)
		{
			this.AttributeName = name;
			this.AttributeValue = value;
		}

		public string AttributeName { get; set; }
		public object AttributeValue { get; set; }
	}


	public class HtmlPropertiesDictionary : Dictionary<string, object>
	{
		public HtmlPropertiesDictionary() { }

		public HtmlPropertiesDictionary(IEnumerable<HtmlPropertyAttribute> attrs)
		{
			foreach (HtmlPropertyAttribute attr in attrs) this.Add(attr.AttributeName, attr.AttributeValue);
		}
	}
}
