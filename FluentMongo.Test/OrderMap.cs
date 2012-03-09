using FleuntDoc.Mapping;

namespace FleuntDoc.Test
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
