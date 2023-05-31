using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //Müşteri - Customer
    public class Customer:BaseEntity
    {

        [Column("customer_id")] // Vt karşılığı customer_id olarak tanımlanır.
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public String FirstName { get; set; }
        public string LastName { get; set; }

        public User User { get; set; }    // eğer hem customer hem de
                                          // user tabblosuna karşılıklı prop eklersek "one to one " bir ilişki olur.  
        public int UserId { get; set; }
        // burası Uniq bir alan olacak - Tek bir kayıda karşılık gelicek .

        [NotMapped]
        public string FullName { get; set; } //NotMapped yaptığımızda , Vt oluşan Customer tablosunda bu alanı görmeyiz.
    }
}
