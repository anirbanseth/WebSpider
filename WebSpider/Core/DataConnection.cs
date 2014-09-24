using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;
using System.Data;

namespace WebSpider.Core
{
    public class DataConnection
    {
        private OleDbConnection connection;
        private OleDbCommand command;


        public DataConnection()
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["connString"].ToString();
                OleDbConnection connection = new OleDbConnection(connectionString);
            }
            catch { }
        }

        public DataTable ExecuteQuery(String sqlString)
        {
            try
            {
                connection.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlString, connection);
                DataTable oDataTable = new DataTable();
                dataAdapter.Fill(oDataTable);
                connection.Close();
                return oDataTable;
            }
            catch
            {
                return null;
            }
        }

        public int ExecuteNonQuery(String sqlString)
        {
            try {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlString;
                command.Connection = connection;
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
            catch
            { 
                return 0; 
            }
        }
    }
}
