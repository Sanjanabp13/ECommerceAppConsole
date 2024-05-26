using System;
namespace hexaware_casestudy.Model
{
    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }


        public OrderItems(int orderItemId, int orderId, int productId, int quantity)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
        public OrderItems()
        {
        }

        public override string ToString()
        {
            return $"Order Item ID: {OrderItemId}\nOrder ID: {OrderId}\nProduct ID: {ProductId}\nQuantity: {Quantity}\n";
        }
    }
}

