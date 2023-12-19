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

    public class CustomerAccessor : ICustomerAccessor
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_all_customers";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;      

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var c = new Customer();
                        c.CustomerID = reader.GetInt32(0);
                        c.GivenName = reader.GetString(1);
                        c.FamilyName = reader.GetString(2);
                        c.Email = reader.GetString(3);
                        c.Phone = reader.GetString(4);
                        customers.Add(c);

                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
            return customers;
        }

        public Customer GetCustomerUsingGivenName(string GivenName)
        {
           Customer customer = new Customer();


            var conn = SqlConnectionProvider.GetConnection();


            var cmdText = "sp_select_customer_using_givenname";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 100);

            cmd.Parameters["@GivenName"].Value = GivenName;

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
                        customer.CustomerID = reader.GetInt32(0);
                        customer.GivenName = reader.GetString(1);
                        customer.FamilyName = reader.GetString(2);
                        customer.Email = reader.GetString(3);
                        customer.Phone = reader.GetString(4);                      
                    }
                }
                else
                {
                    throw new ArgumentException("Customer not found");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return customer;
        }
    }
}
