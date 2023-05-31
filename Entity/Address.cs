using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Address:BaseEntity
    {
        //Bir kullanıcının birden fazla adresi olabilir .

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public User User { get; set; }   //Navigation Property denir bu yapıya.

        //sadece kullanıcının Id bilgisinden ziyade  ,
        //kullanıcıyı işaret eden objeye de ulaşmak istiyoruz.

         public int UserId { get; set; }         // herhangi kullanıcının ıd bilgisiyle adresini bulabiliriz.(Zorunlu alan)

        // Bu kısım bizim için Foreing Key (Yabancı Anahtar) -burada tipin ismi ve ıd bilgisiyle bunu FK oldğ anlar-
       // yazamasak da prop , otomatik arkada int yapılı ıd kısmı oluşur .

        // int , Null değer (int =>0) kabul etmez. --> 0'ı kabul etmez yani 
        //Bundan dolayı  "int?" yaparsak , Artık Null değerleri de kabul etmiş olur.(int => Null,1,2,3,.. vb.)


    }
}
