public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Khóa ngoại
    public int CategoryId { get; set; }

    // Navigation property
    public Category Category { get; set; }
}