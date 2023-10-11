namespace CodePulseAPI.Models.DomainModels
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        public ICollection<BlogPosts> Blogs { get; set; }

    }
}
