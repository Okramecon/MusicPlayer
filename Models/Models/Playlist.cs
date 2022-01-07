using System.Collections.Generic;

namespace MusicPlayer.Models.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        /// <summary>
        /// Обложка
        /// </summary>
        public string CoverUrl { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
