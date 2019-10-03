using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using CoffeeShopApp.Model;

namespace CoffeeShopApp.Repository
{
    public class OrderRepository
    {
        private SqlDataReader reader;
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        public DataTable GetCustomer()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT Id, Name FROM Customers";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        public DataTable GetItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT Id, Name FROM Items";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        public int GetItemPrice(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT Price FROM Items Where Id = "+ id +"";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            object price = dataTable.Rows[0][0];
            return Convert.ToInt16(price);
        }
        public bool InsertOrder(Order order)
        {
            bool rowAffected = false;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"INSERT INTO Orders (CustomerId, ItemId, Quantity, TotalPrice) Values (" + order.CustomerId + ", " + order.ItemId + ", '" + order.Quantity + "', " + order.TotalPrice + ")";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                    rowAffected = true;
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                rowAffected = false;
            }
            return rowAffected;
        }
        public DataTable ShowOrder()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM OrderInformations";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        public bool UpdateOrder(Order order)
        {
            bool rowAffected = false;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"UPDATE Orders SET CustomerId = " + order.CustomerId + ", ItemId = " + order.ItemId + " , Quantity = " + order.Quantity + ", TotalPrice = " + order.TotalPrice + " WHERE Id = " + order.Id + "";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (isExecuted > 0)
                    rowAffected = true;
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                rowAffected = false;
            }
            return rowAffected;
        }

        public bool DeleteItem(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Orders WHERE Id = " + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (rowAffected > 0)
                return true;
            return false;
        }

        public DataTable SearchOrder(string name, string item, int quantity)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM OrderInformations WHERE CustomerName = '" + name + "' OR ItemName = '" + item + "' OR Quantity = " + quantity + " ";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
