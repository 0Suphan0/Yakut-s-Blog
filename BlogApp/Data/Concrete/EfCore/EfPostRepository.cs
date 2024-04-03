using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;

        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
        }

        public void AddComment(int postId, string comment,string userName)
        {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            if (post != null)
            {
                if (post.Comments == null)
                {
                    post.Comments = new List<Comment>();
                }

                post.Comments.Add(new Comment { User = new User{UserName = userName,Image ="/p1.jpg" }, CommentText = comment ,PublishedOn = DateTime.Now});
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Post not found with the given postId");
            }
        }



    }
}
