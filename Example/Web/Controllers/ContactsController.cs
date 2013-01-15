using Autumn.Mvc.Infrastructure.DataAccess;
using Example.Domain;
using Example.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Example.Web.Controllers
{
    public class ContactsController : ApiController
    {
        public ContactsController(IContactsService contacts, IUserSession userSession)
        {
            this.Contacts = contacts;
            this.UserSession = userSession;
        }

        #region Properties

        private IContactsService Contacts { get; set; }
        private IUserSession UserSession { get; set; }

        #endregion

        // GET api/values
        public IEnumerable<Contact> Get()
        {
            var contacts = this.Contacts.GetAll();
            return contacts;
        }

        // GET api/values/5
        public Contact Get(int id)
        {
            return this.Contacts.Get(id);
        }

        // POST api/values
        public HttpResponseMessage Post(Contact contact)
        {
            if (contact.Address == null) contact.AddressId = null;

            this.Contacts.CreateContact(contact);
            this.Contacts.Commit();

            //Construct the response with the Uri to this new item in the header
            var response = Request.CreateResponse<Contact>(HttpStatusCode.Created, contact);
            string uri = Url.Link("DefaultApi", new { controller = "Contacts", id = contact.Id });
            response.Headers.Location = new Uri(uri);

            return response;
        }

        // PUT api/values/5
        public void Put(int id, Contact contact)
        {
            Contact original = this.Get(id);
            if (original != null && original.OwnerId == this.UserSession.CurrentUserId)
            {
                original.Merge(contact);
                this.Contacts.UpdateContact(original);
                this.Contacts.Commit();
                return;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Contact original = this.Get(id);
            if (original == null || original.OwnerId != this.UserSession.CurrentUserId)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            this.Contacts.DeleteContact(id);
            this.Contacts.Commit();
        }

        //#region Helpers

        //private IEnumerable<Contact> GetDefaultContacts()
        //{
        //    List<Contact> contacts = new List<Contact>();
        //    contacts.Add(new Contact("Bill Smith", 35, "bsmith@test.com"));
        //    contacts.Add(new Contact("Joan Jett", 79, "jjett@test.com", new Address("100 Rock Blvd", "Suite 666", "Hollywood", "CA", "21234")));

        //    return contacts;
        //}

        //#endregion
    }
}