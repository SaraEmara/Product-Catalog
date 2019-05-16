using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Repository.Entities;

namespace ProductCatalog.Repository.Context
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions Options):base(Options){ }
       public DbSet<Product> Products { get; set; }

    }
}
