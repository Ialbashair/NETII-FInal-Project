using DataAccessFakes;
using DataAccessInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class SaleManagerTests
    {
        ISaleManager _saleManager = null;

        [TestInitialize]
        public void TestSetUp() 
        {
            _saleManager = new SaleManager(new SaleAccessorFake());
        }

        [TestMethod]
        public void TestGetSaleReturnsCorrectSale() 
        {
            int saleID = 1;
            double expectedTotal = 650.34;
            double actualTotal = 0.0;

            actualTotal = _saleManager.getSale(saleID).total;

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void TestGetAllSalesReturnsCorrectNumberOfSales()
        {
            int expectedSales = 4;
            int actualSales = 0;

            actualSales = _saleManager.getAllSales().Count();

            Assert.AreEqual(actualSales, expectedSales);
        }

        [TestMethod]
        public void TestAddSalesReturnsCorrectBoolValue()
        {
            int phoneID = 100000;
            int customerId = 100001;
            int employeeId = 100001;
            string dateOfSale = "2018-09-08";
            double total = 898.09;
            bool active = true;
            int expectedValue = 1;
            int actualValue;

            actualValue = _saleManager.addSale(phoneID, customerId, employeeId, dateOfSale, total, active);

            Assert.AreEqual(expectedValue, actualValue);
        }

    }
}
