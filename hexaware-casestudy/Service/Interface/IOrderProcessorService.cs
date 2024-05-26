using System;
namespace hexaware_casestudy.Service
{
    public interface IOrderProcessorService
    {
        void CreateProduct();
        void CreateCustomer();
        void DeleteProduct();
        void DeleteCustomer();
        void AddToCart();
        void RemoveFromCart();
        void GetAllFromCart();
        void PlaceOrder();
        void GetOrdersByCustomer();
    }
}


