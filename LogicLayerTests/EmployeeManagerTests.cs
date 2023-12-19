using DataAccessInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjects;

namespace LogicLayerTests
{
    [TestClass]
    public class EmployeeManagerTests
    {
        IEmployeeManager _employeeManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _employeeManager = new EmployeeManager(new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue()
        {
            // Arrange
            string testString = "newuser";
            string actualHash = "";
            string excpectedHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";

            // Act
            actualHash = _employeeManager.HashSha256(testString);

            // Assert
            Assert.AreEqual(excpectedHash, actualHash);
        }
        [TestMethod]
        public void TestAuthenticateEmployeePassesWithCorrectEmailAndPassword()
        {
            // Arrange
            string email = "tess@company.org";
            string password = "newuser";
            bool expectedResult = true;
            bool actualResults = false;

            // Act 
            actualResults = _employeeManager.AuthenticateEmployee(email, password);

            // Assert
            Assert.AreEqual(expectedResult, actualResults);
        }

        [TestMethod]
        public void TestAuthenticateEmployeeFailsWithIncorrectEmailAndPassword()
        {
            // arrange
            string email = "tess@company.org";
            string password = "newloser";
            bool expectedResult = false;
            bool actualResult = true;

            // act

            actualResult = _employeeManager.AuthenticateEmployee(email, password);

            // assert

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetEmployeeByEmailReturnsCorrectEmployee()
        {
            // arrange
            string email = "tess@company.org";
            int expectedEmployeeID = 1;
            int actualEmployeeID = 0;

            // act
            Employee employee = _employeeManager.GetEmployeeVMByEmail(email);
            actualEmployeeID = employee.EmployeeID;

            // assert
            Assert.AreEqual(expectedEmployeeID, actualEmployeeID);
        }
    }
}
