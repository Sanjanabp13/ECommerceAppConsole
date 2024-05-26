using System;
using System.Data.SqlClient;
using hexaware_casestudy.Model;
using hexaware_casestudy.Util;

namespace hexaware_casestudy.Repositories
{
	public class OrderRepository:IOrderRepository
	{
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public OrderRepository(string connectionstring)
		{
            sqlConnection = new SqlConnection(connectionstring);
            cmd = new SqlCommand();
        }

        public int CreateOrder(Order order)
        {
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "INSERT INTO Orders OUTPUT INSERTED.Order_id VALUES (@CustomerId,@OrderDate,@total_price, @ShippingAddress)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            //cmd.Parameters.AddWithValue("@ProductId", item.Key.Product_id);
            //cmd.Parameters.AddWithValue("@Quantity", item.Value);
            cmd.Parameters.AddWithValue("@total_price", order.TotalPrice);
            cmd.Parameters.AddWithValue("@shippingAddress", order.ShippingAddress);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);


            int orderId = (int)cmd.ExecuteScalar();
            

           
            sqlConnection.Close();
            return orderId;
        }

       

        public bool UpdateOrder(int order_id,decimal totalprice)
        {
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "Update Orders set total_price=@total_price where order_id=@order_id ";
            cmd.Parameters.Clear();
            cmd.Parameters["@order_id"].Value = order_id;
            cmd.Parameters["@total_price"].Value = totalprice;
            


            int count = cmd.ExecuteNonQuery();



            //how many rows are affected is said , if no rows are affected then returns -1
            sqlConnection.Close();
            return count == -1 ? false : true;
        }
    }
}

