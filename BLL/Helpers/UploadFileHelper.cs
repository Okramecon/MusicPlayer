using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.BLL.Helpers
{
    public static class UploadFileHelper
    {
        public static async Task UploadMusicFileAsync(IFormFile upload)
        {
            var file = Path.Combine(Environment.CurrentDirectory, "StaticFiles/Music", upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await upload.CopyToAsync(fileStream);
            }
        }
    }
}
