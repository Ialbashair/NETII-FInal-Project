using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IEmployeeAccessor
    {
        int AuthenticateUserwWithEmailAndPasswordHash(string email, string passowrdHash);

        EmployeeVM SelectEmployeeVMbyEmail(string email);

        EmployeeVM SelectEmployeeVMbyGivenName(string GivenName);

        List<string> SelectRolesByEmployeeID(int employeeID);

        int UpdatePasswordHash(string email, string oldPassowrdHash, string newPasswordHash);


        
    }
}
