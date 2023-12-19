using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class SaleAccessorFake : ISaleAccessor
    {
        private List<Sale> fakeSales = new List<Sale>();
        public SaleAccessorFake() 
        {
            fakeSales.Add(new Sale() { 
                saleID = 1,
                phoneID = 1,
                customerID = 1,
                employeeID = 1,
                dateOfSale = "2019-09-04",
                total = 650.34,
                active= true,
            });
            fakeSales.Add(new Sale()
            {
                saleID = 2,
                phoneID = 2,
                customerID = 2,
                employeeID = 2,
                dateOfSale = "2019-09-04",
                total = 894.34,
                active = true,
            });
            fakeSales.Add(new Sale()
            {
                saleID = 3,
                phoneID = 1,
                customerID = 1,
                employeeID = 1,
                dateOfSale = "2019-09-04",
                total = 623.23,
                active = true,
            });
            fakeSales.Add(new Sale()
            {
                saleID = 4,
                phoneID = 3,
                customerID = 3,
                employeeID = 2,
                dateOfSale = "2019-09-04",
                total = 345.34,
                active = true,
            });

        }
        public int activeteSale(int saleID)
        {
            throw new NotImplementedException();
        }

        public int addSale(int phoneID, int customerID, int employeeID, string dateOfSale, double total, bool active)
        {
            int result = 0;
            int firstCount = fakeSales.Count();
            fakeSales.Add(new Sale() {  saleID = phoneID, customerID = customerID, employeeID = employeeID, dateOfSale = dateOfSale, total = total, active = active });
            int secondCount = fakeSales.Count();
            if (firstCount != secondCount)
            {
                result = 1;
            }
            return result;
        }

        public int deActiveteSale(int saleID)
        {
            throw new NotImplementedException();
        }

        public List<Sale> getActiveSales()
        {
            throw new NotImplementedException();
        }

        public List<Sale> getAllSales()
        {           
            return fakeSales;            
        }

        public Sale getSale(int saleID)
        {
            Sale returnedSale = new Sale();

            foreach (Sale sale in fakeSales)
            {
                if (sale.saleID == saleID)
                {
                    returnedSale = sale;
                }
            }
            return returnedSale;
        }

        public int updateSale(int saleID, int phoneID, int customerID, int employeeID, double total, bool active)
        {
            throw new NotImplementedException();
        }
    }
}
