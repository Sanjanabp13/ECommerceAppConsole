using System;
using hexaware_casestudy.Service;
using hexaware_casestudy.Util;

namespace hexaware_casestudy
{
    public class EcommerceApplication
    {


        readonly IOrderProcessorService _orderProcessorServices;

        public EcommerceApplication()
        {
            _orderProcessorServices = new OrderProcessorService(ConnectionUtil.GetConnectionString());
        }
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("WELCOME TO ECOMMERCE APPLICATION \n Here are the options from which you can choose to perform");
                Console.WriteLine("1.Register Customer");
                
                Console.WriteLine("2.Create Product");
                Console.WriteLine("3.Delete Product");
                Console.WriteLine("4.Delete Customer");
                Console.WriteLine("5.Add to cart");
                Console.WriteLine("6.Remove from cart");
                Console.WriteLine("7.Get all products from cart");
                Console.WriteLine("8.Place order");
                Console.WriteLine("9.Get orders ordered by customer");
                Console.WriteLine("10.Exit");
                Console.Write("Select an Option: ");
                int userOption = int.Parse(Console.ReadLine());
                switch (userOption)
                {
                    case 1:
                        _orderProcessorServices.CreateCustomer();
                        
                        break;
                    case 2:
                        _orderProcessorServices.CreateProduct();
                        break;
                    case 3:
                        _orderProcessorServices.DeleteProduct();
                        break;
                    case 4:
                        _orderProcessorServices.DeleteCustomer();
                        break;
                    case 5:
                        _orderProcessorServices.AddToCart();
                        break;
                    case 6:
                        _orderProcessorServices.RemoveFromCart();
                        break;
                    case 7:
                        _orderProcessorServices.GetAllFromCart();
                        break;
                    case 8:
                        _orderProcessorServices.PlaceOrder();
                        break;
                    case 9:
                        _orderProcessorServices.GetOrdersByCustomer();
                        break;
                    case 10:
                        Console.WriteLine("Thank you for shopping !!!");
                        Environment.Exit(0);
                        break;
                    default:
                        if (userOption > 10)
                        {
                            Console.WriteLine("Choose from the given options");
                        }
                        break;
                }
                

            }
        }
    }
}



