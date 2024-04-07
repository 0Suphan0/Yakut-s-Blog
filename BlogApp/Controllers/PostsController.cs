using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;


        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult>Index(string tag)
        {
            #region Tag filtresine göre veri getirir.

            var posts = _postRepository.Posts;

            if (!string.IsNullOrEmpty(tag))
            {
                    posts = posts
                    .Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(await posts.Include(x => x.Tags).ToListAsync());

            #endregion


        }

        public async Task<IActionResult> PostDetail(string url)
        {
           Post post= await _postRepository
               .Posts
               .Include(x=>x.Tags)
               .Include(x=>x.Comments)
               .ThenInclude(x=>x.User)
               .FirstOrDefaultAsync(p => p.Url == url);

           return View(post);
        }

        [HttpPost]
        public async Task<JsonResult> AddComment(int PostId, string UserName, string Text, string Url)
        {

            var entity = new Comment()
            {
                CommentText = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User() { UserName = UserName, Image = "p1.jpg" }
            };

            _commentRepository.CreateComment(entity);

            return Json(new
            {
                UserName,
                Text,
                entity.PublishedOn,
                entity.User.Image
            });
        }




    }
}
