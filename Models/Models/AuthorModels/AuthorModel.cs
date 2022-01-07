namespace MusicPlayer.Models.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Pseudonym { get; set; }
        public string UrlToPhoto { get; set; }
        public string Bio { get; set; }
    }

    public class AddAuthorModel
    {
        public string FullName { get; set; }
        public string Pseudonym { get; set; }
        public string UrlToPhoto { get; set; }
        public string Bio { get; set; }
    }
}
