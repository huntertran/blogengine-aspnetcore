namespace BlogEngine.Api.ViewModels
{
    using Models;

    public class CategoryViewModel : Category
    {
        public bool IsSelected { get; set; }

        public CategoryViewModel(Category cat)
        {
            if (cat == null)
            {
                return;
            }

            Id = cat.Id;
            Name = cat.Name;

        }
    }
}
