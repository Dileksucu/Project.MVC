using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //test verileri eklemek için kullanılır .
    //istediğimiz context'i bu methoda gönderebiliriz.
    public static class DataSeeding
    {
        public static void Seed(DbContext context) 
        {
             


            //if (context.Database.GetDatabaseMigrations().Count == 0)
            //{
            //    //getpendingmigration() --> migrationların bir listesini verir.

            //    //her db contex için kaç migr. oldğ bulmak istiyorsak tek tek if açarak her  
            //    //birine bakmalıyız .

            //    // ya da hangi contex migrationı olduğunu bulmak için de kullanılır 

            //}




        }
    }
}
