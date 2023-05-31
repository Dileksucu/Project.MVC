using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccess
{
    public class ProjectDbContext:DbContext
    {

        

        // DbContext sınıfı biizm yerimize Vt bağlantısını kendi yapacak 
        //Ama VT bağlanabilmesi için hangi Vt ile çalışacağını yani PROVİDER'ı tanımlaman gerekir.


        public DbSet<Product> Products { get; set; }
        // ürün ekliyeceksek bu liste üzerinden ekleriz
        public DbSet<Category> Categories { get; set; }
        // categori ekliceksek bu liste üzerinden ekleriz

       // DbSet<Order> Orders { get; set; }   
        //siparişleri ekleyeceğimiz context listesi

        public DbSet<User> Users { get; set; }
        //User bilgilerini bu context üzerinden değiştiricez

        public DbSet<Address> Address { get; set; }
        //Adres bilgilerini bu context üzerinden değiştiricez
        
        public DbSet<Customer> Customers { get; set; }
        //Müşteri bilgileirini güncellemek için bu Contexi kullanıcaz.
        
        public DbSet<Supplier> Suppliers { get; set; }
        // Tedarikçi bilgilerini güncellemek için bu contexti kullanıcaz
        
        public DbSet<ProductCategory> ProductCategories { get; set; }

        



        /// <summary>
        ///TODO:Providerb --> Hangi VT kullanacaksın (sql,mysql,sqlite vb.)
        /// "ovveride OnCon.." yazıp tab tab yaapınca kendi oluşturuyor s
        /// </summary>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-1RUKRP01\SQLEXPRESS01;Initial Catalog=Northwind;Integrated Security=SSPI;TrustServerCertificate=True;");
            
            //optionsBuilder.UseSqlServer(@"Server=LAPTOP-1RUKRP01\SQLEXPRESS01;Database=Nortwind;Trusted_Connection=True;MultipleActiveResultSets=True;Integrated Security=true;TrustServerCertificate=True");

            // Burada Sql Connect kurmak için bu şekilde kullanılması gerekir . 

            // Db Providers olarak, sqlserver kullandığımı belirttim.

            //server ile oluşturulan bir yapı olmadığı için içine he4rhangi db ismi , şifresi gibi alanları yazmıyoruz 
            // Biz Migration fonk ile ilerleiyoruz

        }


        /// <summary>
        /// Fluent Api Kullanım Örneği;
        ///Bu modelBuild sayesinde artık ProductId ve CategoryId kısmlarında tekrarlanan alanlar kabul edilmez. 
        ///HasKey()--> Bu durumda şunu yaptık; ProductCategory tablosunun , iki Key'in kombinasyonu olarak tanımladık (ProductId ve CategoryId).
        ///Yani bu tablonun iki tane PK(Birincil anahtarı) olmuş olacak.
        ///HasOne()-->Biz şu anda ProductCategory tablosundayız , product tablosuna geçmek istiyoruz , o zaman bu methodu kullanırız.
        /// </summary>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.UserName)
                .IsUnique();

            // Artık uygulama da Product entity'sinin, Database'de olan tablo adı "Urunler" dir.
            // Buna tabloyu Map etme işlemi denir .

            //modelBuilder.Entity<Product>()
            //   .ToTable("Urunler");

            modelBuilder.Entity<Customer>()
                .Property(p => p.IdentityNumber)
                .HasMaxLength(11) //TC NO olduğunu düşünelim
                .IsRequired(); //Bu alanı zorunlu olarak tanımladık


            modelBuilder.Entity<ProductCategory>()
                     .HasKey(t => new {t.ProductId,t.CategoryId });

            modelBuilder.Entity<ProductCategory>() // bir entity şeçtik , bu entity konumlandırdık kendimizi.
                .HasOne(pc=> pc.Product) // bir productCategories de bir tane product'ı var.
                .WithMany(p=>p.ProductCategories) //product'ın birden fazla categories var
                .HasForeignKey(pc => pc.ProductId); // productCategories'in de productId kısmını yabancı anahtar yapıyoruz . 


            modelBuilder.Entity<ProductCategory>()
               .HasOne(pc => pc.Category)
               .WithMany(c=>c.ProductCategories)
               .HasForeignKey(pc => pc.CategoryId);


            //BU şekilde tablolar arasında geçiş yapmak ve alanları belirlemek - Fluent Api kullanım şeklidir.
        }



        // Her veritabanı işlemş sonucunda savechange çalıştırılır; yani her varlık için ortak işlemler burada yapılabilir.Bu yazdıklarımız entitylerin ortak olan ,
        //ekleme ve güncelleme işlemleridir.
       
        public override int SaveChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(x => {
                if (x.Entity is IEntity) {

                    if (x.State is EntityState.Added) { // eklemek -insert

                        ((IEntity)x.Entity).InsertDate = DateTime.Now;  
                    }

                    if (x.State is EntityState.Modified) // güncellemek
                    {
                        ((IEntity)x.Entity).LastUpdateDate = DateTime.Now;
                    }
                }
            });
            return base.SaveChanges();
        }

    }



}
