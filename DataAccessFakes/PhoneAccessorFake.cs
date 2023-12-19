using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class PhoneAccessorFake : IPhoneAccessor
    {
        private List<Phone> fakePhones = new List<Phone>();

        public PhoneAccessorFake() 
        {
            fakePhones.Add(new Phone() { 
                PhoneID = 1,
                Make = "Apple",
                Model = "Iphone 10" ,
                MakeYear = 2019,
                Status = "Ready",
                Price = 2003
            });

            fakePhones.Add(new Phone()
            {
                PhoneID = 2,
                Make = "Apple",
                Model = "Iphone 13 PRO MAX",
                MakeYear = 2022,
                Status = "Ready",
                Price = 300
            });

            fakePhones.Add(new Phone()
            {
                PhoneID = 3,
                Make = "Apple",
                Model = "Iphone 11",
                MakeYear = 2020,
                Status = "Ready",
                Price = 200
            });

            fakePhones.Add(new Phone()
            {
                PhoneID = 4,
                Make = "Samsung",
                Model = "Galaxy s9",
                MakeYear = 2019,
                Status = "Sold",
                Price = 4200
            });
        }

        public List<Phone> GetAllPhonesUsingStatus(string status)
        {
            List<Phone> phones = new List<Phone>();

            foreach (Phone phone in fakePhones) 
            {
                if (phone.Status == status)
                {
                    phones.Add(phone);
                }
            }
            return phones;
        }

        public Phone GetPhoneUsingModel(string model)
        {
            Phone phone1 = new Phone();
            foreach (Phone phone in fakePhones) 
            {
                if (phone.Model == model) 
                {
                    phone1 = phone;
                }
            }
            return phone1;
        }
    }
}
