namespace MusicPlayer.DAL.Entities
{
    public class Track : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public string CoverUrl { get; set; }
        public string MusicUrl { get; set; }

        // Refrence to author
        public Author Author { get; set; }
    }
}
