using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get; set; }
        public string ProductName {get; set; }

        public string ProductDescription {get; set; }
        public decimal ProductPrice {get; set; }

        public DateTime CreatedAt {get; set; }

        public DateTime UpdatedAt {get; set; }

        public List <Association> Association {get; set; }

    }
}