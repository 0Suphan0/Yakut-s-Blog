﻿using BlogApp.Entity;

namespace BlogApp.Models
{
    public class PostViewModel
    {
        public List<Post> Posts { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
