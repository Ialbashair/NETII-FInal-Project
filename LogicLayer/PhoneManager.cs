using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class PhoneManager : IPhoneManager
    {
        private IPhoneAccessor _phoneAccessor = null;
        public PhoneManager() 
        {
            _phoneAccessor = new PhoneAccessor();
        }

        public PhoneManager(IPhoneAccessor phone)
        {
            _phoneAccessor = phone;
        }

        public List<Phone> GetAllPhonesUsingStatus(string status)
        {
            List<Phone> phones = new List<Phone>();

            try
            {
                phones = _phoneAccessor.GetAllPhonesUsingStatus(status);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Phones Found");
            }
            return phones;
        }

        public Phone GetPhoneUsingModel(string model)
        {
            Phone phone = null;
            try
            {
                phone = _phoneAccessor.GetPhoneUsingModel(model);
            }
            catch (Exception)
            {

                throw new ApplicationException("Phone not Found");
            }
            return phone;
        }
    }
}
