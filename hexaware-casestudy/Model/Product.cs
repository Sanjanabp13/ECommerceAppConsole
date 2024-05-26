using System;
namespace hexaware_casestudy.Model
{
    public class Product
    {
        public int Product_id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public Product()
        {

        }
        public Product(int product_id,
       string name,
       decimal price,
       string description,
       int stockQuantity)
        {
            Product_id = product_id;
            Name = name;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }
        public override string ToString()
        {
            return $"Product ID: {Product_id}\nName: {Name}\nPrice: {Price:C}\nDescription: {Description}\nStock Quantity: {StockQuantity}\n";
        }
    }

}


