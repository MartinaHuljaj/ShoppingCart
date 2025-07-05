namespace AbySalto.Mid.Domain.Entities
{
    public class FavoriteItem
    {
        public int FavoriteItemId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; } = default!;
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
