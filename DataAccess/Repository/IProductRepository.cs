using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);

        Product GetProductById(int productId);

    }
}
