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
    public class PhoneAccessor : IPhoneAccessor
    {
        public List<Phone> GetAllPhonesUsingStatus(string status)
        {
            List<Phone> phones = new List<Phone>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_all_phones_using_Status";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // Parameters
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 25);

            // Parameter values
            cmd.Parameters["@Status"].Value = status;


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var p = new Phone();
                        p.PhoneID = reader.GetInt32(0);
                        p.Make = reader.GetString(1);
                        p.Model = reader.GetString(2);
                        p.MakeYear = reader.GetInt32(3);
                        p.Price = reader.GetDouble(4);
                        phones.Add(p);

                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
            return phones;
        }

        public Phone GetPhoneUsingModel(string model)
        {
            Phone phone = null;

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_phone_using_model";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // Parameters
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 25);

            // Parameter values
            cmd.Parameters["@Model"].Value = model;


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var p = new Phone();
                        p.PhoneID = reader.GetInt32(0);
                        p.Make = reader.GetString(1);
                        p.Model = reader.GetString(2);
                        p.MakeYear = reader.GetInt32(3);
                        p.Price = reader.GetDouble(4);
                        phone = p;

                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
            return phone;
        }
    }
}
