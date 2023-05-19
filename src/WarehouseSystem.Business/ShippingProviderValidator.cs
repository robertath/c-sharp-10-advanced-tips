using System.Diagnostics.CodeAnalysis;
using WarehouseSystem.Domain;

namespace WarehouseSystem.Business;

public class ShippingProviderValidator
{
    public static bool
        ValidateShippingProvider(
        [NotNullWhen(true)]
            ShippingProvider provider)
    {
        if (provider is { FreightCost: > 100 })
        {
            return true;
        }

        return false;
    }
}
