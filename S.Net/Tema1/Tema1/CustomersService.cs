using System.Data;
using System.Linq;

namespace Tema1
{
    public class CustomersService
    {
        private readonly CustomersModelContainer _context;

        public CustomersService()
        {
            _context = new CustomersModelContainer();
        }

        public IQueryable<CustomerType> GetCustomerTypes()
        {
            return _context.CustomerTypes.AsQueryable();
        }

        public void CreateCustomerType(string description)
        {
            if (_context.CustomerTypes.Any(x => x.Description.Equals(description)))
            {
                throw new DataException("Customer type with provided description already exists");
            }

            var type = new CustomerType
            {
                Description = description
            };

            _context.CustomerTypes.Add(type);
            _context.SaveChanges();
        }

        public IQueryable<Customer> GetCustomers()
        {
            return _context.Customers.AsQueryable();
        } 

        public void CreateCustomer(string name, CustomerType type)
        {
            var customer = new Customer()
            {
                Name = name,
                CustomerType = type
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public IQueryable<CustomerEmail> GetCustomerEmails()
        {
            return _context.CustomerEmails.AsQueryable();
        } 

        public void CreateCustomerEmail(string email, Customer customer)
        {
            var newEmail = new CustomerEmail()
            {
                Email = email,
                Customer = customer
            };

            _context.CustomerEmails.Add(newEmail);
            _context.SaveChanges();
        }
    }
}
