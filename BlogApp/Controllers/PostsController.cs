using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            var claims = User.Claims.ToList();
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
        public async Task<JsonResult> AddComment(int PostId,string Text, string Url)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userImage = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment()
            {
                CommentText = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId??"")
            };

            _commentRepository.CreateComment(entity);

            return Json(new
            {
                userName,
                Text,
                entity.PublishedOn,
                userImage
            });
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {

                _postRepository.CreatePost(new Post
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                    Image = "1.jpg",
                    UserId = int.Parse(userId),
                    PublishedOn = DateTime.Now,
                    IsActive = false
                }) ;

                return RedirectToAction("Index");

            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)??"");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role))
            {
               posts= posts.Where(p => p.UserId == userId);
            }


            return View(await posts.ToListAsync());
        }



    }
}
