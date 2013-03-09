using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Autumn.Mvc.Infrastructure.Domain;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	public interface IAutumnService<TModel> where TModel : AutumnModel
	{
		IEnumerable<TModel> GetAll();
		IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null,
								 Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
								 string includeProperties = "");
		TModel Get(int id);

		void Create(TModel contact);
		void Update(TModel contact);
		void Delete(int id);

		int Commit();
	}
}
