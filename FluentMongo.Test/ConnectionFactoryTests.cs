using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using FluentMongo.Repositories;
using FleuntMongo.Mapping;

namespace FluentMongo.Test
{
    [TestClass]
    public class ConnectionFactoryTests
    {
        [TestMethod]
        public void Make_Sure_DocumentMap_Is_Added_When_Calling_AddMap()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.AddMap<Customer>(new CustomerMap());
            Assert.IsTrue(connectionFactory.DocumentMaps.ContainsKey(typeof(Customer)) && connectionFactory.DocumentMaps[typeof(Customer)].GetType() == typeof(CustomerMap));
        }
        [TestMethod]
        public void GetConnection_Creates_A_Connection_For_The_Specified_Type()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.AddMap<Order>(new OrderMap());
            connectionFactory.AddMap<Customer>(new CustomerMap());
            var connection = connectionFactory.GetConnection<Order>();
            Assert.IsNotNull(connection);
            Assert.AreEqual(typeof(Order),connection.AssociatedType);
        }
    }

    public class CustomerMap:DocumentMap<Customer>
    {
        public CustomerMap()
        {
            Id(c => c.Id);
            Map(c => c.Name);
        }
    }
}
