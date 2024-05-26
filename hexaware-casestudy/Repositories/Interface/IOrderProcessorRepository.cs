using System;
using hexaware_casestudy.Model;

namespace hexaware_casestudy.Repositories
{
    public interface IOrderProcessorRepository
    {
        
        
        bool AddToCart(int customerId, int productId, int quantity);
        bool RemoveFromCart(int customerId, int productId);
        List<KeyValuePair<Product, int>> GetAllFromCart(int customerId);
        bool PlaceOrderItems(int orderid, List<KeyValuePair<Product, int>> products);
        List<KeyValuePair<Product, int>> GetOrdersByCustomer(int customerId);
    }
}
//{
//	public interface IOrderProcessorRepository
//	{
//        bool CreateProduct(Product product); 
//        bool CreateCustomer(Customer customer);
//        bool DeleteProduct(int productId);
//        bool DeleteCustomer(int customerId);
//        bool AddToCart(Customer customer, Product product, int quantity);
//        bool RemoveFromCart(Customer customer, Product product);
       
//        List<Product> GetAllFromCart(int customer);
//        bool PlaceOrder(Customer customer, Dictionary<Product, int> productsAndQuantities, string shippingAddress);
    
//    List<KeyValuePair<Product, int>> GetOrdersByCustomer(long customerId);
//    }
//}
   

