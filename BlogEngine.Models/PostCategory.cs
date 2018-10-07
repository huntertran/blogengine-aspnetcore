namespace BlogEngine.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostCategory
    {
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
