namespace BlogEngine.Client.Areas.API.ViewModels
{
    using BlogEngine.Models;

    public class CategoryViewModel : Category
    {
        public bool IsSelected { get; set; }

        public CategoryViewModel(Category cat)
        {
            Id = cat.Id;
            Name = cat.Name;
        }
    }
}
