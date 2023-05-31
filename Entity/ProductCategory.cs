using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProductCategory:BaseEntity
    {

       //NOT: Bu tabloyu dbSet<> olarak tanımlamadık , çünkü product ve category tablolarının için de bu tabloya ait bir 
       //Lİste Property tanımladık .

       // Bu tabloya ya product ya da category tablosu üzerinden ulaşıcaaz.

        public int ProductId { get; set; }  //Foreing Key (Yabancı Anahtar)
        public Product Product { get; set; }
        public int CategoryId { get; set; }  //Foreing Key (Yabancı Anahtar)
        public Category Category { get; set; }



        //[NotMapped] - ilgili entity'nin üzerine gelerek, Bu durumda ilgili Db içerisinde tablo olarak oluşturulmaz.
       
        //[Table("UrunKategorileri")] - Artık Db oluşturulan tablo ismi bu olur . 
        // Yani Uygulama kısmında "ProrductCategory" entity ismi , Db kısmında tablo ismi "UrunKategorileri" olur. 


    }
}
