using Autumn.Mvc.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Example.Domain
{
    public class Contact : AutumnModel
    {
        public Contact()
        {
            this.DateCreated = this.DateLastUpdated = DateTime.UtcNow;
        }

        public Contact(string name, int age, string email, Address address = null)
            : this()
        {
            this.Name = name;
            this.Age = Math.Abs(age);
            this.Email = email;

            if (address != null)
            {
                this.Address = address;
                this.AddressId = address.Id;
            }
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        public override void Merge(AutumnModel other)
        {
            var source = other as Contact;
            if (source != null)
            {
                this.Name = source.Name;
                this.Age = source.Age;
                this.Email = source.Email;
            }
        }

		public override void Prepare()
		{
			if (this.Address == null) this.AddressId = null;
		}
    }
}
