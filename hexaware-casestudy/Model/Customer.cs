
using System;
namespace hexaware_casestudy.Model
{
    
	public class Customer
    {
        public int Customer_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }

        public Customer() { }
        public Customer(int customer_id,
        string name,
        string email,
        string password,
        string role)
        {
            Customer_id = customer_id;
            Name = name;
            Email = email;
            Password = password;
            UserRole = role == "yes" ? "user" : "admin";
        }

        public override string ToString()
        {
            return $"Customer ID: {Customer_id}\nName: {Name}\nEmail: {Email}\n";
        }
    }

}

