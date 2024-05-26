using System;
using hexaware_casestudy.Model;

namespace hexaware_casestudy.Repositories
{
	public interface IOrderRepository
	{
        int CreateOrder(Order order);
        bool UpdateOrder(int order_id, decimal totalprice);

       
    }
}

