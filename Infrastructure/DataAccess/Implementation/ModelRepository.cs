using System;
using System.Linq;
using System.Linq.Expressions;
using Autumn.Mvc.Infrastructure.Domain;

namespace Autumn.Mvc.Infrastructure.DataAccess.Implementation
{
    public class ModelRepository<TModel> : Repository<TModel, int>, IModelRepository<TModel> where TModel : AutumnModel
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="userSession"></param>
		public ModelRepository(IApplicationContext context, IUserSession userSession)
			: base(context)
		{
			this.UserSession = userSession;
		}

		#region Properties

		protected IUserSession UserSession { get; set; }

        /// <summary>
        /// Indicates whether inactive models should be included in the results or not.
        /// </summary>
        public bool IncludeInactive { get; set; }

        /// <summary>
        /// A flag to indicate the user filter should not be applied in this case
        /// </summary>
        public bool SkipOwnerFilter { get; set; }

		#endregion

		/// <summary>
		/// Gets an Entity by the Id and verifies that the Owner ID matches that of the current
		/// user session
		/// </summary>
		/// <param name="id"></param>
		/// <param name="includeProperties"></param>
		/// <returns></returns>
		public override TModel GetById(int id, string includeProperties = null)
		{
			TModel entity = base.Get(e => e.Id == id, null, includeProperties).FirstOrDefault(); // base.GetById(id, includeProperties);
			if (entity != null && (!this.SkipOwnerFilter && entity.OwnerId != this.UserSession.CurrentUserId))
			{
				throw new UnauthorizedAccessException("Current session user is not authorized to get the selected entity.");
			}

			return entity;
		}

		/// <summary>
		/// Initializes the Query used in the Get method of the repository
		/// </summary>
		/// <param name="filter">The filter to apply to the query</param>
		/// <returns>The Query with the UserId filter added</returns>
		protected override IQueryable<TModel> InitializeQuery(Expression<Func<TModel, bool>> filter)
		{
			IQueryable<TModel> query = this.DbSet;

            Expression<Func<TModel, bool>> userFilter = m => m.OwnerId == this.UserSession.CurrentUserId;
            Expression<Func<TModel, bool>> activeFilter = m => m.IsActive;
            
            //Build the query by applying the built-in filters
            IQueryable<TModel> result = query; //.Where(userFilter);
            if (!this.SkipOwnerFilter)
            {
                //TODO: Log the fact that user filter is being skipped
                result = result.Where(userFilter);
            }

            if (!this.IncludeInactive)
                result = result.Where(activeFilter);
            
            if (filter != null)
                result = result.Where(filter);

            return result;
		}

		/// <summary>
		/// Prepares a model entity to be inserted by performing the necessary initialization
		/// </summary>
		/// <param name="entity"></param>
		protected override bool PrepareInsert(TModel entity)
		{
			var result = base.PrepareInsert(entity);

			//If the session hasn't been set yet...
			if (result && this.UserSession.IsAuthenticated)
			{
				if (this.SkipOwnerFilter)
				{
					if (entity.OwnerId == Guid.Empty)
					{
						result = false;
						throw new InvalidOperationException("When SkipUserFilter is true, entity Id must be manually set.");
					}
					else
						entity.Initialize(DateTime.UtcNow, null);
				}
				else
					entity.Initialize(DateTime.UtcNow, this.UserSession.CurrentUserId);
			}

			return result;
		}

		/// <summary>
		/// Prepares a model entity to be updated.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		protected override bool PrepareUpdate(TModel entity)
		{
			bool result =  base.PrepareUpdate(entity);
			if (result)
			{
				if (entity.OwnerId != this.UserSession.CurrentUserId)
				{
					throw new UnauthorizedAccessException("Curent Session User is not authorized to modify the selected entity.");
				}

				entity.DateLastUpdated = DateTime.UtcNow;
			}

			return result;
		}

		/// <summary>
		/// Prepares a model to be deleted - will mark it as inactive rather than deleting it
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		protected override bool PrepareDelete(TModel entity)
		{
			bool result =  base.PrepareDelete(entity);
			if (result)
			{
				if (entity.OwnerId != this.UserSession.CurrentUserId)
				{
					throw new UnauthorizedAccessException("The curent user is not authorized to modify the selected entity.");
				}

				entity.IsActive = false;
				entity.DateDeactivated = DateTime.UtcNow;
			}

			return false;	//Don't delete the entity, just mark it inactive.
		}
		
	}

}
