using DataLayer;

var ds = new DataService();

var list = ds.GetOrderDetailsByProductId(1);

Console.WriteLine("here:");
Console.WriteLine(list.First().Order?.Date.ToString());

// .Date.ToString("yyyy-MM-dd")

foreach (var orderdetail in list)
{
    Console.WriteLine(orderdetail.Order?.Id);

}