using System;
using System.Data.SqlClient;
using hexaware_casestudy.Exception;
using hexaware_casestudy.Model;
using hexaware_casestudy.Util;

namespace hexaware_casestudy.Repositories
{

	public class ProductRepository:IProductRepository
	{
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public ProductRepository(string connectionstring)
		{
            sqlConnection = new SqlConnection(connectionstring);
            cmd = new SqlCommand();
        }

        public bool CreateProduct(Product newProduct)
        {
            if (string.IsNullOrEmpty(newProduct.Name))
            {
                throw new ProductNotFoundException("product name is required");
               
                
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.CommandText = "Insert into products values(@Name,@Price,@Description,@stockQuantity)";
                cmd.Parameters.AddWithValue("@Name", newProduct.Name);
                cmd.Parameters.AddWithValue("@Price", newProduct.Price);
                cmd.Parameters.AddWithValue("@Description", newProduct.Description);
                cmd.Parameters.AddWithValue("@stockQuantity", newProduct.StockQuantity);
                int status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return status > 0 ? true : false;
            }
            
        }

        public bool DeleteProduct(int productId)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "delete from products where product_Id=@productId";
            cmd.Parameters.AddWithValue("@productId", productId);
            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;

            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Select * from Products where Product_Id=@productId";
            cmd.Parameters.AddWithValue("@productId", productId);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                product = new Product()
                {
                    Product_id = (int)reader["Product_Id"],
                    Name = (string)reader["Name"],
                    Price = (decimal)reader["price"],
                    Description = (string)reader["description"],
                    StockQuantity = (int)reader["stockquantity"]
                };

            }
            cmd.Parameters.Clear();
            sqlConnection.Close();
            return product;


        }
    }
}

