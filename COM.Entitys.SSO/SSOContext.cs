using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace COM.Entitys.SSO
{
    public class SSOContext : DbContext, IDataProtectionKeyContext
    {
        public SSOContext(DbContextOptions<SSOContext> option)
            : base(option)
        { }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
    public class LAContextFactory : IDesignTimeDbContextFactory<SSOContext>
    {
        public SSOContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SSOContext>();
            optionsBuilder.UseSqlServer("Data Source=192.168.0.155;Initial Catalog=SSO;Persist Security Info=True;User ID=sa;Password=lj@123456;Application Name=SSO");
            return new SSOContext(optionsBuilder.Options);
        }
    }
}
