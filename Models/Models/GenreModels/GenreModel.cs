namespace MusicPlayer.Models.Models.GenreModels
{
    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GetGenreModel : GenreModel
    {

    }

    public class AddGenreModel
    {
        public string Name { get; set; }
    }

    public class EditGenreModel : GenreModel
    {

    }
}
