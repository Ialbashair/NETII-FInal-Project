using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {

        public int AuthenticateUserwWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int rows = 0;

            //start with a connnection object
            var connection = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_authenticate_employee";

            // create the command object
            var cmd = new SqlCommand(commandText, connection);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to add paramaters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the paramter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                connection.Open();

                rows = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex) { throw ex; }

            finally { connection.Close(); }

            return rows;
        }

        public EmployeeVM SelectEmployeeVMbyEmail(string email)
        {
            EmployeeVM employeeVM = new EmployeeVM();


            var conn = SqlConnectionProvider.GetConnection();


            var cmdText = "sp_select_employee_using_email";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;

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
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.GivenName = reader.GetString(1);
                        employeeVM.FamilyName = reader.GetString(2);
                        employeeVM.Phone = reader.GetString(3);
                        employeeVM.Email = reader.GetString(4);
                        employeeVM.Active = reader.GetBoolean(5);

                    }
                }
                else
                {
                    throw new ArgumentException("Employee not found");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return employeeVM;
        }

        public EmployeeVM SelectEmployeeVMbyGivenName(string GivenName)
        {
            EmployeeVM employeeVM = new EmployeeVM();


            var conn = SqlConnectionProvider.GetConnection();


            var cmdText = "sp_select_employee_using_givenname";

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
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.GivenName = reader.GetString(1);
                        employeeVM.FamilyName = reader.GetString(2);
                        employeeVM.Phone = reader.GetString(3);
                        employeeVM.Email = reader.GetString(4);
                        employeeVM.Active = reader.GetBoolean(5);

                    }
                }
                else
                {
                    throw new ArgumentException("Employee not found");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return employeeVM; 
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<String> roles = new List<String>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_roles_using_employeeID";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // Parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // Parameter values
            cmd.Parameters["@EmployeeID"].Value = employeeID;


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));

                    }
                }

            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }



            return roles;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;


            //start with a connnection object
            var connection = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_passwordHash";

            // create the command object
            var cmd = new SqlCommand(commandText, connection);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to add paramaters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);

            // set the paramter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;

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
                    throw new ArgumentException("Bad email or password");
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
