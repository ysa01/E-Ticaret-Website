using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.Eticaret.UI.WEB.Areas.Admin
{
    public class AdminControllerBase:Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var IsLogin = false;
            if (requestContext.HttpContext.Session["AdminLoginUser"]==null)//oturum var mı diye kontrol ediyoruz null ise tekrar giriş yaptırıyoruz değilse else düşyor
            {
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin");
            }
            else
            {
                //Admin icerde
                //sayfayı çalıştır
                base.Initialize(requestContext);
            }
            
        }
    }
}