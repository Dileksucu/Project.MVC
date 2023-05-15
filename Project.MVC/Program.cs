using DataAccess;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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


//TODO: using NEDEN KULLANILIR ? --> using diyerek bir context oluþturuhyoruz

//AddProducts() --> VT Kayýt Ekleme
using (var db = new ProjectDbContext())
{
    var Products = new List<Product>()
        {
       new Product { Name = "Samsung S6" , Price=4000},
       new Product { Name = "Samsung S7" , Price=5000},
       new Product { Name = "Samsung S8" , Price=6000},
       new Product { Name = "Samsung S9" , Price=7000},
       new Product { Name = "Samsung S10" , Price=8000}
        };



    db.AddRange(Products); //(Add.Products.AddRange() - böyle de kullanýlýr) list giib collection yani çoklu veri eklemede kullanýlýr .    
                           //db.Add(Products); // vt bu bilgi direkt gitmez Save Changes methodu çaðýrmamýz gerekir--> (tek bir veri ekler )
    db.SaveChanges();
    Console.WriteLine("Veriler eklendi"); // bunu yazmamýzýn sebebi çalýþýyor mu diye kontrol etmek 


    // entity framework core loggin ile AddRange() methodunun sql sorgusuna nasýl dönüþtüðünü görebilriz
    // AddRange() --> Bu method insert sorgusu iþlevine dönüþür aslýnda
}



//GetAllProducts() --> VT Kayýt Seçme (hepdisini seçer)
using (var ctext = new ProjectDbContext())
{

    var products = ctext.Products.ToList(); //--> get product all

    var value = ctext.Products
        .Where(products => products.Price > 1000 );

    foreach (var rslt in value)
        Console.WriteLine($"Name:{rslt.Name}  Price:{rslt.Price}");


    // context üzerinden tüm Products listesinin referansýný alýrýz .
    //Collectioný --> Listeye çeviririz ToList() ile (Bu sayede list yapýsýna çevirilerek VT'nýna sorgu göndermiþ oluruz .)
}



//GetProductsById(int id) --> VT Kayýt Seçme , Id göre kayýt alýyoruz. ??
using (var context = new ProjectDbContext())
{
    var products = context.Products.ToList(); //--> get product all
    // context üzerinden tüm Products listesinin referansýný alýrýz .
    //Collectioný --> Listeye çeviririz ToList() ile (Bu sayede list yapýsýna çevirilerek VT'nýna sorgu göndermiþ oluruz .)


    // Ýstediðimiz kayýdý nasýl þeçeriz (filitreledik) --> Linq
    var result = context.Products.Where(products => products.Name.ToLower().Contains("Dilo".ToLower())).Select(products => new
    {// ToLOwer() --> yazýlarý küçük yapar. 
        products.Name,
        products.Price 
    }).ToList();  //id kýsmý mrthod için de verilmiþ olmasý gerekir
         //random id verilir , tablodaki Id ile eþitse name ve price deðerini yazar

    foreach (var product in result)
        Console.WriteLine($"Name:{product.Name} , Price:{product.Price}");
}



//UpdateProduct() --> VT'daki ürünleri güncelleme iþlemi yapýlacak.
using (var db = new ProjectDbContext())
{ 
  
    var products = db.Products.Where(i=>i.Id==1).FirstOrDefault(); // select sorgusu

    //AsNoTracking() --> products Objesi takip edilmez ,  bu method sayesinde
    // þöyle , bir kere %20 zam yapýldýktan sonra ikinci kere update geçtiðimizde
    // eðer AsNoTracking() varsa Artým iþlemini yansýtmýcaz . Herhangi bir güncelleme de kullanmýcaksak kullanýlýr.

    if (products != null)
    {
        products.Price *= 1.2m; // dönen fiyat null deðilse; fiyata %20 lik artýþ yap.
        db.SaveChanges(); //--> VT gitmesini saðlar

        Console.WriteLine("update done");

            //Update iþlemi için hem bir select hem de update sorgudu çalýþtýrmýþ olduk.
    }
    

    // change tracking --> alýnan öbjenin takibi yapýlýr , deðiþiklik olduðunda SaveChange() methodu ile VT gönderlir.
    // Bu durumun takibini "Change Tracking" yapar
}

//UpdateProduct()
using (var db = new ProjectDbContext())
{
    var p = db.Products.Where(i => i.Id == 1).FirstOrDefault();
    
    if (p != null) 
    {
        db.Update(p);
        db.SaveChanges();
        
        // NOT: Bu method performans açýsýndan çok iyi deðil; Nedeni,bir alan için gönderiyoruz Update'ti
        //fakat sadece bir kolonu güncellemez , diðer tüm kolanlarý da günceller. 

        // Hazýr Update() methodu 
        //UpdateRange() --> birden fazla veriye update etmek istersek , bu methodu kullanýrýz .

    }







}


//DeleteProduct() --> VT dan veri silme iþlemi 
using (var db = new ProjectDbContext()) 
{
    var result = db.Products.FirstOrDefault(i=>i.Id==6); //select sorgusu

    if (result != null) 
    {
        db.Remove(result);  //db.Products.Remove() 'da aymý iþlemi gerçekleþtiri
        db.SaveChanges();

        Console.WriteLine( "Data Delete !" );
    }



}

//DeleteProduct() --> select iþlemi olmadanyapýlan silme iþlemi 
using (var db = new ProjectDbContext())
{

    var result = new Product() { Id = 6 };

    db.Remove(result);
    db.Entry(result).State = EntityState.Deleted; 
    //ilgili objeyi context üzerinden silme iþlemi .2 satýrda silme iþlemi yapar

    
    db.SaveChanges();   
    


}
