using System;
using System.Collections.Generic;

namespace CSharpBasics
{
    public interface IDiscountable
    {
        //leaving this in the interface allows flexibility - each class now can develop its own Discount function.
        //Any thing wih this interface says: Can be Discounted.
        decimal ApplyDiscount(decimal percentage);
    }

    //Anything that inherits the class says, so-so is a Product,
    public class Product
    {
        private decimal _price;
        public string Name { get; set; }
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
            }
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public virtual void DisplayProductDetails()
        {
            Console.WriteLine($"Product: {Name}, Price: {Price:C}");
        }

        public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
        {
            return price - (price * (discountPercentage / 100));
        }
    }

    public class Clothing : Product, IDiscountable
    {
        public int Size { get; set; }

        //So base refers to the immediate parent, and looks for a constructor that takes arguments (string, decimal).
        //that's how it knows its Product here.
        public Clothing(string name, decimal price, int size) : base(name, price)
        {
            Size = size;
        }

        public string GetSize()
        {
            switch (Size)
            {
                case 1:
                    return "SM";
                case 2:
                    return "MD";
                case 3:
                    return "LG";
                default:
                    return "Unknown Size";
            }
        }

        public override void DisplayProductDetails()
        {
            base.DisplayProductDetails();
            Console.WriteLine($"Size: {GetSize()}");
        }

        public decimal ApplyDiscount(decimal percentage)
        {
            return CalculateDiscount(Price, percentage);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Clothing> catalog = new List<Clothing>
            {
                new Clothing("T-Shirt", 19.99m, 1),
                new Clothing("Hoodie", 39.99m, 2),
                new Clothing("Jacket", 79.99m, 3)
            };

            Console.WriteLine("Displaying catalog with for loop:");
            for (int i = 0; i < catalog.Count; i++)
            {
                catalog[i].DisplayProductDetails();
                Console.WriteLine();
            }

            Console.WriteLine("Displaying catalog with foreach loop:");
            foreach (Clothing item in catalog)
            {
                item.DisplayProductDetails();
                Console.WriteLine();
            }

            decimal discountedPrice = catalog[0].ApplyDiscount(10);
            Console.WriteLine($"Discounted price for first item: {discountedPrice:C}");

            decimal calculatedDiscount = Product.CalculateDiscount(50m, 15m);
            Console.WriteLine($"Calculated discount using static method: {calculatedDiscount:C}");
        }
    }
}
