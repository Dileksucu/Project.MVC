using DataAccess.ModelFirstNorthwind;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Product = DataAccess.ModelFirstNorthwind.Product;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();


class Program
{


    static void Main(string[] Args)
    {
        


        using (var db = new NorthwindContext())
        {

            // T�m m��terilerin city ,country ve region bilgilerini getirin

            //var customers = db.Customers.Select(c => new
            //{
            //    c.City,
            //    c.Region,
            //    c.Country
            //});

            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(customer.Region + "" + customer.Country + "" + customer.City);

            //}


            //city alan�n�n berlin olanlar i�in �sim s�ras�na g�re getirin 

            //var cs = db.Customers
            //    .Where(i => i.City == "Berlin") // kolon filitrelemesi yapt�k , o y�zden ilk where geldi 
            //    .Select(s => new { s.Country })
            //    .ToList();

            //foreach (var item in cs)
            //{
            //    Console.WriteLine(item.Country);
            //}



            // product tab. category�d =1 olanalr�n , productname lerini getiridik .

            //var products = db.Products
            //    .Where(i => i.CategoryId == 1)
            //    .Select(i => i.ProductName)
            //    .ToList(); // liste �eklinde getirdik

            //foreach (var product in products)
            //{
            //    Console.WriteLine(product);
            //    // products liste i�erisinde string de�erler getirir,
            //    // bundan dolay� products  her elaman� producttname kar��l�k gelir . item yazsak yeterli olur.
            //}



            //En son eklenen 5 �r�n bilgisini alal�m 

            //var p = db.Products
            //    .OrderByDescending(i =>i.ProductId)
            //    .Take(5); // En ba�tak 5 �r�n� al�r Take() --> db de soruguda L�M�T ile tan�maln�r .

            ////Ama biz son ekleneni istiyoruz , Yani TABLOYU TERS �EV�RMEL�Y�Z.
            ////OrderbyDescendin() --> azalan s�ralada s�ralar.Yani �okdan aza s�ralar . bu da en sondaki �r�nlere kar��l�k gelir.          

            //foreach (var item in p) 
            //{
            //    Console.WriteLine(item.ProductName);

            //}


            //Fiyat� 10 ile 30 aras�nda olan �r�nlerin , fiyat bilgisini al�caz

            //var pr = db.Products
            //    .Where(p => p.UnitPrice > 10 && p.UnitPrice < 30)
            //    .Select(p => p.UnitPrice)
            //    .ToList();

            //foreach (var item in pr) 
            //{
            //    Console.WriteLine(item);

            //}


            // "Beverages" kategorisindeki �r�nlerin ort fiyat� nedir ? --> 37,9791

            var avg = db.Products
                .Where(i => i.CategoryId == 1)
                .Average(i => i.UnitPrice);

            Console.WriteLine(avg);


         //"Beverages" kat. ka� �r�n vard�r  ? (10 dan b��yk �r�nlerin adetini geitr)

            var count = db.Products.Count(i => i.UnitPrice>10);
            Console.WriteLine(count);

            //"Tea" kelimesi i�eren �r�nleri getiriniz
            var product = db.Products
                .Where(i => i.ProductName.Contains("Tea")) // Contains() --> uzun kelime gruplar�nda , i�inde bulunan k�sa kelimeleri aratmak i�in kullan�l�r , sql de LIKE 'a kar��l�k gelir .
                 .ToList();                                         // ( productName i�in de arat�lmak istenen kelime yaz�l�r)
            foreach (var item in product) 
            {
                Console.WriteLine(item.ProductName);
            }

            // En pahal� �r�n ve en ucuz �r�n hangisidir  ?

            var minPrice = db.Products.Min(i => i.UnitPrice);
            var maxPrice = db.Products.Max(p => p.UnitPrice);
            Console.WriteLine("Min �r�n Fiyat�: {0} Max �r�n Fiyat� :{1}", minPrice,maxPrice);


        }


        using (var db = new NorthwindContext()) 
        {
            var customers = db.Customers
                .Where(i => i.Orders.Count() > 0) // count k�sm� m��teriniz en az 1 kay�d� varsa o m��teri gelir.
                .Select(a=> new 
                { 
                   a.CustomerId,
                   a.CompanyName
                
                })
                .ToList();

            foreach (var item in customers) 
            {
                Console.WriteLine(item);

            }
           
        
        
        }


        // Adres tablosu �zerinden ya da kullan�c� �zerinden ald���m�z bilgilerle , kullan�c�y� veya adresini bulabiliriz.
        //one to many ili�ki t�r� (bire �ok ili�ki  t�r�d�r )

        // NOT: Keylerle ilgili ��yle bir �rnek vermek istiyorum;
        // Ben Product class�nda , e�er prop olarak Id yada ProductId tan�mlarsam; sistem onu direkt PK olarak tan�mlar  , kabul eder.
        // Fakat ben UrunId dersem bunu analayamaz , o y�zden bir  �st sat�r�na "[Key]" yaz�larak belirtilir .

        // Convetion - Bir i�i kolay yoluyla yapabiliyorsak bu convetion'd�r.
        // Data annatations - property �zerinde, kolay olamayan y�ntemler kullanmak . --> mesela Id tan�mlayamad���m�z da kulland���m�z [Key] belirteci .
        //Fluent Api - Alternatifi , ya da biz bunu farkl� yolla da yapabildi�imiz �ekline denir. (hepsine  g�re daha bask�nd�r)


        //TODO: using NEDEN KULLANILIR ? --> using diyerek bir context olu�turuyoruz




        // Bu �rnekte biz bir product'a birden fazla Category atam�� olduk bu �rnekte.
        //using (var db = new ProjectDbContext())
        //{
        //    var p = new Product()
        //    {
        //        Name = "samsung 6",
        //        Price = 5000
        //    };
        //    db.Products.Add(p);

        //    var product = db.Products.FirstOrDefault(p => p.Id == 9);
        //    product.Name = "";
        //    db.Products.Update(product);
        //    db.SaveChanges();


        //    var categories = new List<Category>()
        //    {
        //        new Category(){Name="Telefon"},
        //        new Category(){Name="Bilgisayar"},
        //        new Category(){Name="Elektronik"}

        //    };



        //    int[] ids = new int[2] { 1, 2 }; //2 elemanl� Id arrayi olu�turduk.
        //                                     //kullan�c� (2 tane category se�ecek)

        //    //Find()--> bizden Id bilgisi ister.
        //    var p = db.Products.Find(1); //product id 1 olarak kay�t edilecek .
        //                                 // Bu durumda category�d de�i�ecek (1 ya da 2 olacak.) 

        //    //ProductCategories bizden bir liste bekliyor , biz de bunu Id bilgisi �zerinden yap�caz.
        //    //select ile foreach gibi datalar� d�n�cek.

        //    p.ProductCategories = ids.Select(cid => new ProrductCategory()
        //    {
        //        CategoryId = cid,
        //        ProductId = p.Id
        //    }).ToList();




        //    //Burada 1 �r�ne --> birden fazla kategori atam�� olduk (One to Many Relation)
        //}



        //   //AddProducts() --> VT Kay�t Ekleme
        //   static void AddProducts()
        //        {
        //            using (var db = new ProjectDbContext())
        //            {
        //                var Products = new List<Product>()
        //            {
        //                new Product { Name = "Samsung S6" , Price=4000},
        //                new Product { Name = "Samsung S7" , Price=5000},
        //                new Product { Name = "Samsung S8" , Price=6000},
        //                new Product { Name = "Samsung S9" , Price=7000},
        //                new Product { Name = "Samsung S10" , Price=8000}
        //             };



        //                db.AddRange(Products); //(Add.Products.AddRange() - b�yle de kullan�l�r) list giib collection yani �oklu veri eklemede kullan�l�r .    
        //                                       //db.Add(Products); // vt bu bilgi direkt gitmez Save Changes methodu �a��rmam�z gerekir--> (tek bir veri ekler )
        //                db.SaveChanges();
        //                Console.WriteLine("Veriler eklendi"); // bunu yazmam�z�n sebebi �al���yor mu diye kontrol etmek 


        //                // entity framework core loggin ile AddRange() methodunun sql sorgusuna nas�l d�n��t���n� g�rebilriz
        //                // AddRange() --> Bu method insert sorgusu i�levine d�n���r asl�nda
        //            }

        //        }

        //    //GetAllProducts() --> VT Kay�t Se�me (hepdisini se�er)
        //    static void GetAllProducts()
        //    {
        //        using (var ctext = new ProjectDbContext())
        //        {

        //            var products = ctext.Products.ToList(); //--> get product all

        //            var value = ctext.Products
        //                .Where(products => products.Price > 1000);

        //            foreach (var rslt in value)
        //                Console.WriteLine($"Name:{rslt.Name}  Price:{rslt.Price}");


        //            // context �zerinden t�m Products listesinin referans�n� al�r�z .
        //            //Collection� --> Listeye �eviririz ToList() ile (Bu sayede list yap�s�na �evirilerek VT'n�na sorgu g�ndermi� oluruz .)
        //        }
        //    }


        //    //GetProductsById(int id) --> VT Kay�t Se�me , Id g�re kay�t al�yoruz. ??
        //    static void GetProductsById(int id)
        //    {
        //        using (var context = new ProjectDbContext())
        //        {
        //            var products = context.Products.ToList(); //--> get product all
        //                                                      // context �zerinden t�m Products listesinin referans�n� al�r�z .
        //                                                      //Collection� --> Listeye �eviririz ToList() ile (Bu sayede list yap�s�na �evirilerek VT'n�na sorgu g�ndermi� oluruz .)


        //            // �stedi�imiz kay�d� nas�l �e�eriz (filitreledik) --> Linq
        //            var result = context.Products.Where(products => products.Name.ToLower().Contains("Dilo".ToLower())).Select(products => new
        //            {// ToLOwer() --> yaz�lar� k���k yapar. 
        //                products.Name,
        //                products.Price
        //            }).ToList();  //id k�sm� mrthod i�in de verilmi� olmas� gerekir
        //                          //random id verilir , tablodaki Id ile e�itse name ve price de�erini yazar

        //            foreach (var product in result)
        //                Console.WriteLine($"Name:{product.Name} , Price:{product.Price}");
        //        }
        //    }


        //    //UpdateProduct() --> VT'daki �r�nleri g�ncelleme i�lemi yap�lacak.
        //    static void UpdateProduct()
        //    {
        //        using (var db = new ProjectDbContext())
        //        {

        //            var products = db.Products.Where(i => i.Id == 1).FirstOrDefault(); // select sorgusu

        //            //AsNoTracking() --> products Objesi takip edilmez ,  bu method sayesinde
        //            // ��yle , bir kere %20 zam yap�ld�ktan sonra ikinci kere update ge�ti�imizde
        //            // e�er AsNoTracking() varsa Art�m i�lemini yans�tm�caz . Herhangi bir g�ncelleme de kullanm�caksak kullan�l�r.

        //            if (products != null)
        //            {
        //                products.Price *= 1.2m; // d�nen fiyat null de�ilse; fiyata %20 lik art�� yap.
        //                db.SaveChanges(); //--> VT gitmesini sa�lar

        //                Console.WriteLine("update done");

        //                //Update i�lemi i�in hem bir select hem de update sorgudu �al��t�rm�� olduk.
        //            }


        //            // change tracking --> al�nan �bjenin takibi yap�l�r , de�i�iklik oldu�unda SaveChange() methodu ile VT g�nderlir.
        //            // Bu durumun takibini "Change Tracking" yapar
        //        }

        //        //UpdateProduct() --> Haz�r fonksiyon
        //        using (var db = new ProjectDbContext())
        //        {
        //            var p = db.Products.Where(i => i.Id == 1).FirstOrDefault();

        //            if (p != null)
        //            {
        //                db.Update(p);
        //                db.SaveChanges();

        //                // NOT: Bu method performans a��s�ndan �ok iyi de�il; Nedeni,bir alan i�in g�nderiyoruz Update'ti
        //                //fakat sadece bir kolonu g�ncellemez , di�er t�m kolanlar� da g�nceller. 

        //                // Haz�r Update() methodu 
        //                //UpdateRange() --> birden fazla veriye update etmek istersek , bu methodu kullan�r�z .

        //            }
        //        }

        //    }


        //    //DeleteProduct() --> VT dan veri silme i�lemi 
        //    static void DeleteProduct()
        //    {
        //        using (var db = new ProjectDbContext())
        //        {
        //            var result = db.Products.FirstOrDefault(i => i.Id == 6); //select sorgusu

        //            if (result != null)
        //            {
        //                db.Remove(result);  //db.Products.Remove() 'da aym� i�lemi ger�ekle�tiri
        //                db.SaveChanges();

        //                Console.WriteLine("Data Delete !");
        //            }



        //        }

        //        //DeleteProduct() --> select i�lemi olmadanyap�lan silme i�lemi
        //        //--> Haz�r remove() fonksiyonu
        //        using (var db = new ProjectDbContext())
        //        {

        //            var result = new Product() { Id = 6 };

        //            db.Remove(result);
        //            db.Entry(result).State = EntityState.Deleted;
        //            //ilgili objeyi context �zerinden silme i�lemi .2 sat�rda silme i�lemi yapar


        //            db.SaveChanges();



        //        }
        //    }

        //    static void InsertUsers()
        //    {
        //        var users = new List<User>()
        //{ new User() { UserName="dileksucu",Email="info@sad�kturan.com"},
        //  new User() { UserName="meleksucu",Email="info@sad�kturan.com"},
        //  new User() { UserName="yaseminkaya",Email="info@sad�kturan.com"},
        //  new User() { UserName="mehmety�lmaz",Email="info@sad�kturan.com"},
        //   new User() { UserName="�erife�elik",Email="info@sad�kturan.com"}
        //};

        //        using (var db = new ProjectDbContext())
        //        {
        //            db.AddRange(users);
        //            db.SaveChanges();

        //        }

        //    }

        //    static void InsertAddresses()
        //    {
        //        var addresses = new List<Address>()
        //{ new Address() {FullName="Dilek sucu", Title="ev adresi",Body="Kocaeli",UserId=1},
        //  new Address() {FullName="melek sucu", Title="i� adresi",Body="istanbul",UserId=2},
        //  new Address() {FullName="yasemin kaya", Title="kom�u adresi",Body="malatya",UserId=3},
        //  new Address() {FullName="mehmet y�lmaz", Title="ev adresi",Body="kastamonu",UserId=4},
        //  new Address() {FullName="�erife �elik sucu", Title="�irket adresi",Body="izmir",UserId=5},
        //  new Address() {FullName="Dilek sucu", Title="i� adresi",Body="Kocaeli",UserId=1},
        //  new Address() {FullName="yasemin kaya", Title="i� adresi",Body="malatya",UserId=3}

        ////UserId --> zorunlu Aland�r 
        //};

        //        using (var db = new ProjectDbContext())
        //        {
        //            db.AddRange(addresses);
        //            db.SaveChanges();

        //        }

        //    }


        //    //CustomerAddRange()--->Burada customer alan�na ekleme yapt�k .
        //    using (var db = new ProjectDbContext())
        //    {
        //        var customer = new List<Customer>() {
        //        new Customer {IdentityNumber ="98964553", FirstName="Melek", LastName="Kaya", UserId=2 /* bir kere daha ekleme i�lemi ger�ekle�tirsem  , bu UserId bilgisini kullanamam ��nk� Uniq.*/},
        //        new Customer{IdentityNumber ="45632886", FirstName="Yasmin", LastName="Cihangir", UserId=3}
        //    };
        //        db.AddRange(customer);  // Buraya Customers yazd���m da debug olu�turuyor . !!! 
        //        db.SaveChanges();
        //    }


        //    //UserRelationsCustomerAdd() -->   // ilkili Customer , User  ile ili�kilendirilecek.
        //    //(Bu �u demek bir kullan�c� , ayn� zamanda bir m��teri de olabilir .)
        //    using (var db = new ProjectDbContext())
        //    {
        //        var user = new User()
        //        {
        //            // ilk ba�ta user tablosuna , deneme ad�nda bir kayt eklendi 
        //            UserName = "Deneme",
        //            Email = "deneme@gmail.com",
        //            Customer = new Customer()
        //            {
        //                //daha sonra bu m��teri ile ili�kilendirildi --> yani customera eklendi
        //                FirstName = "deneme",
        //                LastName = "deneme",
        //                IdentityNumber = "13225465478",
        //            }

        //        };

        //        db.Add(user);
        //        db.SaveChanges();



        //    }


        //    //Not : Many to Many Tablolar�nda arada bir birle�tirme tablosu olu�turulur. (entity tablosu.)
        //    // ili�kilendirilen 2 tablonun , Id'leri birle�tirme tablosunda olu�turulacak .



    }
}