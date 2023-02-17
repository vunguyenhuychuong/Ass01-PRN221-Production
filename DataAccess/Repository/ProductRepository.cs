using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int productId)
        {
            ProductDAO.Instance.DeleteProduct(productId);
        }

        public Product GetProductById(int productId)
        {
           return ProductDAO.Instance.GetProductById(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return ProductDAO.Instance.GetProducts();
        }

        public void InsertProduct(Product product)
        {
           ProductDAO.Instance.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            ProductDAO.Instance.UpdateProduct(product);
        }
    }
}
