using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {

        IQueryable<Post> Posts { get; }

        void CreatePost(Post post);

        void AddComment(int postId, string comment, string userName);




    }
}
