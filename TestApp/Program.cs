using DataLayer;

var ds = new DataService();

var list = ds.GetProductByCategory(1);

Console.WriteLine("here:");
Console.WriteLine(list.First());

// .Date.ToString("yyyy-MM-dd")

foreach (var dummyProduct in list)
{
    Console.WriteLine(dummyProduct.Name);

}