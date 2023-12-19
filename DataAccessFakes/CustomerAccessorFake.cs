using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class CustomerAccessorFake : ICustomerAccessor
    {
        private List<Customer> fakeCustomers = new List<Customer>();

        public CustomerAccessorFake() 
        {
            fakeCustomers.Add(new Customer() 
            {
                CustomerID = 1000,
                GivenName = "John",
                FamilyName = "Johnson",
                Email = "Johnson-John@gmail.com",
                Phone = "492-546-2345"
            });

            fakeCustomers.Add(new Customer()
            {
                CustomerID = 10001,
                GivenName = "Jakob",
                FamilyName = "William",
                Email = "JakobWilliam@gmail.com",
                Phone = "423-345-2345"
            });

            fakeCustomers.Add(new Customer()
            {
                CustomerID = 10002,
                GivenName = "Beth",
                FamilyName = "Connor",
                Email = "CBethgmail.com",
                Phone = "084-545-9832"
            });

            fakeCustomers.Add(new Customer()
            {
                CustomerID = 10003,
                GivenName = "Emily",
                FamilyName = "Tokal",
                Email = "TokalE@yahoo.com",
                Phone = "980-345-9879"
            });


        }

        public List<Customer> GetAllCustomers()
        {
            return fakeCustomers;
        }

        public Customer GetCustomerUsingGivenName(string GivenName)
        {
            throw new NotImplementedException();
        }
    }
}
