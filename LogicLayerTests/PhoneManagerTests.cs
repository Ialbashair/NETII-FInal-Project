using DataAccessFakes;
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
    public class PhoneManagerTests
    {
        IPhoneManager _phoneManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _phoneManager = new PhoneManager(new PhoneAccessorFake());
        }

        [TestMethod]
        public void TestGetAllPhonesUsingStatusReturnsCorrectList()
        {
            string status = "Ready";
            int expectedPhoneCount = 3;
            int actualPhoneCount = 0;
            
            List<DataObjects.Phone> phones = _phoneManager.GetAllPhonesUsingStatus(status);
            actualPhoneCount = phones.Count();

            Assert.AreEqual(expectedPhoneCount, actualPhoneCount);
        }

        [TestMethod]
        public void TestGetPhoneUsingModelReturnsCorrectPhone()
        {
            string model = "Iphone 11";
            int expctedPhoneID = 3;
            int actualPhoneID = 0;

            actualPhoneID = _phoneManager.GetPhoneUsingModel(model).PhoneID;

            Assert.AreEqual(actualPhoneID, expctedPhoneID);
        }
    }
}
