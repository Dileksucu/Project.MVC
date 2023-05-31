using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BaseEntity : IEntity
    {
        // date kıısmları burada ortak bütün entityler için oluşturulacak . (paketlemiş olduk ortak yapılan kısımları)
        public DateTime? InsertDate { get; set ; }
        public DateTime? LastUpdateDate { get ; set;  }
    }
}
