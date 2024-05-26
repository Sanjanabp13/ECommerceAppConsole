using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using hexaware_casestudy.Exception;
using hexaware_casestudy.Model;
using hexaware_casestudy.Util;

namespace hexaware_casestudy.Repositories
{
    public class OrderProcessorRepository : IOrderProcessorRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;


        public OrderProcessorRepository(string connectionstring)
        {
            sqlConnection = new SqlConnection(connectionstring);
            cmd = new SqlCommand();
        }
       
        public bool AddToCart(int customerId, int productId, int quantity)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "INSERT INTO Cart VALUES (@CustomerId, @ProductId, @Quantity)";

            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@Quantity", quantity);

            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }


        public bool RemoveFromCart(int customerId, int productId)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Delete from Cart where customer_id = @customerId and product_id =@productId";

            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@productId", productId);

            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }


        public List<KeyValuePair<Product, int>> GetAllFromCart(int customerId)
        {
            List<KeyValuePair<Product, int>> products = new List<KeyValuePair<Product, int>>();
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "SELECT p.Product_id, p.Name, p.Price, p.Description, c.Quantity " +
                "FROM Cart c " +
                "JOIN Products p ON c.Product_id = p.Product_id " +
                "WHERE c.Customer_id = @CustomerId";

            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Product_id = (int)reader["Product_id"],
                    Name = Convert.ToString(reader["Name"]),
                    Price = (decimal)reader["Price"],
                    Description = Convert.ToString(reader["Description"])
                };

                int quantity = (int)reader["Quantity"];

                products.Add(new KeyValuePair<Product, int>(product, quantity));
            }
            reader.Close();
            sqlConnection.Close();
            return products;
        }
        

        public bool PlaceOrderItems(int orderid, List<KeyValuePair<Product, int>> products)
            
        {
           
            cmd.Parameters.Clear();

            try
            {
               
                foreach (var item in products)
                {
                    sqlConnection.Open();
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = "INSERT INTO Order_items VALUES (@order_id,@product_id,@quantity)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@order_id", orderid);
                    //cmd.Parameters.AddWithValue("@ProductId", item.Key.Product_id);
                    //cmd.Parameters.AddWithValue("@Quantity", item.Value);
                    cmd.Parameters.AddWithValue("@product_id",item.Key.Product_id);
                    cmd.Parameters.AddWithValue("@quantity",item.Value);
                    


                    cmd.ExecuteNonQuery();

                    
                    sqlConnection.Close();

                }

                return true;
                

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false);
            }
           
            finally
            {
                sqlConnection.Close();
            }

        }

        public List<KeyValuePair<Product, int>> GetOrdersByCustomer(int customerId)
        {
            List<KeyValuePair<Product, int>> productsAndQuantities = new List<KeyValuePair<Product, int>>();
            
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText =
            
            "SELECT p.Product_id, p.Name, p.Price, p.Description, p.StockQuantity, oi.Quantity " +
                    "FROM Orders o " +
                    "JOIN order_items oi ON o.Order_id = oi.Order_id " +
                    "JOIN Products p ON oi.Product_id = p.Product_id " +
                    "Where o.Customer_id = @CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product
                {
                    Product_id = (int)reader["Product_id"],
                    Name = Convert.ToString(reader["Name"]),
                    Price = (decimal)reader["Price"],
                    Description = Convert.ToString(reader["Description"]),
                    StockQuantity = (int)reader["StockQuantity"]
                };

                int quantity = reader.GetInt32(5);

                productsAndQuantities.Add(new KeyValuePair<Product, int>(product, quantity));
            }
            sqlConnection.Close();
            return productsAndQuantities;
        }
    }
}


