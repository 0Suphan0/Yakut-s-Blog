﻿namespace BlogApp.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; } = string.Empty;

        public DateTime PublishedOn { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public int UserId { get; set; }
        public required User User { get; set; } = null!;
    }
}