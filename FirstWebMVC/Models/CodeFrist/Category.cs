public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property (1 -> nhiều)
    public ICollection<Product> Products { get; set; }
}