using And.Eticaret.Core.Model;
using And.Eticaret.Core.Model.Entity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace And.Eticaret.APİ
{
    public partial class LoginProvider:OAuthAuthorizationServerProvider //startupda provaiderın bu clastan türüetilmesini istediği için kalıtım yaptık
    {
        AndDB db = new AndDB();
        public  User Login(string email,string password)
        {
            var contr = db.Users.Where(x => x.Email == email && x.Password == password && x.IsActive == true).ToList();
            if(contr.Count>0)
            {
                return contr.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();//gelen requesti doğrulamamız gerek
            return base.ValidateClientAuthentication(context);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var use = Login(context.UserName, context.Password);
            if(use==null)
            {
                //hatali
                context.SetError("Invalid.Grand","Hatalı Kullanıcı Bilgisi");
            }
            else
            {
                //Basarili
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("UserName", context.UserName));
                identity.AddClaim(new Claim("Password", context.Password));
                identity.AddClaim(new Claim("UserID", use.ID.ToString()));
                context.Validated(identity);
            }
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}