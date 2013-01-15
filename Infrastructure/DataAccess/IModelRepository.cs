using Autumn.Mvc.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
    public interface IModelRepository<TModel> : IRepository<TModel, int> where TModel : AutumnModel
    {
		/// <summary>
		/// Indicicates whether or not the automatic owner filter should be skipped.  Defaults to false.
		/// </summary>
        bool SkipOwnerFilter { get; set; }

		/// <summary>
		/// Indicates whether or not inactive items should be included in the results.  Defaults to false.
		/// </summary>
        bool IncludeInactive { get; set; }
    }
}
