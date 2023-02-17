using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public Product GetProductById(int productId)
        {
            Product product = null;
            try
            {
                using var dBContext = new FStoreDBAssignmentContext();
                product = dBContext.Products.SingleOrDefault(p => p.ProductId == productId);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();
            try
            {
                using var dBContext = new FStoreDBAssignmentContext();
                products = dBContext.Products.ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public void AddProduct(Product newProduct)
        {
            try
            {
                Product product = GetProductById(newProduct.ProductId);
                if (product == null)
                {
                    using var dBContext = new FStoreDBAssignmentContext();
                    dBContext.Products.Add(newProduct);
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The account is allready exist");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                Product product = GetProductById(productId);
                if (product != null)
                {
                    using var dBContext = new FStoreDBAssignmentContext();
                    dBContext.Products.Remove(product);
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("the product is not exist");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                Product productToUpdate = GetProductById(product.ProductId);
                if (productToUpdate != null)
                {
                    bool check = true;
                    if (product.ProductName == "")
                    {
                        check = false;
                        throw new Exception("The product Name is not null.");
                    }
                    if (check)
                    {
                        using var dbContext = new FStoreDBAssignmentContext();
                        dbContext.Products.Update(product);
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The Product is not exist");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
