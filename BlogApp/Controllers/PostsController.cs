﻿using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostRepository _postRepository;


        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {
            ;

            return View(_postRepository.Posts.ToList());
        }

        public async Task<IActionResult> PostDetail(string url)
        {
           Post post= await _postRepository.Posts.FirstOrDefaultAsync(p => p.Url == url);

           return View(post);

        }


    } 
}
