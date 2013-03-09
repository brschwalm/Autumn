using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Autumn.Mvc.Infrastructure.DataAccess;
using Autumn.Mvc.Infrastructure.Domain;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	public abstract class AutumnController<TService, TModel> : ApiController where TModel : AutumnModel where TService : IAutumnService<TModel> 
	{
		public AutumnController(TService service, IUserSession session)
		{
			this.Service = service;
			this.UserSession = session;
		}

		#region Properties

		private TService Service { get; set; }
		private IUserSession UserSession { get; set; }

		protected abstract string ControllerName { get; }
		protected virtual string RouteName { get { return "DefaultApi"; } }

		#endregion

		#region Verbs

		// GET api/values
		public IEnumerable<TModel> Get()
		{
			return this.Service.GetAll();
		}

		// GET api/values/5
		public TModel Get(int id)
		{
			return this.Service.Get(id);
		}

		// POST api/values
		public virtual HttpResponseMessage Post(TModel model)
		{
			//if (contact.Address == null) contact.AddressId = null;
			model.Prepare();

			this.Service.Create(model);
			this.Service.Commit();

			//Construct the response with the Uri to this new item in the header
			var response = Request.CreateResponse<TModel>(HttpStatusCode.Created, model);
			string uri = Url.Link(this.RouteName, new { controller = this.ControllerName, id = model.Id });
			response.Headers.Location = new Uri(uri);

			return response;
		}

		// PUT api/values/5
		public virtual void Put(int id, TModel contact)
		{
			TModel original = this.Get(id);
			if (original != null && original.OwnerId == this.UserSession.CurrentUserId)
			{
				original.Merge(contact);
				this.Service.Update(original);
				this.Service.Commit();
				return;
			}

			throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		// DELETE api/values/5
		public virtual void Delete(int id)
		{
			TModel original = this.Get(id);
			if (original == null || original.OwnerId != this.UserSession.CurrentUserId)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			this.Service.Delete(id);
			this.Service.Commit();
		}

		#endregion

	}
}
