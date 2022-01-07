namespace MusicPlayer.DAL.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
