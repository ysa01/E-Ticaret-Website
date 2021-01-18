using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(And.Eticaret.APİ.Startup))]
namespace And.Eticaret.APİ
{
    public partial class Startup // bu bir rutiel gibi owin, owin security, Oauth2  kütüphaneleri gerekli 
    {


        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
           ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);

        }
        //wepapinin configurrationunu çagırabiliriz

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new LoginProvider()
            });
            
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}