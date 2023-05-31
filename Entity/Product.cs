using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product:BaseEntity
    {
      //[DatabaseGenerated(DatabaseGeneratedOption.None)]- Otomatik olarak bir sayı gitmez Id alanına.
      // ıdentity dediğimiz de tek bir değer gelecek ve değiştirilemeyecek 
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

    }
}
