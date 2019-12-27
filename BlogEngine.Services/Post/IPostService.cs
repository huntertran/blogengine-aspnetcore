namespace BlogEngine.Services.Post
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IPostService
    {
        int Count(Expression<Func<Post, bool>> filter = null);

        Post GetById(object id);

        IList<Post> All(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0);

        IList<Post> GetUnpublishedPosts(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0);

        void Insert(Post post);

        void Update(Post post);

        void Delete(int id);
    }
}
