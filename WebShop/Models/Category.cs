namespace WebShopAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
