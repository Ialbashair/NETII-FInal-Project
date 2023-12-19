using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class SaleManager : ISaleManager
    {
        ISaleAccessor _saleAccessor = null;
        
        public SaleManager() 
        {
            _saleAccessor = new SaleAccessor();
        }

        public SaleManager(ISaleAccessor saleAccessor)
        {
            _saleAccessor = saleAccessor;
        }

        public int activeteSale(int saleID)
        {
            throw new NotImplementedException();
        }

        public int addSale(int phoneID, int customerID, int employeeID, string dateOfSale, double total, bool active)
        {
            int rows = 0;
            try
            {
                rows = _saleAccessor.addSale(phoneID, customerID, employeeID, dateOfSale, total, active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Sale could not be made", ex);
            };

            return rows;
        }

        public int deActiveteSale(int saleID)
        {
            throw new NotImplementedException();
        }

        public List<Sale> getActiveSales()
        {
            List<Sale> sales = new List<Sale>();

            try
            {
                sales = _saleAccessor.getActiveSales();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Sales not found", ex);
            }
            return sales;
        }

        public List<Sale> getAllSales()
        {
            List<Sale> sales = new List<Sale>();

            try
            {
                sales = _saleAccessor.getAllSales();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Sales not found", ex);
            }

            return sales;
            
        }

        public Sale getSale(int saleID)
        {
            Sale sale;

            try
            {
                sale = _saleAccessor.getSale(saleID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Sale not Found", ex);
            }

            return sale;
        }

        public int updateSale(int saleID, int phoneID, int customerID, int employeeID, double total, bool active)
        {
            int result = 0;

            try
            {
                result = _saleAccessor.updateSale(saleID, phoneID, customerID, employeeID, total, active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Sale not Updated", ex);
            }
            return result;
        }
    }
}
