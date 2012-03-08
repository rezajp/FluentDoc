using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMongo.Test
{
    public class Order
    {
        public Customer Customer;

        public int Id { get; set; }

        public string Description { get; set; }
    }
}
