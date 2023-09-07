namespace CodePulseAPI.Models.DomainModels
{
    public class BlogPosts
    {
        public Guid Id{ get; set; }
        public string Title{ get; set; }
        public string ShortDesc{ get; set; }
        public string Content{ get; set; }
        public string FeaturedImgURL{ get; set; }
        public string UrlHandle{ get; set; }
        public DateTime PublishedDate{ get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

    }
}
