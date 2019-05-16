using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Entities
{
    public class ProductDTO
    {
      
            public string ProductName { get; set; }
            public string ProductPhoto { get; set; }
            public decimal ProductPrice { get; set; }
            public DateTime LastUpdatedDate { get; set; }
        
    }
}
