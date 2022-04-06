using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using AzureTableStorageBackend.Models;

namespace AzureTableStorageBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration config;
        public EmployeeController(IConfiguration configuration)
        {
            config = configuration;
        }

     
        [HttpGet]          
        public IEnumerable<Employee> Get()
        {

            var query = new TableQuery<Employee>();

            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("Employee");
            var lst = table.ExecuteQuery(query);
            return lst.ToList();
        }
        [HttpPost]
        public IEnumerable<Employee> Post([FromBody]Employee employeeRef)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();

            var table = client.GetTableReference("Employee");

            table.CreateIfNotExists();

            Employee employee = new Employee(employeeRef.FirstName, employeeRef.LastName);
           
            //#Post_attribute_assign_values
            employee.FirstName = employeeRef.FirstName;
            employee.LastName = employeeRef.LastName;
            employee.Email = employeeRef.Email;
            employee.PhoneNumber = employeeRef.PhoneNumber;

            var query = new TableQuery<Employee>();
            TableOperation insertOperation = TableOperation.Insert(employee);
           
           
            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();
           
        }

        [HttpPut]
        public IEnumerable<Employee> Put([FromBody] Employee employeeRef)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();

            var table = client.GetTableReference("Employee");

            table.CreateIfNotExists();

            Employee employee = new Employee(employeeRef.FirstName, employeeRef.LastName);
           
            //#Put_attribute_assign_values
            employee.FirstName = employeeRef.FirstName;
            employee.LastName = employeeRef.LastName;
            employee.Email = employeeRef.Email;
            employee.PhoneNumber = employeeRef.PhoneNumber;

            var query = new TableQuery<Employee>();
            TableOperation insertOperation = TableOperation.InsertOrMerge(employee);
            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();

        }

        [HttpDelete]
        public IEnumerable<Employee> Delete([FromBody] Employee employeeRef)
        {
            string _dbCon1 = config.GetSection("ConnectionStrings").GetSection("MyAzureTable").Value;
            // Method 2
            string _dbCon2 = config.GetValue<string>("ConnectionStrings:MyAzureTable");
            var account = CloudStorageAccount.Parse(_dbCon2);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("Employee");
            table.CreateIfNotExists();
            Employee employee = new Employee(employeeRef.FirstName, employeeRef.LastName);
           
            //#Delete_attribute_assign_values
            employee.FirstName = employeeRef.FirstName;
            employee.LastName = employeeRef.LastName;
            employee.Email = employeeRef.Email;
            employee.PhoneNumber = employeeRef.PhoneNumber;

            employee.ETag = "*"; // wildcard
            var query = new TableQuery<Employee>();
            TableOperation insertOperation = TableOperation.Delete(employee);
            table.Execute(insertOperation);
            var lst = table.ExecuteQuery(query);
            return lst.ToList();

        }

    }
}