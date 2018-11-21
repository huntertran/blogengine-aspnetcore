namespace BlogEngine.Client.Areas.API.ViewModels
{
    using System;
    using BlogEngine.Models;

    public class MinifiedPostViewModel
    {
        public int Id { get; set; }
        public DateTime PostedDateTime { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool IsPublished { get; set; }

        public MinifiedPostViewModel(Post post)
        {
            Id = post.Id;
            PostedDateTime = post.PostedDateTime;
            Title = post.Title;
            Summary = post.Summary;
            IsPublished = post.IsPublished;
        }
    }
}
