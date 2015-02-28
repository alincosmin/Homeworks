using System;
using System.Linq;

namespace Tema1.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCustomers = new CustomersService();

            //var newEmail = new CustomerEmail()
            //{
            //    Customer = new Customer()
            //    {
            //        CustomerType = new CustomerType()
            //        {
            //            Description = "testtype"
            //        }
            //        ,
            //        Name = "testname"
            //    }
            //    ,
            //    Email = "testemail"
            //};

            //serviceCustomers.CreateCustomerEmail(newEmail.Email, newEmail.Customer);

            //var list = serviceCustomers.GetCustomers().ToList();
            //Console.WriteLine("Result: {0}", list.Count);

            //Console.WriteLine("Customers:");
            //foreach (var type in serviceCustomers.GetCustomerTypes())
            //{
            //    Console.WriteLine("\t{0}", type.Description);
            //    foreach (var customer in type.Customers)
            //    {
            //        Console.WriteLine("\t \t{0}", customer.Name);
            //        foreach (var email in customer.CustomerEmails)
            //        {
            //            Console.WriteLine("\t \t \t{0}", email.Email);
            //        }
            //        Console.WriteLine("");
            //    }
            //    Console.WriteLine("");
            //}

            Console.WriteLine(new CustomersModelContainer().Customers.Count());
        }
    }
}
