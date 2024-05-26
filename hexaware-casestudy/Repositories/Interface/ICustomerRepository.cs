using System;
using hexaware_casestudy.Model;

namespace hexaware_casestudy.Repositories
{
	public interface ICustomerRepository
	{
        Customer GetCustomerById(int customerId);
        bool CreateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
    }
}

