using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace e_SpaBackend.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<e_SpaBackend.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
            ContextKey = "e_SpaBackend.Models.MobileServiceContext";
        }

        protected override void Seed(e_SpaBackend.Models.MobileServiceContext context)
        {
            //Seed the ClientAppointments_view script in the configuration file.
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var baseDirectory = Path.GetDirectoryName(path) + "\\Migrations\\ClientAppointmentsview.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDirectory));

            //Seed the SalonAppointments_view script in the configuration file.
            string codeBase2 = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri2 = new UriBuilder(codeBase2);
            string path2 = Uri.UnescapeDataString(uri2.Path);
            var baseDirectory2 = Path.GetDirectoryName(path2) + "\\Migrations\\SalonAppointmentsview.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDirectory2));

            //Seed the SalonserviceView script in the configuration file.
            string codeBase3 = Assembly.GetCallingAssembly().CodeBase;
            UriBuilder uri3 = new UriBuilder(codeBase3);
            string path3 = Uri.UnescapeDataString(uri3.Path);
            var baseDirectory3 = Path.GetDirectoryName(path3) + "\\Migrations\\SalonServicesView.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDirectory3));
        }
    }
}
