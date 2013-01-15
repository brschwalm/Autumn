using Autumn.Mvc.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain
{
    public class Address : AutumnModel
    {
        public Address()
        {
            //To make sure these are populated
            this.DateCreated = this.DateLastUpdated = DateTime.UtcNow;
        }

        public Address(string s1, string s2, string city, string state, string zip)
            : this()
        {
            this.Street1 = s1;
            this.Street2 = s2;
            this.City = city;
            this.State = state;
            this.ZipCode = zip;
        }

        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
