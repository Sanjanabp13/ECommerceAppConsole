
using System;
namespace hexaware_casestudy.Model
{
    public class Cart
    {
        public int? CartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart() { }
        public Cart(int? cartId, int customerId, int productId, int quantity)
        {
            CartId = cartId;
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
        public override string ToString()
        {
            return $" Cart-Id: {CartId}\nCustomer-Id: {CustomerId}\nProduct-Id: {ProductId}\nQuantity: {Quantity}\n";
        }
    }
}



