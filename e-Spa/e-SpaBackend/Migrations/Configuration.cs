using System.IO;
using System.Reflection;
using Microsoft.Azure.Mobile.Server.Tables;

namespace e_SpaBackend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.MobileServiceContext>
    {
        public Configuration()

        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
            ContextKey = "SalonBizManager_Backend.Models.SalonBizContext";
        }

        protected override void Seed(Models.MobileServiceContext context)
        {
            #region MyRegion

            //Seed the ClientAppointments_view script in the configuration file.
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var baseDirectory = Path.GetDirectoryName(path) + "\\Migrations\\ClientAppointmentsView.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDirectory));

            //Seed the SalonAppointments_view script in the configuration file.
            string codeBase2 = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri2 = new UriBuilder(codeBase2);
            string path2 = Uri.UnescapeDataString(uri2.Path);
            var baseDirectory2 = Path.GetDirectoryName(path2) + "\\Migrations\\SalonAppointmentsView.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDirectory2));

            //Seed the SalonserviceView script in the configuration file.
            string codeBase3 = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri3 = new UriBuilder(codeBase3);
            string path3 = Uri.UnescapeDataString(uri3.Path);
            var baseDirectory3 = Path.GetDirectoryName(path3) + "\\Migrations\\SalonServicesView.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDirectory3));

            #endregion
        }
    }
}
