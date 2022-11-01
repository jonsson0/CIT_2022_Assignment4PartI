using DataLayer.Models;
using System.Collections.Generic;
using DataLayer.Model;
using DataLayer.DummyModel;

namespace DataLayer
{
    public interface IDataService
    {
        List<Category> GetCategories();
        Category? GetCategory(int id);
        List<Product> GetProducts();
        Product? GetProduct(int id);
        List<Product_getProductByCategory_Model>? GetProductByCategory(int id);

        Category CreateCategory(string name, string desc);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);

        bool UpdateCategory(int id, string name, string desc);
        bool DeleteCategory(int id);

        List<ProductSearchModel> GetProductByName(string search);
    }
}