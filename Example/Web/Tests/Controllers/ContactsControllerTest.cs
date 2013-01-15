using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example;
using Example.Web.Controllers;
using Example.Domain;

namespace Example.Web.Tests.Controllers
{
    [TestClass]
    public class ContactsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            ContactsController controller = new ContactsController(null, null);

            // Act
            IEnumerable<Contact> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ContactsController controller = new ContactsController(null, null);

            // Act
            Contact result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ContactsController controller = new ContactsController(null, null);

            // Act
            controller.Post(null);

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ContactsController controller = new ContactsController(null, null);

            // Act
            controller.Put(5, null);

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ContactsController controller = new ContactsController(null, null);

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
