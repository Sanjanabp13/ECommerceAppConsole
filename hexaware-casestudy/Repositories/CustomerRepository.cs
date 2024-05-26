using System;
using System.Data.SqlClient;
using hexaware_casestudy.Model;
using hexaware_casestudy.Util;
using hexaware_casestudy.Exception;

namespace hexaware_casestudy.Repositories
{
	public class CustomerRepository: ICustomerRepository
	{
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public CustomerRepository(string connectionstring)
		{
            sqlConnection = new SqlConnection(connectionstring);
            cmd = new SqlCommand();
        }

        public bool CreateCustomer(Customer customer)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Insert into customers values(@Name,@Email,@Password,@Role)";
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Password", customer.Password);
            cmd.Parameters.AddWithValue("@Role", customer.UserRole);
            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }

        public bool DeleteCustomer(int customerId)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "delete from customers where customer_Id=@customerId";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
            return true;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Select * from customers where customer_Id=@customerId";
            cmd.Parameters.AddWithValue("@customerId", customerId);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                customer = new Customer()
                {
                    Customer_id = (int)reader["customer_Id"],
                    Name = (string)reader["Name"],
                    Email = (string)reader["Email"]

                };

            }
            cmd.Parameters.Clear();
            sqlConnection.Close();
            return customer;
        }
    }
}

