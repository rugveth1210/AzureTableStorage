using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableStorageBackend.Models
{
    public class Employee : TableEntity
    {
        public Employee(string FirstName, string LastName)
        {
            this.PartitionKey = FirstName; this.RowKey = LastName;
        }
        public Employee() { }

        //#model_attributes_here
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}