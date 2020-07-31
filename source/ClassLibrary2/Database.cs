using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
//using System.
//using Models;

namespace DAL
{
    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
        }
    }

    [DbConfigurationType(typeof(MyConfiguration))]
    public class DatabaseContext : DbContext
    {
        //private const string ConnectionString = @"Data Source=SML-03-S136\SQLEXPRESS2019;Initial Catalog=webapp1;User ID=sa;Password=sa";

        public DatabaseContext(string ConnectionString) : base(ConnectionString)
        //public DatabaseContext() : base(ConnectionString)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Func> Funcs { get; set; }
    }
}
