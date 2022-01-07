using MusicPlayer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public class TrackService : BaseContextService
    {
        public TrackService(AppDbContext context) : base(context)
        {

        }


    }
}
