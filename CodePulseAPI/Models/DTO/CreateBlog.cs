﻿namespace CodePulseAPI.Models.DTO
{
    public class CreateBlog
    {
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Content { get; set; }
        public string FeaturedImgURL { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public Guid[] CategoryIDs { get; set; }
    }
}
