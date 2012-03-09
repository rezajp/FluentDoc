using System;
using System.Collections.Generic;
using System.Reflection;
using FleuntDoc.Repositories;
using FleuntDoc.Repositories.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FleuntDoc.Test
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void Save_Adds_Entity_To_Collection()
        {
            var connectionFactory = new ConnectionFactory();
            var customerMapperMock = new Mock<IMapper>();
            
            connectionFactory.AddMap<Customer>(new CustomerMap());
            var connection= connectionFactory.GetConnection<Customer>();
            connectionFactory.Mappers = new Dictionary<Type, IMapper> {{typeof (Customer), customerMapperMock.Object}};
            connection.Save<Customer>(new Customer() {Id = 1, Address = "Here", Name = "Reza"});
            var customer = connection.Get<Customer>(1);
            Assert.IsNotNull(customer);
            Assert.AreEqual("Reza",customer.Name);
            Assert.AreEqual("Here",customer.Address);
        }
    }
}
