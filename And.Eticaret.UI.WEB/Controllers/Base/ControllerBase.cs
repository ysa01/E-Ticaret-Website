using And.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.Eticaret.UI.WEB.Controllers.Base
{
    public class AndControllerBase:Controller
    {
        //kullanıcı login kontorlü
        public bool IsLogin { get; set; }
        //giriş yapmış kişinin ID si
        public int LoginUserID { get; private set; }
        //Login User Entity
        public User LoginUserEntity { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
           /* Session["LoginUserID"] 
            Session["LoginUser"] */
           if(requestContext.HttpContext.Session["LoginUserID"]!=null)
            {
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserID"];
                LoginUserEntity = (Core.Model.Entity.User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);
        }
    }
}