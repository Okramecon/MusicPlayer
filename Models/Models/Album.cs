using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Models.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CoverUrl { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
