
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductPhoto { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Product() { }
    }
}
