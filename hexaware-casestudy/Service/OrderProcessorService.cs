using System;
using hexaware_casestudy.Exception;
using hexaware_casestudy.Model;
using hexaware_casestudy.Repositories;
using hexaware_casestudy.Util;

namespace hexaware_casestudy.Service
{
    public class OrderProcessorService : IOrderProcessorService
    {
        readonly IOrderProcessorRepository orderProcessorRepo;
        readonly ICustomerRepository customerRepo;
        readonly IProductRepository productRepository;
        readonly IOrderRepository orderRepository;



        public DateTime orderDate { get; set; }

        public OrderProcessorService(string connectionstring)
        {
            orderProcessorRepo = new OrderProcessorRepository(connectionstring);
            customerRepo = new CustomerRepository(connectionstring);
            productRepository = new ProductRepository(connectionstring);
            orderRepository = new OrderRepository(connectionstring);
        }


        public void CreateProduct()
        {

            Console.Write("Enter the product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the price of the product: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter the description of the product: ");
            string description = Console.ReadLine();
            Console.Write("Enter the stock quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Product newProduct = new Product() {Name= name, Price=price,Description=description, StockQuantity=stockQuantity };


            bool addnewProductStatus = productRepository.CreateProduct(newProduct);
            if (addnewProductStatus)
            {
                Console.WriteLine("Product created Successfully/n");
            }
            else
            {
                Console.WriteLine("Failed to create product/n");
            }
        }
        public void CreateCustomer()
        {

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your email id: ");
            string emailId = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            Console.Write("Register as User (yes/no) if yes youll be registered as user , if no youll be registered as admin:\n");

            string role = Console.ReadLine().ToLower();



            Customer newCustomer = new Customer() { Name = name, Email = emailId, Password = password, UserRole = role=="yes"?"Customer":"Admin"};


            bool addnewCustomerStatus = customerRepo.CreateCustomer(newCustomer);
            if (addnewCustomerStatus)
            {
                Console.WriteLine("Added new customer!!!\n");
                

            }
           
            else
            {
                Console.WriteLine("Failed to add new customer\n");
            }
        }

        public void DeleteProduct()
        {
            Console.WriteLine("Enter the product's ID:\n");
            int prodId = int.Parse(Console.ReadLine());
            bool status = productRepository.DeleteProduct(prodId);
            if (status)
            {
                Console.WriteLine("Deleted the specified product\n");
            }
            else
            {
                Console.WriteLine("Failed to delete the specified product\n");
            }
        }

        public void DeleteCustomer()
        {
            Console.WriteLine("Enter the customer's ID:\n");
            int customerId = int.Parse(Console.ReadLine());
            bool status = customerRepo.DeleteCustomer(customerId);
            if (status)
            {
                Console.WriteLine("Deleted the specified customer\n");
            }
            else
            {
                throw new CustomerNotFoundException("Failed to delete the specified customer because customer id is not found!\n");
            }
        }
        public void AddToCart()
        {
            Console.WriteLine("Enter the customer id:\n");
            int customerId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the product id:\n");
            int productId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity:\n");
            int quantity = int.Parse(Console.ReadLine());

            bool status = orderProcessorRepo.AddToCart(customerId, productId, quantity);

            if (status)
            {
                Console.WriteLine("Added to cart!!\n");
            }
            else
            {
                Console.WriteLine("Failed to add to cart\n");
            }
        }


        public void RemoveFromCart()
        {
            Console.WriteLine("Enter the customer id:\n");
            int customerId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the product id:\n");
            int productId = int.Parse(Console.ReadLine());
            try
            {
                bool status = orderProcessorRepo.RemoveFromCart(customerId, productId);

                if (status)
                {
                    Console.WriteLine("Removed from cart!!\n");
                }
                else
                {
                    throw new ProductNotFoundException("Product not found in the cart\n");
                }
            }
            catch (ProductNotFoundException pnfex)
            { Console.WriteLine(pnfex.Message); }
        }

        public void GetAllFromCart()
        {

            Console.Write("Enter Customer ID: \n");
            int Customer_id = int.Parse(Console.ReadLine());



            List<KeyValuePair<Product, int>> productsInCart = orderProcessorRepo.GetAllFromCart(Customer_id);


            if (productsInCart.Count > 0)
            {
                Console.WriteLine("Products in the cart:\n");
                foreach (var item in productsInCart)
                {
                    Console.WriteLine($"Product Name: {item.Key.Name}\t Product price:{item.Key.Price}\t Product quantity:{item.Value}\n");
                }
            }
            else
            {
                Console.WriteLine("The cart is empty.\n");
            }

        }


        public void PlaceOrder()
        {

            List<KeyValuePair<Product, int>> items = new List<KeyValuePair<Product, int>>();
            string addMore;
            int quantity;

            Customer customer = new Customer();
            
            Console.Write("Enter Customer ID: \n");
            customer.Customer_id = int.Parse(Console.ReadLine());
            Console.Write("Enter Customer Name: \n");
            customer.Name = Console.ReadLine();
            Console.Write("Enter Shipping Address: \n");
            string shippingAddress = Console.ReadLine();
            decimal total_amount = 0;
            do
            {
                Product product = new Product();
                Console.Write("Enter Product ID: \n");
                product.Product_id = int.Parse(Console.ReadLine());
                Console.Write("Enter Product Name: \n");
                product.Name = Console.ReadLine();
                Console.Write("Enter Product Price: \n");
                product.Price = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter Quantity:\n ");
                quantity = int.Parse(Console.ReadLine());
                total_amount = total_amount + (quantity * product.Price);
                KeyValuePair<Product, int> newKey = new KeyValuePair<Product, int>(product, quantity);
                items.Add(newKey);

                Console.Write("Do you want to add another product? (yes/no): \n");
                addMore = Console.ReadLine().ToLower();
            }
            while (addMore.Equals("yes"));
            if(customerRepo.GetCustomerById(customer.Customer_id) == null)
            {
                throw new CustomerNotFoundException($"Customer with ID {customer.Customer_id} not found.\n");
            }
            foreach (var item in items)
            {
                if (productRepository.GetProductById(item.Key.Product_id) == null)
                {
                    throw new ProductNotFoundException($"Product with ID {item.Key.Product_id} not found.\n");
                }

            }
            Order order = new Order()
            {
                CustomerId = customer.Customer_id,
                ShippingAddress = shippingAddress,
                OrderDate = DateTime.Now,
                TotalPrice = total_amount
            };
            order.OrderId = orderRepository.CreateOrder(order);
                

            var success = orderProcessorRepo.PlaceOrderItems(order.OrderId, items);

            if (success)
            {
                Console.WriteLine($"Order placed successfully! Total amount: ${total_amount}\n");
            }
            else
            {
                Console.WriteLine("Failed to place order.\n");
            }
        }
        public void GetOrdersByCustomer()
        {
            Console.Write("Enter Customer ID:\n ");
            int customerId = int.Parse(Console.ReadLine());


            List<KeyValuePair<Product, int>> orders = orderProcessorRepo.GetOrdersByCustomer(customerId);

            try
            {
                if (orders.Count == 0)
                {
                    throw new OrderNotFoundException("No orders found for this customer!\n");
                }
                
                Console.WriteLine("Orders for Customer ID: \n" + customerId);
                foreach (var order in orders)
                {
                    Console.WriteLine($"Product: {order.Key.Name}, Quantity: {order.Value}\n");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


                }

    }
}
#region
//{
//	internal class OrderProcessorService:IOrderProcessorService
//	{

//            readonly IOrderProcessorRepository orderProcessorRepo;

//            public OrderProcessorService()
//            {
//                orderProcessorRepo = new OrderProcessorRepository();
//            }


//        public void CreateProduct()
//        {

//            Console.Write("Enter the product name: ");
//            string name = Console.ReadLine();
//            Console.Write("Enter the price of the product: ");
//            decimal price = decimal.Parse(Console.ReadLine());
//            Console.Write("Enter the description of the product: ");
//            string description = Console.ReadLine();
//            Console.Write("Enter the stock quantity: ");
//            long stockQuantity = long.Parse(Console.ReadLine());

//            Product newProduct = new Product(null, name, price, description, stockQuantity);


//            bool addnewProductStatus = orderProcessorRepo.CreateProduct(newProduct);
//            if (addnewProductStatus)
//            {
//                Console.WriteLine("Product added Successfully");
//            }
//            else
//            {
//                Console.WriteLine("Failed to add product");
//            }
//        }
//              public void CreateCustomer()
//            {

//                Console.Write("Enter your name: ");
//                string name = Console.ReadLine();
//                Console.Write("Enter your email id: ");
//                string emailId = Console.ReadLine();
//                Console.Write("Enter your password: ");
//                string password = Console.ReadLine();


//                Customer newCustomer = new Customer(null, name, emailId, password);


//                bool addnewCustomerStatus = orderProcessorRepo.CreateCustomer(newCustomer);
//                if (addnewCustomerStatus)
//                {
//                    Console.WriteLine("Added new customer!!!");
//                }
//                else
//                {
//                    Console.WriteLine("Failed to add a new customer");
//                }

//            }
//             public void DeleteProduct()
//            {
//                Console.WriteLine("Enter the product's ID:");
//                int prodId = int.Parse(Console.ReadLine());
//                bool status = orderProcessorRepo.DeleteProduct(prodId);
//                if (status)
//                {
//                    Console.WriteLine("Deleted the specified product");
//                }
//                else
//                {
//                    Console.WriteLine("Failed to delete the specified product");
//                }
//            }

//             public void DeleteCustomer()
//            {
//                Console.WriteLine("Enter the customer's ID:");
//                int customerId = int.Parse(Console.ReadLine());
//                bool status = orderProcessorRepo.DeleteCustomer(customerId);
//                if (status)
//                {
//                    Console.WriteLine("Deleted the specified customer");
//                }
//                else
//                {
//                throw new CustomerNotFoundException("Failed to delete the specified customer because customer id");
//                }
//            }
//             public void AddToCart()
//            {
//            Console.WriteLine("Enter the customer id:");
//            long customerId = long.Parse(Console.ReadLine());
//            Console.WriteLine("Enter the product id:");
//            long? productId = long.Parse(Console.ReadLine());
//            Console.WriteLine("Enter the quantity:");
//            int quantity = int.Parse(Console.ReadLine());

//            bool status = orderProcessorRepo.AddToCart(customerId, productId, quantity);

//            if (status)
//            {
//                Console.WriteLine("Added to cart!!");
//            }
//            else
//            {
//                Console.WriteLine("Failed to add to cart");
//            }



//        }


//            public void RemoveFromCart()
//            {


//            }


//            public void GetAllFromCart()
//            {


//            }


//            public void PlaceOrder()
//            {


//            }
//            public void GetOrdersByCustomer()
//                {


//                }

//        }
//}
#endregion

