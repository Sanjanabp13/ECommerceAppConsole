using System;
using hexaware_casestudy.Model;

namespace hexaware_casestudy.Repositories
{
	public interface IProductRepository
	{
        bool CreateProduct(Product product);

        bool DeleteProduct(int productId);
        Product GetProductById(int productId);
    }
}

