using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DateObjects;

namespace LogicLayer
{
    public interface IEmployeeManager
    {
        // Helper Methods, but public for reuse
        EmployeeVM LoginEmployee(string email, string password);

        bool AuthenticateEmployee(string email, string password);

        string HashSha256(string source);

        EmployeeVM GetEmployeeVMByEmail(String email);

        List<string> GetRolesByEmployeeID(int emplyoeeID);

        bool ResetPassword(string email, string oldPassword, string newPassword);

        EmployeeVM SelectEmployeeVMbyGivenName(string GivenName);



    }
}