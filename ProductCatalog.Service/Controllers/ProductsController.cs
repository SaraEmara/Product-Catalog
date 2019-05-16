
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Repository.Entities;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ProductCatalog.Repository.Controllers;
using ProductCatalog.Repository.Repository;
using System;
using System.Web;
using System.Reflection;
using OfficeOpenXml;
using ProductCatalog.Service.Services.ExportProductCatalogServices;
using Microsoft.AspNetCore.Hosting;

namespace ProductCatalog.Service.Controllers
{
    public class ProductsController : Controller
    {
        private IGenericRepository<Product> ProductRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductsController(IGenericRepository<Product> productRepo,IHostingEnvironment hostingEnvironment)
        {
            ProductRepo = productRepo;
            _hostingEnvironment = hostingEnvironment;
        }

        [NonAction]
        void SaveProductImage(IFormFile file)
        {
            string storePath = "ProductsImages\\";
            var path = Path.Combine(
                            _hostingEnvironment.WebRootPath, storePath,
                            file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);

            }
        }

        #region Add Product
        // GET: Products/AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product Product, [FromForm] IFormFile file)
        {
            SaveProductImage(file);
            Product.ProductPhoto = file.FileName;

            try
            {
                if (Product != null)
                {
                    ProductRepo.Insert(Product);
                    return RedirectToAction(nameof(ViewAllProducts));
                }
                else return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Get All Products

        // GET: Products
        [HttpGet]
        public ActionResult ViewAllProducts()
        {
            List<Product> Products = ProductRepo.GetALL();
            if (Products == null)
            {
                Products = new List<Product>();
            }
            return View(Products);
        }
        #endregion

        #region Edit Product Details
        // GET: Products/Edit/5
        [HttpGet]
        public ActionResult EditProduct(string id)
        {
            Product Product = ProductRepo.GetById(id);

            return View(Product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(string id, Product Product, IFormFile file)
        {
            SaveProductImage(file);
            Product.ProductPhoto = file.FileName;

            try
            {
                if (Product != null)
                {
                    Product.ProductId = id;
                    Product.LastUpdatedDate = DateTime.UtcNow;
                    ProductRepo.Update(Product);
                    return RedirectToAction(nameof(ViewAllProducts));

                }
                else return BadRequest();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Remove Product

        // GET: Products/RemoveProduct/5
        [HttpGet]
        public ActionResult RemoveProduct(string id)
        {
            Product Product = ProductRepo.GetById(id);
            return View(Product);
        }

        // POST: Products/RemoveProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveProduct(string id, Product Product)
        {
            Product = ProductRepo.GetById(id);
            try
            {
                ProductRepo.Delete(Product);

                return RedirectToAction(nameof(ViewAllProducts));
            }
            catch
            {
                return View(Product);
            }
        }
        #endregion


        #region Search For Specific Product

        // GET: Products/ProductDetails/5
        [HttpGet]
        public ActionResult ProductDetails(string id)
        {
            return View(ProductRepo.GetById(id));
        }
        #endregion


        [HttpGet]
        public ActionResult ExportCatalog()
        {
            try
            {
                List<Product> Products = ProductRepo.GetALL();

                return File(ExportProductCatalog.ToExcel(Products), "application/ms-excel", $"ProductCatalog.xlsx");
            }
            catch
            {
                return RedirectToAction(nameof(ViewAllProducts));
            }
        }
    }
}