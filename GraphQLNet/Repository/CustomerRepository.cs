using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CRUDContext _context;
        public CustomerRepository(CRUDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(long id)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
             await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer dbCustomer, Customer customer)
        {
            dbCustomer.FirstName = customer.FirstName;
            dbCustomer.LastName = customer.LastName;
            dbCustomer.MobileNo = customer.MobileNo;
            dbCustomer.Email = customer.Email;
            dbCustomer.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return dbCustomer;
        }

        public async Task DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}