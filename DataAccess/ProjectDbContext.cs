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




        /// <summary>
        ///TODO:Provider
        /// "ovveride OnCon.." yazıp tab tab yaapınca kendi oluşturuyor s
        /// </summary>
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
          optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-1RUKRP01\SQLEXPRESS01;Initial Catalog=ProjectDb;Integrated Security=SSPI;TrustServerCertificate=True;");
           // Burada Sql Connect kurmak için bu şekilde kullanılması gerekir . 


            // Db Providers olarak, sqlserver kullandığımı belirttim.

            //server ile oluşturulan bir yapı olmadığı için içine he4rhangi db ismi , şifresi gibi alanları yazmıyoruz 
            // Biz Migration fonk ile ilerleiyoruz
        }

    }


}
