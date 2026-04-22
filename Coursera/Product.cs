using Newtonsoft.Json;

public class Product
{
    public string Name{get; set;}
    public decimal Price {get; set;}
    public List<string> Tags {get; set;}
}

public class Program
{
    static void Main(){
        string json = "{\"Name\":\"Laptops\", \"Price\": 999.99, \"Tags\": [\"Electronics\",\"Computers\"]}";
        Product product = JsonConvert.DeserializeObject<Product>(json);
        Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Tags: {string.Join(", " , product.Tags)}");


        Product newproduct = new Product
        {
            Name = "Smartphone",
            Price = 999.99m,
            Tags = new List<string>{"Electronics", "Mobile"},
        };

        string newjson =  JsonConvert.SerializeObject(newproduct, Formatting.Indented);
        Console.WriteLine($"Serialised Object:  \n{newjson}");
    }
}
