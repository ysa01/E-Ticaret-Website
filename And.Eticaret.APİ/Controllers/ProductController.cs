using And.Eticaret.Core.Model;
using And.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace And.Eticaret.APİ.Controllers
{

    public class ProductController : ApiController
    {
        AndDB db = new AndDB();
        
        public List<Product> Get()
        {
            var getir = db.Products.Where(x => x.IsActive == true).ToList();
            return getir;
        }
        public Product Get(int id)
        {
            var data = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return data;
        }
    }
}
