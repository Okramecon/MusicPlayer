using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Models.Models
{
    public class TrackModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public string CoverUrl { get; set; }
        public string MusicUrl { get; set; }

        // Refrence to author
        public AuthorModel Author { get; set; }
    }
}
