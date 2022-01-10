using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
    }

    public class GetAccountModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public RoleModel Role { get; set; }
    }

    public class AddAccountModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
