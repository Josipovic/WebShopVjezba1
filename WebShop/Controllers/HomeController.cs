using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using PagedList;
using PagedList.Mvc;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetCategories()
        {

            WebShopContext db = new WebShopContext();
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult GetProducts(int categoryId, int? page, string search, string orderBy)
        {

            //ViewBag.OrderName = string.IsNullOrEmpty(orderBy) ? "NameDesc" : "";
           // ViewBag.OrderPrice = orderBy == "Price" ? "PriceDesc" : "Price";
            WebShopContext db = new WebShopContext();
            var products = db.Products.Where(x => x.CategoryId == categoryId).AsQueryable();
            if (string.IsNullOrEmpty(search) == false)
            {

                products = products.Where(p => p.Name.ToUpper().StartsWith(search.ToUpper()));
            }
            switch (orderBy)
            {

                case "NameDesc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "PriceDesc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }
            return View(products.ToList().ToPagedList(page ?? 1, 3));
        }


        public ActionResult GetRandomDiscountedProduct()
        {

            WebShopContext db = new WebShopContext();
            var products = db.Products.Where(x => x.Discount == true).ToList();
            int selectedIndex = (int)(DateTime.Now.Ticks % products.Count);
            var product = products[selectedIndex];
            return PartialView(product);
        }

        public ActionResult Details(int Id)
        {

            WebShopContext db = new WebShopContext();
            var product = db.Products.Find(Id);
            return View(product);
        }

        public ActionResult GetAdminMenu()
        {

            return PartialView();
        }
        public ActionResult GetCartCount() {

            return PartialView();
        }

    }
}