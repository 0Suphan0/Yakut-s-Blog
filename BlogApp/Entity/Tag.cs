﻿namespace BlogApp.Entity
{

    public enum TagColors
    {
        primary,
        secondary,
        danger,
        warning,
        success
    }


    public class Tag
    {
        public int TagId { get; set; }
        public string? Text { get; set; }

        public string? Url { get; set; }

        public TagColors? Color { get; set; }

        //bir tag hangi postları içerir? icerdigi tüm ppostların listesi..
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
