using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DapperGenericRepository.App_Start
{
    public class AutofacWebApiConfig
    {
        //public static void Initialize(HttpConfiguration config)
        //{
        //    Initialize(config, RegisterServices());
        //}

        //private static void Initialize(HttpConfiguration config, IContainer container)
        //{
        //    config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        //}

        //private static IContainer RegisterServices()
        //{
        //    //Register your Web API controllers.  
        //    //AutofacResolution.Instance.Builder().RegisterApiControllers(Assembly.GetExecutingAssembly());

        //    //AutofacResolution.Instance.Build();

        //    //return AutofacResolution.Instance.Container();
        //}
    }
}