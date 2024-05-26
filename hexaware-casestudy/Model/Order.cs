using System;
namespace hexaware_casestudy.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }

        public Order()
        {

        }
        public Order(int orderId, int customerId, DateTime orderDate, decimal totalPrice, string shippingAddress)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
        }
        public override string ToString()
        {
            return $"Order ID: {OrderId}\nCustomer ID: {CustomerId}\nOrder Date: {OrderDate}\nTotal Price: {TotalPrice:C}\nShipping Address: {ShippingAddress}\n";
        }
    }
}



