using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class WebShopContext:DbContext
    {
        public System.Data.Entity.DbSet<WebShop.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<WebShop.Models.Product> Products { get; set; }
    }
}