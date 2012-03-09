using System;
using System.Linq;
using FluentDoc.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentDoc.Test
{
    /// <summary>
    /// Summary description for IdFieldTests
    /// </summary>
    [TestClass]
    public class DocumentMapTests
    {
        [TestMethod]
        public void When_Set_IdField_It_Is_Set_As_Primary_Key()
        {
            var customerMap= new DocumentMap<Customer>();
            customerMap.Id(c => c.Id);
            Assert.AreEqual("Id",customerMap.IdWrapper.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void When_Given_A_NonPrimitive_Id_Type_Throws_Exception()
        {
            var orderMap = new DocumentMap<Order>();
            orderMap.Id(c => c.Customer);

        }
        [TestMethod]
        public void When_Set_Mapped_Fields_It_Is_Added_To_Primitive_Fields()
        {
            var customerMap = new DocumentMap<Customer>();
            customerMap.Map(c => c.Name);
            customerMap.Map(c => c.Address);
            Assert.AreEqual(2, customerMap.GetMappedFields().Count);
            Assert.IsTrue(customerMap.GetMappedFields().Any(m => m.Name == "Name" && m.Type == typeof(string)));
            Assert.IsTrue(customerMap.GetMappedFields().Any(m => m.Name == "Address" && m.Type == typeof(string)));
        }
        [TestMethod]
        public void When_Referenced_It_Is_Added_To_Reference_Fields()
        {
            var orderMap = new DocumentMap<Order>();
            orderMap.Reference(o => o.Customer);
            
            Assert.AreEqual(1, orderMap.GetReferencedFields().Count);
            Assert.IsTrue(orderMap.GetReferencedFields().Any(m => m.Name == "Customer" && m.Type == typeof(Customer)));
            
        }

    }
}
