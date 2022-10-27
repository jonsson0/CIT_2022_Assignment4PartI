using System.Collections;
using System.Security.Cryptography.X509Certificates;
using DataLayer.Model;
using EF;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DataService
    {
        NorthwindContext db = new NorthwindContext();


        /* CATEGORY METHODS: */
        public IList<Category> GetCategories()
        {
            using var db = new NorthwindContext();

            return db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return db.Categories.Find(id);
        }

        public Category CreateCategory(string name)
        {
            Category category = new Category();
            category.Id = GetCategories().Count + 1;
            category.Name = name;
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }

        public Category CreateCategory(string name, string desc)
        {
            Category category = new Category();
            category.Id = GetCategories().Count + 1;
            category.Name = name;
            category.Description = desc;
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var c = db.Categories.Find(id);
            if (c != null)
            {
                db.Categories.Remove(c);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCategory(int id, string name, string desc)
        {
            var c = db.Categories.Find(id);

            if (c != null)
            {
                c.Name = name;
                c.Description = desc;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /* PRODUCT METHODS */

        public Product GetProduct(int id)
        {

            var productList = db
                                                 .Products
                                                 .Include(x => x.Category)
                                                 .Where(x => x.Id == id);
            return productList.ToList().First();
        }

        public List<Product> GetProductByCategory(int id)
        {
            var list = db
                .Products
                .Include(x => x.Category)
                .Where(x => x.CategoryId == id).ToList();

           var Nameholder = new Nameholder();
       
            foreach (var product in list)
            {
                product.Nameholder = Nameholder;
                product.Nameholder.Name = product.Category.Name;
                product.CategoryName = product.Nameholder.Name;
            }
            return list;
        }

        public List<Product> GetProductByName(string name)
        {

            var list = db
                .Products
                .Include(x => x.Category)
                .Where(x => x.Name.Contains(name)).ToList();

            var Nameholder = new Nameholder();

            foreach (var product in list)
            {
                product.Nameholder = Nameholder;
                product.Nameholder.Name = product.Name;
                product.ProductName = product.Nameholder.Name;
            }

            return list;
        }

        public Order GetOrder(int id)
        {
            Console.WriteLine("we in");
            var orderList = db
                .Orders
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(x => x.Category)
                .Where(x => x.Id == id);

           // var x = db.Orders.Find(id);

            return orderList.ToList().First();
        }

        public List<Order> GetOrders()
        {
            var orderList = db
                .Orders
                .Include(x => x.OrderDetails).ToList();

            return orderList;
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
        {

            var orderDetailsList = db
                .OrderDetails
                .Include(x => x.Product)
                .Where(x => x.Order.Id == id).ToList();
            return orderDetailsList;
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            var orderDetailsList = db
                .OrderDetails
                .Include(x => x.Order)
                .Where(x => x.ProductId == id)
                .OrderBy(x => x.OrderId).ToList();
            return orderDetailsList;
        }
    }
}
