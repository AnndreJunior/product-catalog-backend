namespace ProductCatalog.Api.Models;

public class Category
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public IEnumerable<Product> Products { get; set; } = [];
}
