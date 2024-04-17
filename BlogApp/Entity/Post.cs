namespace BlogApp.Entity
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; } 
        public string? Content { get; set; }
        public string? Description { get; set; }

        public string? Url { get; set; }

        public string? Image { get; set; }

        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }

        //hangi kullanıcı postu yayınladı ?
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        //ilgili postta hangi tag'ler var. Liste tutulur.
        public List<Tag> Tags { get; set; } = new List<Tag>();

        //iligli postta hangi comment'ler var liste tutulur.
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
