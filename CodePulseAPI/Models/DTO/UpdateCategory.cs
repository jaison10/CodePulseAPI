namespace CodePulseAPI.Models.DTO
{
    public class UpdateCategory
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        public Guid[] BlogIDs { get; set; }
    }
}
