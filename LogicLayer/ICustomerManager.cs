﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICustomerManager
    {
        List<Customer> GetAllCustomers();

        Customer GetCustomerByGivenName(string GivenName);
    }
}
