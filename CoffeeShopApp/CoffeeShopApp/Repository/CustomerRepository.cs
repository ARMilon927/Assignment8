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
    public class CustomerRepository
    {
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private SqlDataReader reader;
        public bool ExistCustomer(Customer customer)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers WHERE Name = '" + customer.Name + "' AND Id <>" + customer.Id + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            sqlConnection.Close();
            return isExist;
        }
        public int InsertCustomer(Customer customer)        {

            sqlConnection = new SqlConnection(connectionString);
            commandString = @"INSERT INTO Customers (Name, Contact, Address) Values ('" + customer.Name + "', '" + customer.Contact + "', '" + customer.Address + "')";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecuted = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return isExecuted;
        }
        public DataTable ShowCustomer()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        public bool UpdateCustomer(Customer customer)
        {
            bool rowAffected = false;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"UPDATE Customers SET Name = '" + customer.Name + "', Contact = '" + customer.Contact + "', Address = '" + customer.Address + "' WHERE Id = " + customer.Id + "";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (isExecuted > 0)
                    rowAffected = true;
            }
            catch (Exception e)
            {
                rowAffected = false;
            }
            return rowAffected;
        }

        public bool DeleteCustomer(int id)
        {
            bool rowAffected = false;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"DELETE FROM Customers WHERE Id = " + id + "";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (isExecuted > 0)
                    rowAffected = true;
            }
            catch (Exception e)
            {
                rowAffected = false;
            }
            return rowAffected;
        }
        public DataTable SearchCustomer(Customer customer)
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers WHERE Name = '" + customer.Name + "' OR Contact = '" + customer.Contact + "' OR Address = '" + customer.Address + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
