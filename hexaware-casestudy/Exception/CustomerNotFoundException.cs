using System;
namespace hexaware_casestudy.Exception
{
	public class CustomerNotFoundException:IOException
	{
		public CustomerNotFoundException(string? message) : base(message)
		{
		}
		
	}
}

