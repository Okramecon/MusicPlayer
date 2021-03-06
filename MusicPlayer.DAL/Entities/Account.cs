namespace MusicPlayer.DAL.Entities
{
    public class Account : BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
