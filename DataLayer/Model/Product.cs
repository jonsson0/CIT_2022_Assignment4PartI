using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using DataLayer.DummyModel;

namespace DataLayer.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        
        public Category? Category { get; set; }

        /*
        [NotMapped]
        public string CategoryName { get; set; }
        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public DummyProduct Nameholder { get; set; }
        */

        /*
        [NotMapped]
        public string CategoryName   
        {
            get { return Nameholder.name; }
            set { CategoryName = value; }
        }
        [NotMapped]
        public Nameholder Nameholder { get; set; }
        */


        public string? QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}