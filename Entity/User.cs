using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User:BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        [Column(TypeName="varchar(20)")] //Db deki type kısmını belirledik
        public string Email { get; set; }

        public List<Address> Addresses { get; set; } //Navigation Property denir bu yapıya.

        //Herhangi kullanıcı üzerinden Adresses dediğimde, kullanıcıya ait adres gelecek.

        public Customer Customer { get; set; }
   
    }
}
