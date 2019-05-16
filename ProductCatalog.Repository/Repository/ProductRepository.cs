using ProductCatalog.Repository.Context;
using ProductCatalog.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Repository
{
    /// <summary>
    /// Product Repository Managment for (create, update, delete and view Products).
    /// </summary>
    public class ProductRepository : IGenericRepository<Product>
    {
        ProductDbContext ProductDbContext;
        public ProductRepository(ProductDbContext productDbContext) { ProductDbContext = productDbContext; }

        public void Delete(Product Product)
        {
            ProductDbContext.Remove(Product);
            ProductDbContext.SaveChanges();
        }

        public bool DeleteById(string Id)
        {
            bool Status = false;
            Product Product=  ProductDbContext.Products.Where(p => p.ProductId == Id).FirstOrDefault();
            if(Product != null)
            {
                ProductDbContext.Remove(Product);
                ProductDbContext.SaveChanges();
                Status = true;
            }
            return Status; 

        }

        public List<Product> GetALL()
        {
            IQueryable<Product> Prods = ProductDbContext.Products;
            List<Product> p = new List<Product>(Prods);
            if (ProductDbContext.Products.ToList<Product>().Count == 0)
            {
                return new List<Product>();
            }
            return ProductDbContext.Products.ToList();
           
        }

        public Product GetById(string id)
        {
            return ProductDbContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }

        public void Insert(Product Product)
        {
            if (Product!=null) {
                Product.LastUpdatedDate = DateTime.UtcNow;
                object obj = ProductDbContext.Add(Product);
                ProductDbContext.SaveChanges();
            }
          

        }

        public void Update(Product Product)
        {

           Product SelectedPro= ProductDbContext.Products.Where(p => p.ProductId == Product.ProductId).FirstOrDefault();

            if (SelectedPro != null &&Product!=null )
            {
                SelectedPro.ProductPhoto = Product.ProductPhoto;
                SelectedPro.ProductName = Product.ProductName;
                SelectedPro.ProductPrice = Product.ProductPrice;
                SelectedPro.LastUpdatedDate = Product.LastUpdatedDate;
                ProductDbContext.SaveChanges();
            }
        }
    }
}
