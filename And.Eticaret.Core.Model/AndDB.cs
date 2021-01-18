using And.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.Eticaret.Core.Model
{
   public class AndDB:DbContext
    {
        //Tablolar
        public AndDB()
            : base(@"Data Source=DESKTOP-RF7H0JF\MSSQLSERVER01;Initial Catalog=AndEticaretDB;Integrated Security=True")
        {

        }
        public DbSet <User> Users { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet <UserAddress> Addresses { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet <Status> Statuses { get; set; }
        public DbSet <Basket> Baskets { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet <OrderProduct> OrderProducts { get; set; }
        public DbSet <OrderPayment> OrderPayments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Bu override Ve altındaki kod bloğu database oluşturmak için önemli!!
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
        //burayıda yazdıktan sonra databaseden new data deyip kendimiz adını verin propertden connection stringini alıp 
        // yukardaki constructer içine koyuyoruz ve aşağıda package manager consoleda databasein kurucağımız projeyi seçip 
        // Enable-Migrations yazıyoruz karşımıza configaratipn classı çıkıyor ordan "AutomaticMigrationsEnabled = false;" true ya çeviriyoruz PM>Update-Database yazıyoruz  BİTTİ.(çok önemlii !!!)
    }

}

