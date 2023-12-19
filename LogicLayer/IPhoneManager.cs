using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IPhoneManager
    {
        List<Phone> GetAllPhonesUsingStatus(string status);
        Phone GetPhoneUsingModel(string model);
    }
}
