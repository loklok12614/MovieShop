namespace ApplicationCore.Models;

public class MovieCardPurchasedModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterUrl { get; set; }
    public DateTime? PurchaseDateTime { get; set; }
    public decimal? TotalPrice { get; set; }
    public Guid PurchaseNumber { get; set; }
}