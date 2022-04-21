namespace WebShopAPI.Models.Forms
{
    public class CategoryForm
    {
        public string Name { get; set; }
        public CategoryForm()
        {

        }
        public CategoryForm(string name)
        {
            Name = name;
        }

    }
}
