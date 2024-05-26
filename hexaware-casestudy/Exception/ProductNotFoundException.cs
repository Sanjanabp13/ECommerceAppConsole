using System;
namespace hexaware_casestudy.Exception
{
	public class ProductNotFoundException:IOException
	{
		public ProductNotFoundException(string? message) : base(message)
        {
		}
		
	}
}

