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


//TODO: using NEDEN KULLANILIR ? --> using diyerek bir context olu�turuhyoruz

//AddProducts() --> VT Kay�t Ekleme
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



    db.AddRange(Products); //(Add.Products.AddRange() - b�yle de kullan�l�r) list giib collection yani �oklu veri eklemede kullan�l�r .    
                           //db.Add(Products); // vt bu bilgi direkt gitmez Save Changes methodu �a��rmam�z gerekir--> (tek bir veri ekler )
    db.SaveChanges();
    Console.WriteLine("Veriler eklendi"); // bunu yazmam�z�n sebebi �al���yor mu diye kontrol etmek 


    // entity framework core loggin ile AddRange() methodunun sql sorgusuna nas�l d�n��t���n� g�rebilriz
    // AddRange() --> Bu method insert sorgusu i�levine d�n���r asl�nda
}



//GetAllProducts() --> VT Kay�t Se�me (hepdisini se�er)
using (var ctext = new ProjectDbContext())
{

    var products = ctext.Products.ToList(); //--> get product all

    var value = ctext.Products
        .Where(products => products.Price > 1000 );

    foreach (var rslt in value)
        Console.WriteLine($"Name:{rslt.Name}  Price:{rslt.Price}");


    // context �zerinden t�m Products listesinin referans�n� al�r�z .
    //Collection� --> Listeye �eviririz ToList() ile (Bu sayede list yap�s�na �evirilerek VT'n�na sorgu g�ndermi� oluruz .)
}



//GetProductsById(int id) --> VT Kay�t Se�me , Id g�re kay�t al�yoruz. ??
using (var context = new ProjectDbContext())
{
    var products = context.Products.ToList(); //--> get product all
    // context �zerinden t�m Products listesinin referans�n� al�r�z .
    //Collection� --> Listeye �eviririz ToList() ile (Bu sayede list yap�s�na �evirilerek VT'n�na sorgu g�ndermi� oluruz .)


    // �stedi�imiz kay�d� nas�l �e�eriz (filitreledik) --> Linq
    var result = context.Products.Where(products => products.Name.ToLower().Contains("Dilo".ToLower())).Select(products => new
    {// ToLOwer() --> yaz�lar� k���k yapar. 
        products.Name,
        products.Price 
    }).ToList();  //id k�sm� mrthod i�in de verilmi� olmas� gerekir
         //random id verilir , tablodaki Id ile e�itse name ve price de�erini yazar

    foreach (var product in result)
        Console.WriteLine($"Name:{product.Name} , Price:{product.Price}");
}



//UpdateProduct() --> VT'daki �r�nleri g�ncelleme i�lemi yap�lacak.
using (var db = new ProjectDbContext())
{ 
  
    var products = db.Products.Where(i=>i.Id==1).FirstOrDefault(); // select sorgusu

    //AsNoTracking() --> products Objesi takip edilmez ,  bu method sayesinde
    // ��yle , bir kere %20 zam yap�ld�ktan sonra ikinci kere update ge�ti�imizde
    // e�er AsNoTracking() varsa Art�m i�lemini yans�tm�caz . Herhangi bir g�ncelleme de kullanm�caksak kullan�l�r.

    if (products != null)
    {
        products.Price *= 1.2m; // d�nen fiyat null de�ilse; fiyata %20 lik art�� yap.
        db.SaveChanges(); //--> VT gitmesini sa�lar

        Console.WriteLine("update done");

            //Update i�lemi i�in hem bir select hem de update sorgudu �al��t�rm�� olduk.
    }
    

    // change tracking --> al�nan �bjenin takibi yap�l�r , de�i�iklik oldu�unda SaveChange() methodu ile VT g�nderlir.
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
        
        // NOT: Bu method performans a��s�ndan �ok iyi de�il; Nedeni,bir alan i�in g�nderiyoruz Update'ti
        //fakat sadece bir kolonu g�ncellemez , di�er t�m kolanlar� da g�nceller. 

        // Haz�r Update() methodu 
        //UpdateRange() --> birden fazla veriye update etmek istersek , bu methodu kullan�r�z .

    }







}


//DeleteProduct() --> VT dan veri silme i�lemi 
using (var db = new ProjectDbContext()) 
{
    var result = db.Products.FirstOrDefault(i=>i.Id==6); //select sorgusu

    if (result != null) 
    {
        db.Remove(result);  //db.Products.Remove() 'da aym� i�lemi ger�ekle�tiri
        db.SaveChanges();

        Console.WriteLine( "Data Delete !" );
    }



}

//DeleteProduct() --> select i�lemi olmadanyap�lan silme i�lemi 
using (var db = new ProjectDbContext())
{

    var result = new Product() { Id = 6 };

    db.Remove(result);
    db.Entry(result).State = EntityState.Deleted; 
    //ilgili objeyi context �zerinden silme i�lemi .2 sat�rda silme i�lemi yapar

    
    db.SaveChanges();   
    


}
