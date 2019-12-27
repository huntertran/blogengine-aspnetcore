namespace BlogEngine.Services.Post
{
    using Models;
    using Repository.Generic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _repository;

        public PostService(IGenericRepository<Post> repository)
        {
            _repository = repository;
        }

        public int Count(Expression<Func<Post, bool>> filter = null)
        {
            return _repository.Count(filter);
        }

        public Post GetById(object id)
        {
            return _repository.GetById(id);
        }

        public void Insert(Post post)
        {
            post.CreatedDateTime = DateTime.UtcNow;
            post.EditedDateTime = DateTime.UtcNow;
            post.PostedDateTime = DateTime.UtcNow;

            _repository.Insert(post);
            _repository.SaveChanges();
        }

        public void Update(Post post)
        {
            post.EditedDateTime = DateTime.UtcNow;
            post.PostedDateTime = DateTime.UtcNow;

            _repository.Update(post);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public IList<Post> All(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0)
        {
            Expression<Func<Post, bool>> filter;

            if (categoryId == 0)
            {
                filter = post => post.IsPublished;
            }
            else
            {
                filter = post => post.IsPublished
                                 && post.PostCategories.Any(x => x.CategoryId == categoryId);
            }

            return _repository.GetByPage(
                page,
                postPerPage,
                filter,
                posts => posts.OrderByDescending(x => x.PostedDateTime));
        }

        public IList<Post> GetUnpublishedPosts(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0)
        {
            Expression<Func<Post, bool>> filter;

            if (categoryId == 0)
            {
                filter = post => post.IsPublished == false;
            }
            else
            {
                filter = post => post.IsPublished == false
                                 && post.PostCategories.Any(x => x.CategoryId == categoryId);
            }

            return _repository.GetByPage(
                page,
                postPerPage,
                filter,
                posts => posts.OrderByDescending(x => x.PostedDateTime));
        }
    }
}