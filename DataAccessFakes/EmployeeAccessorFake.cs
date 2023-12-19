using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class EmployeeAccessorFake : IEmployeeAccessor
    {

        private List<EmployeeVM> fakeEmployees = new List<EmployeeVM>();
        private List<string> passwordHashes = new List<string>();

        public EmployeeAccessorFake()
        {
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 1,
                GivenName = "Tess",
                FamilyName = "Date",
                Phone = "1234567890",
                Email = "tess@company.org",
                Active = true,
                Roles = new List<string>()
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 2,
                GivenName = "Bess",
                FamilyName = "Date",
                Phone = "1233567890",
                Email = "Bess@company.org",
                Active = true,
                Roles = new List<string>()
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 3,
                GivenName = "Jess",
                FamilyName = "Date",
                Phone = "1224567890",
                Email = "Jess@company.org",
                Active = true,
                Roles = new List<string>()
            });

            passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordHashes.Add("badhash");
            passwordHashes.Add("badhash");

            fakeEmployees[0].Roles.Add("TestRole1");
            fakeEmployees[0].Roles.Add("TestRole2");
        }
        public int AuthenticateUserwWithEmailAndPasswordHash(string email, string passowrdHash)
        {
            int numAuthenticated = 0;
            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (passwordHashes[i] == passowrdHash && fakeEmployees[i].Email == email)
                {
                    numAuthenticated++;
                }
            }
            return numAuthenticated; 
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;
            //check for employee records in the fake data
            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (passwordHashes[i] == passwordHash && fakeEmployees[i].Email == email)
                {
                    numAuthenticated++;
                }
            }
            return numAuthenticated; // should be one or zero.
        }


        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            foreach (var fakeEmployee in fakeEmployees)
            {
                if (fakeEmployee.EmployeeID == employeeID)
                {
                    roles = fakeEmployee.Roles;
                    break;
                }
            }

            return roles;
        }

        public int UpdatePasswordHas(string email, string OldPasswordHash, string NewPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (fakeEmployees[i].Email == email)
                {
                    if (passwordHashes[i] == OldPasswordHash)
                    {
                        passwordHashes[i] = NewPasswordHash;
                        rows++;
                    }
                }
            }
            if (rows != 1) // no one found
            {
                throw new ApplicationException("Bad email or password");
            }
            return rows;

        }

        public EmployeeVM SelectEmployeeVMbyEmail(string email)
        {
            EmployeeVM emp = null;

            foreach (var fakeEmployee in fakeEmployees)
            {
                if (fakeEmployee.Email == email)
                {
                    emp = fakeEmployee;
                }
            }
            if (emp == null) // no one found
            {
                throw new ApplicationException("Bad Email");
            }
            return emp;
        }

        public int UpdatePasswordHash(string email, string oldPassowrdHash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }

        public EmployeeVM SelectEmployeeVMbyGivenName(string GivenName)
        {
            throw new NotImplementedException();
        }
    }
}

