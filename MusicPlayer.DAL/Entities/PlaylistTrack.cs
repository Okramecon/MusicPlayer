﻿namespace MusicPlayer.DAL.Entities
{
    public class PlaylistTrack : BaseEntity<int>
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
        public Playlist Playlist { get; set; }
        public Track Track { get; set; }
    }
}
