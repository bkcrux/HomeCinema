using Autofac;
using HomeCinema.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace HomeCinema.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));

        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebapiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<HomeCinemaContext>()
                .As<DbContext>()
                .InstancePerRequest();


        }
    }
}