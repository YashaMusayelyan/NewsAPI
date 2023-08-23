namespace News.DAL.Entities
{
    public class NewsEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatorName { get; set; }

        public string UpdateCreatorName { get; set; }

        public string CreateDate { get; set; }

        public string UpdateDate { get; set; } = default;
    }
}
