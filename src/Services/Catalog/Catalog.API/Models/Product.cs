namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = default!;
        public List<string> Category { get; set; } = new();

        public string Descreption { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; } 

    }
}
