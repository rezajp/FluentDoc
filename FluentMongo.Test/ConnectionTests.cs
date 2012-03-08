using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentMongo.Repositories;

namespace FluentMongo.Test
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void Save_Adds_Entity_To_Collection()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.AddMap<Customer>(new CustomerMap());
            var connection= connectionFactory.GetConnection<Customer>();
            connection.Save(new Customer() {Id = 1, Address = "Here", Name = "Reza"});
            var customer = connection.Get<Customer>(1);
            Assert.IsNotNull(customer);
            Assert.AreEqual("Reza",customer.Name);
            Assert.AreEqual("Here",customer.Address);
        }
    }
}
