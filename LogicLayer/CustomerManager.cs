using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer
{


    public class CustomerManager : ICustomerManager
    {
        private ICustomerAccessor _customerAccessor = null;
        public CustomerManager() 
        {
            _customerAccessor = new CustomerAccessor();
        }

        public CustomerManager(ICustomerAccessor customerManager)
        {
            _customerAccessor = customerManager;
        }   

        public List<Customer> GetAllCustomers()
        {
            List<Customer> result = new List<Customer>();
            try
            {
                result = _customerAccessor.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while retrieving customer data", ex);
            };
            return result;
        }

        public Customer GetCustomerByGivenName(string GivenName)
        {
            Customer customer = null;

            try
            {
                customer = _customerAccessor.GetCustomerUsingGivenName(GivenName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Customer Not Found" , ex);
            }
            return customer;
        }
    }
}
