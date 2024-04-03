﻿using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostRepository _postRepository;


        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
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

        public async Task<IActionResult> AddComment(int postId, string userName, string text)
        {

            _postRepository.AddComment(postId,text,userName);
            Post post= await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId==postId);

            // Yorum eklendikten sonra, Post detay sayfasına yönlendirme yapılabilir
            return RedirectToAction("PostDetail", new { url = post.Url });
        }




    }
}
