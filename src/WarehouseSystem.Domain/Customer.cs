using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSystem.Domain
{
    public record Customer(string FirstName, string LastName)
    {
        public string FullName => $"{FirstName} {LastName}";
        //public Address Address { get; init; } //mutable
        public Address Address { get; init; } //imutable

        public bool Validate()
        {
            return true;
        }
    }

    public record Address(string street, string postalCode);

    public record PriorityCustomer(string FirstName, string LastName) : Customer("", "");
}
