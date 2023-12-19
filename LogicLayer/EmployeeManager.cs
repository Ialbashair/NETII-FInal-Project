using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor = null;

        public EmployeeManager() 
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        } 

        public bool AuthenticateEmployee(string email, string password)
        {
            bool result = false;

            password = HashSha256(password);

            result = (1 == _employeeAccessor.AuthenticateUserwWithEmailAndPasswordHash(email, password));

            return result;
        }
        

        public EmployeeVM GetEmployeeVMByEmail(string email)
        {
            EmployeeVM employeeVM = null;

            try
            {
                employeeVM = _employeeAccessor.SelectEmployeeVMbyEmail(email);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Employee not found", ex);
            }
            return employeeVM;
        }

        public List<string> GetRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            roles.Add("");
            roles.Add("");

            try
            {
                roles = _employeeAccessor.SelectRolesByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return roles;
        }

        public string HashSha256(string source)
        {
            string hashValue = "";

            // hash functions run at the bits and bytes level
            // so we create a byte array
            byte[] data;

            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                // hash the source string
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // create an output stringbuilder object
            var s = new StringBuilder();

            // loop through the byte array and build a return string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2")); // outputs the byte as two hex digits
            }

            hashValue = s.ToString();
            return hashValue;
        }

        public EmployeeVM LoginEmployee(string email, string password)
        {
            EmployeeVM employeeVM = null;

            try
            {
                if (AuthenticateEmployee(email, password))
                {
                    employeeVM = GetEmployeeVMByEmail(email);
                    employeeVM.Roles = GetRolesByEmployeeID(employeeVM.EmployeeID);
                }
                else // throw an execption
                {
                    throw new ArgumentException("Bad email or Password");

                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Authentication Failed", ex);
            }

            return employeeVM;
        }

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            {
                bool results = false;

                oldPassword = HashSha256(oldPassword);
                newPassword = HashSha256(newPassword);

                try
                {
                    _employeeAccessor.UpdatePasswordHash(email, oldPassword, newPassword);

                    results = true;
                }
                catch (Exception ex)
                {

                    throw new ArgumentException("Email or Password not found.", ex);
                }


                return results;
            }
        }

        public EmployeeVM SelectEmployeeVMbyGivenName(string GivenName)
        {
            EmployeeVM employee = null;

            try
            {
                employee = _employeeAccessor.SelectEmployeeVMbyGivenName(GivenName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Employee not Found" , ex);
            }
            return employee;
        }
    }
}
