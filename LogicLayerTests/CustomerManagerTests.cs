using DataAccessInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class CustomerManagerTests
    {
        ICustomerManager _customerManager = null;
        [TestInitialize]
        public void TestSetUp()
        {
            _customerManager = new CustomerManager(new CustomerAccessorFake()) ;
        }
        [TestMethod]
        public void TestGetAllCustomersReturnsCorrectNumberOfCustomers() 
        {
            int expectedCount = 4;
            int actualCount = 0;

            actualCount = _customerManager.GetAllCustomers().Count();

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
