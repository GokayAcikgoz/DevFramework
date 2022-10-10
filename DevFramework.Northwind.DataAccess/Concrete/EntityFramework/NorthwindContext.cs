using DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        //Migration islemini kapattık Code-First calistigimiz icin. VeriTabanina herhangi bir sekilde veri eklemsin diye.
        public NorthwindContext() : base("Data source = (localdb)\\MSSQLLocalDB; initial catalog = Northwind; integrated security = true")
        {
            Database.SetInitializer<NorthwindContext>(null);
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            //diger mapleride buraya yazıyoruz.
        }
    }
}
