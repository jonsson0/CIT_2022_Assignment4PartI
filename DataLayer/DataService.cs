using DataLayer.DummyModel;
using DataLayer.Model;
using DataLayer.Models;
using EF;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DataService : IDataService
    {
        NorthwindContext db = new NorthwindContext();

        /* CATEGORY METHODS: */
        public List<Category> GetCategories()
        {
            using var db = new NorthwindContext();

            return db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return db.Categories.Find(id);
        }

        /*
        public Category CreateCategory(string name)
        {
            Category category = new Category();
            category.Id = GetCategories().Count + 1;
            category.Name = name;
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }
        */
        
        public Category CreateCategory(Category category)
        {
            var maxId = db.Categories.Max(x => x.Id);
            category.Id = maxId + 1;
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }
        
        public Category CreateCategory(string name, string desc)
        {
            Category category = new Category();
            category.Name = name;
            category.Description = desc;
            var maxId = db.Categories.Max(x => x.Id);
            category.Id = maxId + 1;
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
        public Category UpdateCategory(Category category)
        {
            return null;
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

        public List<Product> GetProducts()
        {
            using var db = new NorthwindContext();

            return db.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            var productList = db
                .Products
                .Include(x => x.Category)
                .Where(x => x.Id == id).ToList();

            foreach (var product in productList)
            {
                return productList.First();
            }
            
            return null;
        }

        public List<Product_getProductByCategory_Model> GetProductByCategory(int id)
        {
            var list = db
                .Products
                .Include(x => x.Category)
                .Where(x => x.CategoryId == id).ToList();

            var fakeProductList = new List<Product_getProductByCategory_Model>();

            foreach (var product in list)
            {
                var fakeProduct = new Product_getProductByCategory_Model();

                fakeProduct.Name = product.Name;
                fakeProduct.CategoryName = product.Category.Name;

                fakeProductList.Add(fakeProduct);
            }
            return fakeProductList;
        }

        public List<ProductSearchModel> GetProductByName(string name)
        {
            var list = db
                .Products
                .Include(x => x.Category)
                .Where(x => x.Name.Contains(name)).ToList();

            var dummyProductList = new List<ProductSearchModel>();

            foreach (var product in list)
            {
                var dummyProduct = new ProductSearchModel();
                dummyProduct.ProductName = product.Name;

                dummyProductList.Add(dummyProduct);
            }
            return dummyProductList;
        }

        public Order GetOrder(int id)
        {
            Console.WriteLine("we in");
            var orderList = db
                .Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Where(x => x.Id == id).ToList();

            // var x = db.Orders.Find(id);

            return orderList.First();
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
