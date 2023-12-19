using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateObjects
{
    // this class will hold extension helpers
    // extention methods must be in a public static class
    public static class ValidationHelpers
    {
        // extention methods must be public, static, and include 'this'
        // as the first parameter, with the type  being extended following

        public static bool IsValidEmail(this string email)
        {
            bool isValid = false;

            if (email.EndsWith("@company.com") && email.Length > 13 && email.Length <= 100)
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool IsValidPassword(this string password)
        {
            bool isValid = false;

            // *** this needs to be done ***
            if (password.Length >= 7)
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
