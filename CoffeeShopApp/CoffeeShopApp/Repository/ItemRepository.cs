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
    public class ItemRepository
    {
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private string commandString;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader reader;
        public bool ExistItem(Item item)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items WHERE Name = '" + item.Name + "' AND Id <>" + item.Id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            sqlConnection.Close();
            return isExist;
        }
        public int InsertItem(Item item)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"INSERT INTO Items (Name, Price) Values ('" + item.Name + "', " + item.Price + ")";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecuted = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return isExecuted;
        }
        public DataTable ShowItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        public int DeleteItem(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Items WHERE Id = " + id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return rowAffected;
        }
        public int UpdateItem(Item item)
        {

            sqlConnection = new SqlConnection(connectionString);
            commandString = @"UPDATE Items SET Name = '" + item.Name + "', Price = " + item.Price + " WHERE Id = " + item.Id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecuted = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return isExecuted;
        }
        public DataTable SearchItem(Item item)
        {

            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items WHERE Name = '" + item.Name + "' OR Price = " + item.Price + "";
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
