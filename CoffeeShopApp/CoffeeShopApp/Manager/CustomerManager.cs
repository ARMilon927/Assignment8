using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShopApp.Repository;
using CoffeeShopApp.Model;

namespace CoffeeShopApp.Manager
{
    public class CustomerManager
    {
        CustomerRepository _customerRepository = new CustomerRepository();
        public bool ExistCustomer(Customer customer)
        {
            return _customerRepository.ExistCustomer( customer);
        }

        public string InsertCustomer(Customer customer)
        {
            string message;
            message = _customerRepository.InsertCustomer(customer) > 0 ? "Customer is Saved" : "Customer is not saved";
            return message;
        }

        public DataTable ShowCustomer()
        {
            return _customerRepository.ShowCustomer();
        }

        public bool DeleteCustomer(int id)
        {
            return _customerRepository.DeleteCustomer(id);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);

        }
        public DataTable SearchCustomer(Customer customer)
        {
            return _customerRepository.SearchCustomer(customer);
        }
    }
}
