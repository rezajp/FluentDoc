using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FleuntMongo.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMongo.Test
{
    [TestClass]
    public class FieldWrapperTests
    {
        [TestMethod]
        public void SetMember_Sets_The_MemberInfo()
        {
            var fieldWrapper = new FieldWrapper().SetMember<Customer>(c=>c.Name);
            Assert.AreEqual("Name", fieldWrapper.Name);
            Assert.AreEqual(typeof(string),fieldWrapper.Type);
        }
        
       
    }
}
