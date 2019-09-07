using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Naziv kategorije")]
        public string Name { get; set; }

        [Display(Name="Opis kategorije")]
        public string Description { get; set; }

        public virtual List<Product>Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}