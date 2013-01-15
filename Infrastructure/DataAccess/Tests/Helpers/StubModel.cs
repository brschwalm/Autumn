using System;
using Autumn.Mvc.Infrastructure.Domain;
using Workfully.DomainModel;

namespace Workfully.DomainModel.Test.Helpers
{
	/// <summary>
	/// Stub model used for unit tests
	/// </summary>
	public class StubModel : AutumnModel
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public StubModel(int id, string name, string description = null)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id"></param>
		/// <param name="userId"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public StubModel(int id, Guid userId, string name, string description = null)
		{
			this.Id = id;
			this.OwnerId = userId;
			this.Name = name;
			this.Description = description;
		}

		public string Name { get; set; }
		public string Description { get; set; }

		public override string ModelType
		{
			get
			{
				return "StubModel";
			}
		}
	}
}
