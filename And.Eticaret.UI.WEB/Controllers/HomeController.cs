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
    public class HomeController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
          var pro=  db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(pro);
        }
        public PartialViewResult GetMenu()
        {
            var menus = db.Categories.Where(x => x.ParentID == 1).ToList();
            return PartialView(menus);
        }
        [Route("Uye-Giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string Email,string Password)
        {
            var bul = db.Users.Where(x => x.Email == Email && x.Password == Password && x.IsActive == true && x.IsAdmin == false).ToList();
            if(bul.Count==1)
            {
                Session["LoginUserID"] = bul.FirstOrDefault().ID;
                Session["LoginUser"] = bul.FirstOrDefault();
                return Redirect("/Home/Index");
            }
            else
            {
                ViewBag.Error = "Hatalı Email Veya Şifre Girdiniz !!";
                return View();
            }
           
        }
        [Route("Uye-Kayit")]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser(User user)
        {
            try
            {
                user.CreateDate = DateTime.Now;
                user.CreateUserID = 1;
                user.IsActive = true;
                user.IsAdmin = false;
                db.Users.Add(user);
                db.SaveChanges();
                return Redirect("/Uye-Giris");
            }
            catch (Exception ex)
            {

                return View();
            }
            
          
        }
    }
}