using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Autumn.Mvc.Infrastructure.Domain;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// Base service for interacting with a repository and unit of work.  Implements functionality for
	/// standard CRUD operations on a single repository.
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public abstract class AutumnService<TModel> : IAutumnService<TModel> where TModel : AutumnModel
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="unitOfWork"></param>
		public AutumnService(IModelRepository<TModel> repository, IUnitOfWork unitOfWork)
        {
			this.Repository = repository;
            this.UnitOfWork = unitOfWork;
        }

        #region Properties

		/// <summary>
		/// The primary repository for this service
		/// </summary>
        protected IModelRepository<TModel> Repository { get; set; }

		/// <summary>
		/// The unit of work
		/// </summary>
		protected IUnitOfWork UnitOfWork { get; set; }

		/// <summary>
		/// Virtual property to allow derived classes to control default order by behavior for GetAll()
		/// </summary>
		protected virtual Func<IQueryable<TModel>, IOrderedQueryable<TModel>> DefaultOrderBy
		{
			get { return m => m.OrderBy(mo => mo.Id); }
		}

		/// <summary>
		/// Virtual property to allow derived classes to control default includes for GetAll()
		/// </summary>
		protected virtual string DefaultIncludes
		{
			get { return string.Empty; }
		}

        #endregion
				
		/// <summary>
		/// Gets all the model items from the repository
		/// </summary>
		/// <returns></returns>
        public virtual IEnumerable<TModel> GetAll()
        {
			return this.Repository.Get(null, this.DefaultOrderBy, this.DefaultIncludes);
        }

		/// <summary>
		/// Gets specific models from the repository based on filter criteria.
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="orderBy"></param>
		/// <param name="includeProperties"></param>
		/// <returns></returns>
		public virtual IEnumerable<TModel> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, string includeProperties = "")
        {
			return this.Repository.Get(filter, orderBy, includeProperties);
        }

		/// <summary>
		/// Gets a specific model from the repository by its Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual TModel Get(int id)
        {
			return this.Repository.GetById(id, "Address");
        }

		/// <summary>
		/// Adds a newly created model to the repository
		/// </summary>
		/// <param name="model"></param>
		public virtual void Create(TModel model)
        {
			this.OnCreating(model);
			this.Repository.Insert(model);
        }

		/// <summary>
		/// Allows derived classes to perform any actions prior to the model being added to the repository
		/// </summary>
		/// <param name="model"></param>
		protected virtual void OnCreating(TModel model)
		{
		}

		/// <summary>
		/// Updates a model in the repository
		/// </summary>
		/// <param name="model"></param>
		public virtual void Update(TModel model)
        {
			this.Repository.Update(model);
        }

		/// <summary>
		/// Allows derived classes to perform any actions prior to the model being updated in the repository
		/// </summary>
		/// <param name="model"></param>
		protected virtual void OnUpdating(TModel model)
		{
		}

		/// <summary>
		/// Deletes a model from the repository by its id
		/// </summary>
		/// <param name="id"></param>
		public virtual void Delete(int id)
        {
			TModel c = this.Get(id);
            if (c != null)
            {
				this.OnDeleting(c);
                this.Repository.Delete(id);
            }
        }

		/// <summary>
		/// Allows derived classes to perform any actions prior to the model being deleted from the repository
		/// </summary>
		/// <param name="model"></param>
		protected virtual void OnDeleting(TModel model)
		{
		}

		/// <summary>
		/// Commits unsaved changes to the Unit of Work
		/// </summary>
		/// <returns></returns>
		public virtual int Commit()
        {
            return this.UnitOfWork.Commit();
        }
	}
}
