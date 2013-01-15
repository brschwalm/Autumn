using Autumn.Mvc.Infrastructure.DataAccess;
using Example.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Example.Services
{
	/// <summary>
	/// Interface describing a service for CRUD operations on Contacts
	/// </summary>
    public interface IContactsService
    {
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Get(Expression<Func<Contact, bool>> filter = null,
                                 Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderBy = null,
                                 string includeProperties = "");
        Contact Get(int id);

        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int id);

        int Commit();
    }

	/// <summary>
	/// Implementation of the Contacts Service
	/// </summary>
    public class ContactsService : IContactsService
    {
        public ContactsService(IModelRepository<Contact> contactRepository, IModelRepository<Address> addressRepository, IUnitOfWork unitOfWork)
        {
            this.Contacts = contactRepository;
            this.Addresses = addressRepository;
            this.UnitOfWork = unitOfWork;
        }

        #region Properties

        private IModelRepository<Contact> Contacts { get; set; }
        private IModelRepository<Address> Addresses { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        #endregion

        public IEnumerable<Contact> GetAll()
        {
            return this.Contacts.Get(null, c => c.OrderBy(co => co.Name), "Address");
        }

        public IEnumerable<Contact> Get(Expression<Func<Contact, bool>> filter = null, Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderBy = null, string includeProperties = "")
        {
            return this.Contacts.Get(filter, orderBy, includeProperties);
        }

        public Contact Get(int id)
        {
            return this.Contacts.GetById(id, "Address");
        }

        public void CreateContact(Contact contact)
        {
            if (contact.Address != null && contact.Address.Id <= 0)
            {
                this.Addresses.Insert(contact.Address);
            }

            this.Contacts.Insert(contact);
        }

        public void UpdateContact(Contact contact)
        {
            this.Contacts.Update(contact);
        }

        public void DeleteContact(int id)
        {
            Contact c = this.Get(id);
            if (c != null)
            {
                if (c.Address != null)
                    this.Addresses.Delete(c.Address.Id);

                this.Contacts.Delete(id);
            }
        }

        public int Commit()
        {
            return this.UnitOfWork.Commit();
        }
    }
}
