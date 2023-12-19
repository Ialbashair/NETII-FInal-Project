using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SaleAccessor : ISaleAccessor
    {

        public int activeteSale(int saleID)
        {
            throw new NotImplementedException();
        }

        public int addSale(int phoneID, int customerID, int employeeID, string dateOfSale, double total, bool active)
        {
            int rows = 0;


            //start with a connnection object
            var connection = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_add_sale";

            // create the command object
            var cmd = new SqlCommand(commandText, connection);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to add paramaters to the command
            cmd.Parameters.Add("@phoneID", SqlDbType.Int);
            cmd.Parameters.Add("@customerID", SqlDbType.Int);
            cmd.Parameters.Add("@employeeID", SqlDbType.Int);
            cmd.Parameters.Add("@dateOfSale", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@total", SqlDbType.Float);
            cmd.Parameters.Add("@acitve", SqlDbType.Bit);

            // set the paramter values
            cmd.Parameters["@phoneID"].Value = phoneID;
            cmd.Parameters["@customerID"].Value = customerID;
            cmd.Parameters["@employeeID"].Value = employeeID;
            cmd.Parameters["@dateOfSale"].Value = dateOfSale;
            cmd.Parameters["@total"].Value = total;
            cmd.Parameters["@acitve"].Value = active;

            // now that everything is set up we can open the connection
            // and excute the command in a try-catch-finally statememnt

            try
            {
                // open the connection
                connection.Open();

                // an update is executed nonquery
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    // create a failed update as an exception
                    throw new ArgumentException("Invalid Inputs");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { connection.Close(); }
            return rows;
        }

        public int deActiveteSale(int saleID)
        {
            throw new NotImplementedException();
        }

        public List<Sale> getActiveSales()
        {
            List<Sale> sales = new List<Sale>();


            var conn = SqlConnectionProvider.GetConnection();


            var cmdText = "sp_select_active_sales";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;



            try
            {
                // open the connection
                conn.Open();

                // EXCUTE
                var reader = cmd.ExecuteReader();

                // Porccess results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sale = new Sale();
                        sale.saleID = reader.GetInt32(0);
                        sale.phoneID = reader.GetInt32(1);
                        sale.customerID = reader.GetInt32(2);
                        sale.employeeID = reader.GetInt32(3);
                        sale.dateOfSale = reader.GetString(4);
                        sale.total = reader.GetDouble(5);
                        sale.active = reader.GetBoolean(6);
                        sales.Add(sale);
                    }
                }
                else
                {
                    throw new ArgumentException("Sale not Found");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return sales;
        }

        public List<Sale> getAllSales()
        {
            List<Sale> sales = new List<Sale>();


            var conn = SqlConnectionProvider.GetConnection();


            var cmdText = "sp_select_all_sales";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;           

           

            try
            {
                // open the connection
                conn.Open();

                // EXCUTE
                var reader = cmd.ExecuteReader();

                // Porccess results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sale = new Sale();
                        sale.saleID = reader.GetInt32(0);
                        sale.phoneID = reader.GetInt32(1);
                        sale.customerID = reader.GetInt32(2);
                        sale.employeeID = reader.GetInt32(3);
                        sale.dateOfSale = reader.GetString(4);
                        sale.total = reader.GetDouble(5);
                        sale.active = reader.GetBoolean(6);
                        sales.Add(sale);
                    }
                }
                else
                {
                    throw new ArgumentException("Sale not Found");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return sales;
        }

        public Sale getSale(int saleID)
        {
            Sale sale = new Sale();


            var conn = SqlConnectionProvider.GetConnection();


            var cmdText = "sp_select_sale_using_saleid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@saleID", SqlDbType.Int);

            cmd.Parameters["@saleID"].Value = saleID;

            try
            {
                // open the connection
                conn.Open();

                // EXCUTE
                var reader = cmd.ExecuteReader();

                // Porccess results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        sale.saleID = reader.GetInt32(0);
                        sale.phoneID = reader.GetInt32(1);
                        sale.customerID = reader.GetInt32(2);
                        sale.employeeID = reader.GetInt32(3); 
                        sale.dateOfSale = reader.GetString(4);
                        sale.total = reader.GetInt32(5);
                        sale.active = reader.GetBoolean(6);

                    }
                }
                else
                {
                    throw new ArgumentException("Sale not Found");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return sale;
        }

        public int updateSale(int saleID, int phoneID, int customerID, int employeeID, double total, bool active)
        {
            int rows = 0;


            //start with a connnection object
            var connection = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_Sale";

            // create the command object
            var cmd = new SqlCommand(commandText, connection);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to add paramaters to the command
            cmd.Parameters.Add("@saleID", SqlDbType.Int);
            cmd.Parameters.Add("@phoneID", SqlDbType.Int);
            cmd.Parameters.Add("@customerID", SqlDbType.Int);
            cmd.Parameters.Add("@employeeID", SqlDbType.Int);
            cmd.Parameters.Add("@total", SqlDbType.Float);
            cmd.Parameters.Add("@active", SqlDbType.Bit);

            // set the paramter values
            cmd.Parameters["@saleID"].Value = saleID;
            cmd.Parameters["@phoneID"].Value = phoneID;
            cmd.Parameters["@customerID"].Value = customerID;
            cmd.Parameters["@employeeID"].Value = employeeID;        
            cmd.Parameters["@total"].Value = total;
            cmd.Parameters["@active"].Value = active;

            // now that everything is set up we can open the connection
            // and excute the command in a try-catch-finally statememnt

            try
            {
                // open the connection
                connection.Open();

                // an update is executed nonquery
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    // create a failed update as an exception
                    throw new ArgumentException("Invalid Inputs");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { connection.Close(); }
            return rows;
        }
    }
}
