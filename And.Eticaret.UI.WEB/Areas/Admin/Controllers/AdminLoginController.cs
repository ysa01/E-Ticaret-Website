using And.Eticaret.Core.Model;
using And.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Areas.Admin.Controllers
{
     
    public class AdminLoginController : Controller
    {
        AndDB db = new AndDB();
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {
            //var db = new AndDB(); globalde tanımlamak için yorum satırı yaptım fakat globalde böyle olmaz yani sayfanın üstünde
            var data = db.Users.Where(x => x.Email == Email && x.Password == Password&&x.IsActive==true&&x.IsAdmin==true).ToList();
            if(data.Count==1)
            {
                //doğru giriş burda bu usera ait user tablosundaki tüm verileri attık
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return Redirect("/Admin/Default/Index");
            }
            else
            {
                
                return View();
            }
        }
        public ActionResult Deneme()
        {
            var a = db.Orders.ToList();
            return View(a);
        }


    }
}