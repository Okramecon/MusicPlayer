using System.Collections.Generic;

namespace MusicPlayer.DAL.Entities
{
    public class Album : BaseEntity<int>
    {
        public string Name { get; set; }

        public string CoverUrl { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
