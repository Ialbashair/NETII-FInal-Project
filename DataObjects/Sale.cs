using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Sale
    {
        public int saleID { get; set; }
        public int phoneID { get; set; }
        public int customerID { get; set; }
        public int employeeID { get; set; }       
        public string dateOfSale { get; set; }
        public double total { get; set; }
        public bool active { get; set; }

    }
}
