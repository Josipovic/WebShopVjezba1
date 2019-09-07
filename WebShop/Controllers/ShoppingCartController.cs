using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View("Cart");
        }

        public ActionResult AddToCart(int Id) {

            WebShopContext db = new WebShopContext();
            List<Item> cart = (List<Item>)Session["cart"];
            if (cart == null)
            { //ako je null lista ne postoji u sessionu

                cart = new List<Item>();
                var product = db.Products.Find(Id);
                Item item = new Item();
                item.Product = product;
                item.Count = 1;
                cart.Add(item);
                Session["cart"] = cart;
            }
            else
            { //lista postoji u sessionu
                //moramo pretraziti da li proizvod vec postoji u kosarici
                int location = cart.FindIndex(x => x.Product.Id == Id);
                //pronadji mi index od elementa koji zadovoljava ovaj kriterij
                if (location == -1)
                { //ako je lokacija -1 to znaci da proizvod nije pronađen u listi

                    var product = db.Products.Find(Id);
                    Item item = new Item();
                    item.Product = product;
                    item.Count = 1;
                    cart.Add(item);
                    Session["cart"] = cart;

                }
                else { //proizvod je pronadjen na lokaciji spremljenoj u varijabli location

                    cart[location].Count++;
                    
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        public ActionResult IncreaseCount(int Id) {
            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == Id);
            cart[location].Count++;
            Session["cart"]=cart;


            return RedirectToAction("Index");
        }
        public ActionResult DecreaseCount(int Id) {
            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id==Id);
            if (cart[location].Count == 1)
            {
                cart.RemoveAt(location);
            }
            else {
                cart[location].Count--;
            }
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id) {

            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == Id);
            cart.RemoveAt(location);
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        public ActionResult Checkout() {

            return View();
        }

        [HttpPost]
        public ActionResult Checkout(AdressInformation info) {

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new 
                NetworkCredential("bestmailever5@gmail.com","kompjuter123");

            string mailMessage = "Nova narudžba je zaprimljena!" + "Kupac:" + info.FirstName + " " 
                + info.LastName + Environment.NewLine + "Adresa" + Environment.NewLine + info.City + Environment.NewLine +
               info.Adress + Environment.NewLine + info.ZipCode + Environment.NewLine + info.PhoneNumber + Environment.NewLine ;

            var products = (List<Item>)Session["cart"];
            decimal total = 0;

            foreach (var product in products) {
                if (product.Product.Discount == true)
                {
                    total += (product.Count * product.Product.DiscountPrice);
                }
                else {

                    total += (product.Count * product.Product.Price);
                }
                mailMessage += (product.Product.Name + "-" + product.Count + " komada " + Environment.NewLine);
            }
            mailMessage += "Ukupni iznos narudžbe:" + " " + total;
            MailMessage message = new MailMessage("bestmailever5@gmail.com","katarina.josipovic@hotmail.com","Narudžba",mailMessage);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(message);
            return View("OrderCompleted");
        }
    }
}