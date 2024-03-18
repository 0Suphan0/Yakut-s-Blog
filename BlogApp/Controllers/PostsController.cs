using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;


        public PostsController(IPostRepository postRepository,ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            PostViewModel postViewModel = new()
            {
                Posts = _postRepository.Posts.ToList(),
                Tags = _tagRepository.Tags.ToList(),

            };

            return View(postViewModel);
        }
    }
}
