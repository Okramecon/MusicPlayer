using System;

namespace MusicPlayer.Models.Models.AuthModels
{
    public class ResponseAuthModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
