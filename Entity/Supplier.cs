using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //Tedarikçi - Supplier
    public  class Supplier:BaseEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string TaxNumber { get; set; }

    }
}

//BU  durumda gelen MÜŞTERİ de olabilir  , TEDARİKÇİ de olabilir 
// Yada her iksi de olabilir 