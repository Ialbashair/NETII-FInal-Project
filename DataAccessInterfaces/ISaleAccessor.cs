using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface ISaleAccessor
    {
        Sale getSale(int saleID);

        List<Sale> getAllSales();

        List<Sale> getActiveSales();

        int activeteSale(int saleID);

        int deActiveteSale(int saleID);

        int updateSale(int saleID, int phoneID, int customerID, int employeeID, double total, bool active);

        int addSale(int phoneID, int customerID, int employeeID, string dateOfSale, double total, bool active);


    }
}
