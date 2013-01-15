using Example.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Example.Web.Helpers
{
    public class ExampleContextInitializer : DropCreateDatabaseIfModelChanges<ExampleContext>
    {
        protected override void Seed(ExampleContext context)
        {
            base.Seed(context);
            this.SeedContacts(context);
        }

        private void SeedContacts(ExampleContext context)
        {
            Guid userId = Guid.Parse("EAFE8C58-DF4E-4390-A93E-2F8370408CDE");

            context.Contacts.Add(new Contact("Bill Smith", 35, "bsmith@test.com") { OwnerId = userId, IsActive = true });
            context.Contacts.Add(new Contact("Joan Jett", 79, "jjett@test.com", new Address("100 Rock Blvd", "Suite 666", "Hollywood", "CA", "21234") { OwnerId = userId, IsActive = true }) { OwnerId = userId, IsActive = true });
            context.Commit();
        }
    }
}