using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public interface IEntity
    {
        // interface implement oluşmuş hali 
        public DateTime? InsertDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

    }
}
