using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DronTaxiWindowsForms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var config = buildConfig();
            initDb(config);
            //Application.Run(new Form1(config()));
            Application.Run(new Form2(config.GetSection("Configuration")["url"]));
        }
        static IConfigurationRoot buildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("_parameters.json", optional: true, reloadOnChange: true)
                .Build();
        }
        private static void initDb(IConfigurationRoot config)
        {
            using (DatabaseContext db = new DatabaseContext(config.GetSection("Configuration")["ConnectionString"]))
            {
                try
                {
                    var sql = "CREATE LOGIN [BUILTIN\\IIS_IUSRS] from windows WITH DEFAULT_DATABASE=[webapp1], DEFAULT_LANGUAGE=[русский]" + Environment.NewLine +
                "USE[webapp1]" + Environment.NewLine +
                "grant all to[BUILTIN\\IIS_IUSRS]" + Environment.NewLine +
                "ALTER ROLE[db_owner] ADD MEMBER[BUILTIN\\IIS_Iusrs]" + Environment.NewLine;
                    db.Database.ExecuteSqlCommand(sql);
                }
                catch (Exception e) { }
            }
        }
    }
}
