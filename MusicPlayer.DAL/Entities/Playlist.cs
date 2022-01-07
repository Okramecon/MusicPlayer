using System.Collections.Generic;

namespace MusicPlayer.DAL.Entities
{
    public class Playlist : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        /// <summary>
        /// Обложка
        /// </summary>
        public string CoverUrl { get; set; }

        public IEnumerable<PlaylistTrack> Tracks { get; set; }
    }
}
