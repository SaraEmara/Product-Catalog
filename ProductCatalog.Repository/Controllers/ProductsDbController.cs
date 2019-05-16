using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Repository.Entities;
using System.ComponentModel.DataAnnotations;
using ProductCatalog.Repository.Repository;

namespace ProductCatalog.Repository.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]

    public class DbProductsController : ControllerBase
    {
        private IGenericRepository<Product> ProductRepo;
        public DbProductsController() {  }
        public DbProductsController(IGenericRepository<Product> productRepo) { ProductRepo = productRepo; }

        // GET: api/Products
        [HttpPost("AddProduct")]
        public bool AddProduct([FromBody][Required]Product Product)
        {
            bool status = false;
            if (Product != null)
            {
                ProductRepo.Insert(Product);
                status = true;
            }
            return status;
        }

        // GET: api/Products/5
        [HttpGet("Edit/{id}")]
        public bool Edit(Product Product)
        {
            bool status = false;
            if (Product != null)
            {
                ProductRepo.Update(Product);
                status = true;
            }
            return status;
        }

        // POST: api/Products
        [HttpPost("RemoveProduct")]
        public void Delete([FromBody]Product Product)
        {
            ProductRepo.Delete(Product);
        }


        [HttpGet]
        public List<Product> GetAll()
        {
            return ProductRepo.GetALL();

        }

        [HttpGet("{id}")]
        public Product Search(string id)
        {
            return ProductRepo.GetById(id);
        }

    }
}
