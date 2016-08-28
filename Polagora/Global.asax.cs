using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Configuration;

namespace Polagora
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (true)
            {
                var configuration = new Migrations.Configuration();
                string ConnectionString = ConfigurationManager.AppSettings["AzureConnectionString"];
                configuration.TargetDatabase = new DbConnectionInfo(ConnectionString, "System.Data.SqlClient");

                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }
        }
    }
}
