using And.Eticaret.Core.Model;
using And.Eticaret.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class BasketController : AndControllerBase
    {
        // GET: Basket
        AndDB db = new AndDB();
        [HttpPost]
        public JsonResult AddProduct(int productID,int quantity)
        {
            db.Baskets.Add(new Core.Model.Entity.Basket
            {
                ProductID = productID,
                Quantity = quantity,
                CreateDate = DateTime.Now,
                CreateUserID = LoginUserID,
                UserID = LoginUserID

            } );
           var rt= db.SaveChanges();
            return Json(rt,JsonRequestBehavior.AllowGet);
        }
        [Route("MyBasket")]
        public ActionResult Index()
        {
            var bask = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            return View(bask);
        }
        public ActionResult Delete(int id)
        {
            var bask = db.Baskets.Where(x => x.ID == id).FirstOrDefault();
            db.Baskets.Remove(bask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}