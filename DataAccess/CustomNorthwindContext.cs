using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataAccess
{
    // var olan DB üzerine yeni bir db set eklemek istedik o yüzden oluşturduk bu Db'yi.
    class CustomerOrder 
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public int OrderCount { get; set; }

    }
    public class CustomNorthwindContext :NorthwindContext
    {

      //  public DbSet<CustomerOrder> CustomerOrder { get; set; }

        public CustomNorthwindContext()
        {
        }

        public CustomNorthwindContext( DbContextOptions<NorthwindContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sınıf için bir PK tanımlaması yaptık (tablo için de anahtar olmadığını tanımladık)
            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.HasNoKey();


                //entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                //entity.Property(e => e.CustomerId).HasMaxLength(11);
          
                //Burada yapılan işlemler Mapping işlemidir.
                //Mapping --> Db ile uygulama arasında kullanılan objeyi birbirine entegre etme,eşleştirme işlemi denilebilir.

            });
        }
    }
}
