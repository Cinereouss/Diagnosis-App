using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanDoanBenh
{
    class Base
    {
        protected MySqlConnection connection;
        protected MySqlCommand command;
        public Base()
        {
            connection = new MySqlConnection("Server = localhost; Database = chuandoanbenh; Port = 3306; User ID = root; Password =; Character Set=utf8");
        }

        protected void OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Không kết nối được đến cơ sở dữ liệu!" + e);
            }
        }

        protected void CloseConnection()
        {
            connection.Close();
            connection.Dispose();
        }

        protected DataTable ExecuteReaderText(string commandText, params MySqlParameter[] parameters)
        {
            OpenConnection();
            try
            {
                var command = CreateCommand(connection, commandText, CommandType.Text, parameters);
                using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        protected int ExecuteNonQueryText(MySqlConnection connection, string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            OpenConnection();
            var transaction = connection.BeginTransaction();
            try
            {

                var command = CreateCommand(connection, commandText, CommandType.Text, parameters);
                command.Transaction = transaction;
                var result = command.ExecuteNonQuery();

                transaction.Commit();

                return result;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


        protected object ExecuteScalarStoredProcedure(string commandText, params MySqlParameter[] parameters)
        {
            var command = CreateCommand(connection, commandText, CommandType.StoredProcedure, parameters);

            var obj = command.ExecuteScalarAsync();

            return obj;
        }

        protected void ExecuteNonQueryStoredProcedure(string commandText, params MySqlParameter[] parameters)
        {
            var command = CreateCommand(connection, commandText, CommandType.StoredProcedure, parameters);

            command.ExecuteNonQueryAsync();
        }

        protected MySqlCommand CreateCommand(MySqlConnection connection, string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            command = new MySqlCommand();
            command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }



        protected DataTable Select(string table, string[] cols, string[] conditions, MySqlParameter[] parameters)
        {
            try
            {

                var commandText = "SELECT ";
                for (var i = 0; i < cols.Length; i++)
                {
                    commandText += MySqlHelper.EscapeString(cols[i]);
                    if (i < cols.Length - 1) { commandText += ","; }
                }
                commandText += " FROM " + table;
                if (conditions.Length > 0)
                {
                    commandText += " WHERE ";
                    for (var i = 0; i < conditions.Length; i++)
                    {
                        commandText += " " + conditions[i];
                        if (i < conditions.Length - 1)
                            commandText += " AND ";
                    }
                }

                commandText += ";";

                return ExecuteReaderText(commandText, parameters);

            }
            catch (Exception e)
            {
                throw new Exception("Lỗi kết nối dữ liệu!" + e.Message);
            }
        }

        protected int Update(string table, string[] cols, string[] conditions, MySqlParameter[] parameters)
        {
            try
            {
                var commandText = "UPDATE " + table + " SET ";

                for (var i = 0; i < cols.Length; i++)
                {
                    commandText += MySqlHelper.EscapeString(cols[i]);
                    if (i < cols.Length - 1) { commandText += ","; }
                }

                if (conditions.Length > 0)
                {
                    commandText += " WHERE ";
                    for (var i = 0; i < conditions.Length; i++)
                    {
                        commandText += " " + conditions[i];
                    }
                }

                commandText += ";";

                return ExecuteNonQueryText(connection, commandText, CommandType.Text, parameters);

            }
            catch (Exception e)
            {
                throw new Exception("Lỗi kết nối dữ liệu!" + e.Message);
            }
            finally { CloseConnection(); }
        }

        protected int Insert(string table, string[] cols, string[] conditions, MySqlParameter[] parameters)
        {
            try
            {
                var commandText = "INSERT INTO " + table + " ( ";

                for (var i = 0; i < cols.Length; i++)
                {
                    commandText += MySqlHelper.EscapeString(cols[i]);
                    if (i < cols.Length - 1) { commandText += ","; }
                }
                commandText += " ) VALUES ( ";
                for (var i = 0; i < conditions.Length; i++)
                {
                    if (i < conditions.Length - 1)
                        commandText += " " + conditions[i] + ",";
                    if (i == conditions.Length - 1)
                        commandText += " " + conditions[i] + " )";
                }

                commandText += ";";

                return ExecuteNonQueryText(connection, commandText, CommandType.Text, parameters);

            }
            catch (Exception e)
            {
                throw new Exception("Lỗi kết nối dữ liệu! \n" + e.Message);
            }
        }


        protected int Delete(string table, string[] conditions, MySqlParameter[] parameters)
        {
            try
            {
                var commandText = "DELETE FROM " + table + " WHERE";

                for (var i = 0; i < conditions.Length; i++)
                {
                    commandText += " " + MySqlHelper.EscapeString(conditions[i]);
                }

                commandText += ";";

                return ExecuteNonQueryText(connection, commandText, CommandType.Text, parameters);

            }
            catch (Exception e)
            {
                throw new Exception("Lỗi kết nối dữ liệu!" + e.Message);
            }
            finally { CloseConnection(); }
        }
    }
}
