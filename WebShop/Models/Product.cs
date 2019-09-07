using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name="Naziv")]
        public string Name { get; set; }

        [Display(Name="Opis")]
        public string Description { get; set; }

        [Display(Name="Cijena")]
        public decimal Price { get; set; }

        [Display(Name ="Na popustu")]
        public bool Discount { get; set; }

        [Display(Name ="Cijena na popustu")]
        public decimal DiscountPrice { get; set; }

        [Display(Name ="Putanja do slike")]
        public string ImageUrl { get; set; }

        [Display(Name ="Jamstvo")]
        public int Warranty { get; set; }

        [Display(Name="Dostupan")]
        public bool Available { get; set; }

        [Display(Name="Novo")]
        public bool NewProduct { get; set; }

        [Display(Name ="Kategorija")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}