using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set; }
        public string CategoryName {get; set; }

        public DateTime CreatedAt {get; set; }

        public DateTime UpdatedAt {get; set; }

        public List <Association> Association {get; set; }

    }
}