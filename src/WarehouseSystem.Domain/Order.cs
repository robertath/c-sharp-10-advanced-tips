using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace WarehouseSystem.Domain
{
    public record Order(
        [property: JsonPropertyName("total")]
        decimal Total = 0,
                
        [AllowNull]
        ShippingProvider ShippingProvider = default,

        [property: JsonIgnore]
        IEnumerable<Item> LineItems = default,
        bool IsReadyForShipment = true
    )
    {
        [property: JsonIgnore]
        public ShippingProvider ShippingProvider { get; init; } = ShippingProvider ?? new();

        public Guid OrderNumber { get; init; } = Guid.NewGuid();
        
        //public Order()
        //{
        //    OrderNumber = Guid.NewGuid();
        //}

        public string GenerateReport(string email)
        {
            throw new NotImplementedException();
        }


        public void Deconstruct(out decimal total,
            out bool ready)
        {
            total = Total;
            ready = IsReadyForShipment;
        }

        public void Deconstruct(out decimal total,
            out bool ready,
            out IEnumerable<Item> items)
        {
            total = Total;
            ready = IsReadyForShipment;
            items = LineItems;
        }

        protected virtual bool PrintMembers(StringBuilder builder) 
        {
            builder.Append("A custom implementation");
            return true;
        }
    }

    public record PriorityOrder(
        decimal Total,
        ShippingProvider ShippingProvider = default,
        IEnumerable<Item> LineItems = default,
        bool IsReadyForShipment = true
        ) : Order(Total, ShippingProvider, LineItems, IsReadyForShipment) { }

    public record ShippedOrder(
        decimal Total,
        ShippingProvider ShippingProvider,
        IEnumerable<Item> LineItems
        ) : Order(Total, ShippingProvider, LineItems, false)
    { 
        public DateTime ShippedDate { get; set; }   
    }

    public record CancelledOrder(
        decimal Total,
        ShippingProvider ShippingProvider = default,
        IEnumerable<Item> LineItems = default
        ) : Order(Total, ShippingProvider, LineItems, false)
    {
        public DateTime CancelledDate { get; set; }
    }


    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }
}