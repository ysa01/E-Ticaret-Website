using And.Eticaret.Core.Model;
using And.Eticaret.Core.Model.Entity;
using And.Eticaret.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class OrderController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Order
        [Route("Siparisver")]
        public ActionResult AddressList()
        {
            var adres = db.Addresses.Where(x => x.UserID == LoginUserID).ToList();
            return View(adres);
        }
        public ActionResult CreateUserAddress()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateUserAddress(UserAddress adres)
        {
            adres.CreateDate = DateTime.Now;
            adres.CreateUserID = LoginUserID;
            adres.UserID = LoginUserID;
            adres.IsActive = true;
            var adrs = db.Addresses.Add(adres);
            db.SaveChanges();

            return RedirectToAction("AddressList");
        }
        public ActionResult CreateOrder(int id)
        {
            var sepet = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList(); //Burası çok kritik action a geleln ıd adress Id si ama sepetin içine ulaşmak için userId ye göre sepetin içindekileri getiriyoruz ve product bilgileride lazım çünkü fiyat hesaplarını ordan yapıyoruz.

            Order order = new Order();

            order.CreateDate = DateTime.Now;
            order.CreateUserID = LoginUserID;
            order.StatusID = 7;
            order.TotalProductPrice = sepet.Sum(x => x.Product.Price);
            order.TotalTaxPrice = sepet.Sum(x => x.Product.Tax);
            order.TotalDiscount = sepet.Sum(x => x.Product.Discount);
            order.TotalPrice = sepet.Sum(x => x.Product.Price) + sepet.Sum(x => x.Product.Tax);
            order.UserAddressID = id;
            order.UserID = LoginUserID;
            order.OrderProducts = new List<OrderProduct>(); //burda tekrar liste haline getirdik çün aşağıda döngü halinde sepetteki tüm ürünler için maplama yapıyoruz onun için list getirdik
            foreach (var item in sepet)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate=DateTime.Now,
                    CreateUserID=LoginUserID,
                    ProductID=item.ProductID,
                    Quantity=item.Quantity
                });
                db.Baskets.Remove(item); // siparişi oluşturduktan sonra spetten siliyoruz her ürün için maplandikten sonra siliyoruz.
            }
            db.Orders.Add(order);
            db.SaveChanges();
            var orderID= db.Orders.Where(x => x.UserID == LoginUserID).AsEnumerable().LastOrDefault().ID;
            //var orderID = db.Orders.Where(x => x.UserID == LoginUserID).OrderByDescending(q => q.ID).FirstOrDefault();
            return RedirectToAction("Detail", new {id=orderID });
        }
        public ActionResult Detail(int id)
        {
            var getOrder = db.Orders.Include("OrderProducts")
                .Include("OrderProducts.Product") // product la bağlantısını order productan yaptık detay sayfasında herşeyi görüntüleyeceğiz
                .Include("OrderPayments")
                .Include("Status")
                .Include("UserAddress").
                Where(x => x.ID == id).FirstOrDefault();
            return View(getOrder);
        }
        [Route("MyOrder")]
        public ActionResult Index()
        {
            var order = db.Orders.Include("Status").Where(x => x.UserID == LoginUserID).ToList();
            return View(order);
        }
        public ActionResult Pay(int id)
        {
            var ode = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            ode.StatusID = 2;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = ode.ID });
        }
    }
}