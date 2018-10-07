namespace BlogEngine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        //Automatically insert value on add or updated
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EditedDateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostedDateTime { get; set; }

        [StringLength(200)]
        [Required]
        public string Title { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        public string Content { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }
    }
}
