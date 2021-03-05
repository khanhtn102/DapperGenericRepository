using System;
using System.Threading.Tasks;
using System.Web.Http;
using DapperGenericRepository.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DapperGenericRepository.Startup))]

namespace DapperGenericRepository
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            AutofacWebApiConfig.Initialize(configuration);

            app.UseWebApi(configuration);
        }
    }
}
