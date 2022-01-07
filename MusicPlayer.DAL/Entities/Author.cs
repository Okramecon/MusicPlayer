namespace MusicPlayer.DAL.Entities
{
    public class Author : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string Pseudonym { get; set; }
        public string UrlToPhoto { get; set; }
        public string Bio { get; set; }
    }
}