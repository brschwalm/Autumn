using System;
using System.Linq;
using Autumn.Mvc.Infrastructure.DataAccess;
using Example.Domain;

namespace Example.Services
{
	/// <summary>
	/// Implementation of the Contacts Service
	/// </summary>
	public class ContactsService : AutumnService<Contact>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="contactRepository"></param>
		/// <param name="addressRepository"></param>
		/// <param name="unitOfWork"></param>
		public ContactsService(IModelRepository<Contact> contactRepository, IModelRepository<Address> addressRepository, IUnitOfWork unitOfWork)
			: base(contactRepository, unitOfWork)
		{
			this.Addresses = addressRepository;
		}

		#region Properties

		private IModelRepository<Address> Addresses { get; set; }

		/// <summary>
		/// Override default includes to retrieve the Address
		/// </summary>
		protected override string DefaultIncludes
		{
			get { return "Address"; }
		}

		/// <summary>
		/// Override default order by to order by Name
		/// </summary>
		protected override Func<IQueryable<Contact>, IOrderedQueryable<Contact>> DefaultOrderBy
		{
			get { return c => c.OrderBy(co => co.Id); }
		}

		#endregion

		/// <summary>
		/// Override to make sure Address gets inserted if present
		/// </summary>
		/// <param name="model"></param>
		protected override void OnCreating(Contact model)
		{
			base.OnCreating(model);

			if (model.Address != null && model.Address.Id <= 0)
			{
				this.Addresses.Insert(model.Address);
			}
		}

		/// <summary>
		/// Override to make sure address gets deleted if present
		/// </summary>
		/// <param name="model"></param>
		protected override void OnDeleting(Contact model)
		{
			base.OnDeleting(model);

			if (model.Address != null)
				this.Addresses.Delete(model.Address.Id);
		}
	}
}
