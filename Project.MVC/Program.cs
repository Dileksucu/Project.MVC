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

            // Tüm müþterilerin city ,country ve region bilgilerini getirin

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


            //city alanýnýn berlin olanlar için Ýsim sýrasýna göre getirin 

            //var cs = db.Customers
            //    .Where(i => i.City == "Berlin") // kolon filitrelemesi yaptýk , o yüzden ilk where geldi 
            //    .Select(s => new { s.Country })
            //    .ToList();

            //foreach (var item in cs)
            //{
            //    Console.WriteLine(item.Country);
            //}



            // product tab. categoryýd =1 olanalrýn , productname lerini getiridik .

            //var products = db.Products
            //    .Where(i => i.CategoryId == 1)
            //    .Select(i => i.ProductName)
            //    .ToList(); // liste þeklinde getirdik

            //foreach (var product in products)
            //{
            //    Console.WriteLine(product);
            //    // products liste içerisinde string deðerler getirir,
            //    // bundan dolayý products  her elamaný producttname karþýlýk gelir . item yazsak yeterli olur.
            //}



            //En son eklenen 5 ürün bilgisini alalým 

            //var p = db.Products
            //    .OrderByDescending(i =>i.ProductId)
            //    .Take(5); // En baþtak 5 ürünü alýr Take() --> db de soruguda LÝMÝT ile tanýmalnýr .

            ////Ama biz son ekleneni istiyoruz , Yani TABLOYU TERS ÇEVÝRMELÝYÝZ.
            ////OrderbyDescendin() --> azalan sýralada sýralar.Yani çokdan aza sýralar . bu da en sondaki ürünlere karþýlýk gelir.          

            //foreach (var item in p) 
            //{
            //    Console.WriteLine(item.ProductName);

            //}


            //Fiyatý 10 ile 30 arasýnda olan ürünlerin , fiyat bilgisini alýcaz

            //var pr = db.Products
            //    .Where(p => p.UnitPrice > 10 && p.UnitPrice < 30)
            //    .Select(p => p.UnitPrice)
            //    .ToList();

            //foreach (var item in pr) 
            //{
            //    Console.WriteLine(item);

            //}


            // "Beverages" kategorisindeki ürünlerin ort fiyatý nedir ? --> 37,9791

            var avg = db.Products
                .Where(i => i.CategoryId == 1)
                .Average(i => i.UnitPrice);

            Console.WriteLine(avg);


         //"Beverages" kat. kaç ürün vardýr  ? (10 dan büüyk ürünlerin adetini geitr)

            var count = db.Products.Count(i => i.UnitPrice>10);
            Console.WriteLine(count);

            //"Tea" kelimesi içeren ürünleri getiriniz
            var product = db.Products
                .Where(i => i.ProductName.Contains("Tea")) // Contains() --> uzun kelime gruplarýnda , içinde bulunan kýsa kelimeleri aratmak için kullanýlýr , sql de LIKE 'a karþýlýk gelir .
                 .ToList();                                         // ( productName için de aratýlmak istenen kelime yazýlýr)
            foreach (var item in product) 
            {
                Console.WriteLine(item.ProductName);
            }

            // En pahalý ürün ve en ucuz ürün hangisidir  ?

            var minPrice = db.Products.Min(i => i.UnitPrice);
            var maxPrice = db.Products.Max(p => p.UnitPrice);
            Console.WriteLine("Min Ürün Fiyatý: {0} Max Ürün Fiyatý :{1}", minPrice,maxPrice);


        }


        using (var db = new NorthwindContext()) 
        {
            var customers = db.Customers
                .Where(i => i.Orders.Count() > 0) // count kýsmý müþteriniz en az 1 kayýdý varsa o müþteri gelir.
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


        // Adres tablosu üzerinden ya da kullanýcý üzerinden aldýðýmýz bilgilerle , kullanýcýyý veya adresini bulabiliriz.
        //one to many iliþki türü (bire çok iliþki  türüdür )

        // NOT: Keylerle ilgili þöyle bir örnek vermek istiyorum;
        // Ben Product classýnda , eðer prop olarak Id yada ProductId tanýmlarsam; sistem onu direkt PK olarak tanýmlar  , kabul eder.
        // Fakat ben UrunId dersem bunu analayamaz , o yüzden bir  üst satýrýna "[Key]" yazýlarak belirtilir .

        // Convetion - Bir iþi kolay yoluyla yapabiliyorsak bu convetion'dýr.
        // Data annatations - property üzerinde, kolay olamayan yöntemler kullanmak . --> mesela Id tanýmlayamadýðýmýz da kullandýðýmýz [Key] belirteci .
        //Fluent Api - Alternatifi , ya da biz bunu farklý yolla da yapabildiðimiz þekline denir. (hepsine  göre daha baskýndýr)


        //TODO: using NEDEN KULLANILIR ? --> using diyerek bir context oluþturuyoruz




        // Bu örnekte biz bir product'a birden fazla Category atamýþ olduk bu örnekte.
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



        //    int[] ids = new int[2] { 1, 2 }; //2 elemanlý Id arrayi oluþturduk.
        //                                     //kullanýcý (2 tane category seçecek)

        //    //Find()--> bizden Id bilgisi ister.
        //    var p = db.Products.Find(1); //product id 1 olarak kayýt edilecek .
        //                                 // Bu durumda categoryýd deðiþecek (1 ya da 2 olacak.) 

        //    //ProductCategories bizden bir liste bekliyor , biz de bunu Id bilgisi üzerinden yapýcaz.
        //    //select ile foreach gibi datalarý dönücek.

        //    p.ProductCategories = ids.Select(cid => new ProrductCategory()
        //    {
        //        CategoryId = cid,
        //        ProductId = p.Id
        //    }).ToList();




        //    //Burada 1 ürüne --> birden fazla kategori atamýþ olduk (One to Many Relation)
        //}



        //   //AddProducts() --> VT Kayýt Ekleme
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



        //                db.AddRange(Products); //(Add.Products.AddRange() - böyle de kullanýlýr) list giib collection yani çoklu veri eklemede kullanýlýr .    
        //                                       //db.Add(Products); // vt bu bilgi direkt gitmez Save Changes methodu çaðýrmamýz gerekir--> (tek bir veri ekler )
        //                db.SaveChanges();
        //                Console.WriteLine("Veriler eklendi"); // bunu yazmamýzýn sebebi çalýþýyor mu diye kontrol etmek 


        //                // entity framework core loggin ile AddRange() methodunun sql sorgusuna nasýl dönüþtüðünü görebilriz
        //                // AddRange() --> Bu method insert sorgusu iþlevine dönüþür aslýnda
        //            }

        //        }

        //    //GetAllProducts() --> VT Kayýt Seçme (hepdisini seçer)
        //    static void GetAllProducts()
        //    {
        //        using (var ctext = new ProjectDbContext())
        //        {

        //            var products = ctext.Products.ToList(); //--> get product all

        //            var value = ctext.Products
        //                .Where(products => products.Price > 1000);

        //            foreach (var rslt in value)
        //                Console.WriteLine($"Name:{rslt.Name}  Price:{rslt.Price}");


        //            // context üzerinden tüm Products listesinin referansýný alýrýz .
        //            //Collectioný --> Listeye çeviririz ToList() ile (Bu sayede list yapýsýna çevirilerek VT'nýna sorgu göndermiþ oluruz .)
        //        }
        //    }


        //    //GetProductsById(int id) --> VT Kayýt Seçme , Id göre kayýt alýyoruz. ??
        //    static void GetProductsById(int id)
        //    {
        //        using (var context = new ProjectDbContext())
        //        {
        //            var products = context.Products.ToList(); //--> get product all
        //                                                      // context üzerinden tüm Products listesinin referansýný alýrýz .
        //                                                      //Collectioný --> Listeye çeviririz ToList() ile (Bu sayede list yapýsýna çevirilerek VT'nýna sorgu göndermiþ oluruz .)


        //            // Ýstediðimiz kayýdý nasýl þeçeriz (filitreledik) --> Linq
        //            var result = context.Products.Where(products => products.Name.ToLower().Contains("Dilo".ToLower())).Select(products => new
        //            {// ToLOwer() --> yazýlarý küçük yapar. 
        //                products.Name,
        //                products.Price
        //            }).ToList();  //id kýsmý mrthod için de verilmiþ olmasý gerekir
        //                          //random id verilir , tablodaki Id ile eþitse name ve price deðerini yazar

        //            foreach (var product in result)
        //                Console.WriteLine($"Name:{product.Name} , Price:{product.Price}");
        //        }
        //    }


        //    //UpdateProduct() --> VT'daki ürünleri güncelleme iþlemi yapýlacak.
        //    static void UpdateProduct()
        //    {
        //        using (var db = new ProjectDbContext())
        //        {

        //            var products = db.Products.Where(i => i.Id == 1).FirstOrDefault(); // select sorgusu

        //            //AsNoTracking() --> products Objesi takip edilmez ,  bu method sayesinde
        //            // þöyle , bir kere %20 zam yapýldýktan sonra ikinci kere update geçtiðimizde
        //            // eðer AsNoTracking() varsa Artým iþlemini yansýtmýcaz . Herhangi bir güncelleme de kullanmýcaksak kullanýlýr.

        //            if (products != null)
        //            {
        //                products.Price *= 1.2m; // dönen fiyat null deðilse; fiyata %20 lik artýþ yap.
        //                db.SaveChanges(); //--> VT gitmesini saðlar

        //                Console.WriteLine("update done");

        //                //Update iþlemi için hem bir select hem de update sorgudu çalýþtýrmýþ olduk.
        //            }


        //            // change tracking --> alýnan öbjenin takibi yapýlýr , deðiþiklik olduðunda SaveChange() methodu ile VT gönderlir.
        //            // Bu durumun takibini "Change Tracking" yapar
        //        }

        //        //UpdateProduct() --> Hazýr fonksiyon
        //        using (var db = new ProjectDbContext())
        //        {
        //            var p = db.Products.Where(i => i.Id == 1).FirstOrDefault();

        //            if (p != null)
        //            {
        //                db.Update(p);
        //                db.SaveChanges();

        //                // NOT: Bu method performans açýsýndan çok iyi deðil; Nedeni,bir alan için gönderiyoruz Update'ti
        //                //fakat sadece bir kolonu güncellemez , diðer tüm kolanlarý da günceller. 

        //                // Hazýr Update() methodu 
        //                //UpdateRange() --> birden fazla veriye update etmek istersek , bu methodu kullanýrýz .

        //            }
        //        }

        //    }


        //    //DeleteProduct() --> VT dan veri silme iþlemi 
        //    static void DeleteProduct()
        //    {
        //        using (var db = new ProjectDbContext())
        //        {
        //            var result = db.Products.FirstOrDefault(i => i.Id == 6); //select sorgusu

        //            if (result != null)
        //            {
        //                db.Remove(result);  //db.Products.Remove() 'da aymý iþlemi gerçekleþtiri
        //                db.SaveChanges();

        //                Console.WriteLine("Data Delete !");
        //            }



        //        }

        //        //DeleteProduct() --> select iþlemi olmadanyapýlan silme iþlemi
        //        //--> Hazýr remove() fonksiyonu
        //        using (var db = new ProjectDbContext())
        //        {

        //            var result = new Product() { Id = 6 };

        //            db.Remove(result);
        //            db.Entry(result).State = EntityState.Deleted;
        //            //ilgili objeyi context üzerinden silme iþlemi .2 satýrda silme iþlemi yapar


        //            db.SaveChanges();



        //        }
        //    }

        //    static void InsertUsers()
        //    {
        //        var users = new List<User>()
        //{ new User() { UserName="dileksucu",Email="info@sadýkturan.com"},
        //  new User() { UserName="meleksucu",Email="info@sadýkturan.com"},
        //  new User() { UserName="yaseminkaya",Email="info@sadýkturan.com"},
        //  new User() { UserName="mehmetyýlmaz",Email="info@sadýkturan.com"},
        //   new User() { UserName="þerifeçelik",Email="info@sadýkturan.com"}
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
        //  new Address() {FullName="melek sucu", Title="iþ adresi",Body="istanbul",UserId=2},
        //  new Address() {FullName="yasemin kaya", Title="komþu adresi",Body="malatya",UserId=3},
        //  new Address() {FullName="mehmet yýlmaz", Title="ev adresi",Body="kastamonu",UserId=4},
        //  new Address() {FullName="þerife çelik sucu", Title="þirket adresi",Body="izmir",UserId=5},
        //  new Address() {FullName="Dilek sucu", Title="iþ adresi",Body="Kocaeli",UserId=1},
        //  new Address() {FullName="yasemin kaya", Title="iþ adresi",Body="malatya",UserId=3}

        ////UserId --> zorunlu Alandýr 
        //};

        //        using (var db = new ProjectDbContext())
        //        {
        //            db.AddRange(addresses);
        //            db.SaveChanges();

        //        }

        //    }


        //    //CustomerAddRange()--->Burada customer alanýna ekleme yaptýk .
        //    using (var db = new ProjectDbContext())
        //    {
        //        var customer = new List<Customer>() {
        //        new Customer {IdentityNumber ="98964553", FirstName="Melek", LastName="Kaya", UserId=2 /* bir kere daha ekleme iþlemi gerçekleþtirsem  , bu UserId bilgisini kullanamam çünkü Uniq.*/},
        //        new Customer{IdentityNumber ="45632886", FirstName="Yasmin", LastName="Cihangir", UserId=3}
        //    };
        //        db.AddRange(customer);  // Buraya Customers yazdýðým da debug oluþturuyor . !!! 
        //        db.SaveChanges();
        //    }


        //    //UserRelationsCustomerAdd() -->   // ilkili Customer , User  ile iliþkilendirilecek.
        //    //(Bu þu demek bir kullanýcý , ayný zamanda bir müþteri de olabilir .)
        //    using (var db = new ProjectDbContext())
        //    {
        //        var user = new User()
        //        {
        //            // ilk baþta user tablosuna , deneme adýnda bir kayt eklendi 
        //            UserName = "Deneme",
        //            Email = "deneme@gmail.com",
        //            Customer = new Customer()
        //            {
        //                //daha sonra bu müþteri ile iliþkilendirildi --> yani customera eklendi
        //                FirstName = "deneme",
        //                LastName = "deneme",
        //                IdentityNumber = "13225465478",
        //            }

        //        };

        //        db.Add(user);
        //        db.SaveChanges();



        //    }


        //    //Not : Many to Many Tablolarýnda arada bir birleþtirme tablosu oluþturulur. (entity tablosu.)
        //    // iliþkilendirilen 2 tablonun , Id'leri birleþtirme tablosunda oluþturulacak .



    }
}