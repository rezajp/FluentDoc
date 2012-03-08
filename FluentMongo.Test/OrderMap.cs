using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleuntMongo.Mapping;

namespace FluentMongo.Test
{
    public class OrderMap:DocumentMap<Order>
    {
        public OrderMap()
        {
            Id(o => o.Id);
            Map(o => o.Description);
            Reference(o => o.Customer);
        }
    }
}
