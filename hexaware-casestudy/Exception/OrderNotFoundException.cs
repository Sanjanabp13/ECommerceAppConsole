using System;
namespace hexaware_casestudy.Exception
{
	public class OrderNotFoundException:IOException
	{
		public OrderNotFoundException(string? message): base(message)
		{
		}
	}
}

