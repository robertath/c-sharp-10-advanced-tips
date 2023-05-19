namespace WarehouseSystem.Business;

public class PayloadValidator
{
    //public bool Validate(Span<byte> payload)
    public bool Validate(ReadOnlySpan<byte> payload)
    {
        var signature = payload[^128..];
        //payload[0] = 1;
        foreach(var item in signature)
        {
            if (item == 1)
                return false;            
        }

        return true;
    }
}
