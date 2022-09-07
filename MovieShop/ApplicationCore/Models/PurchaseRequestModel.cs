namespace ApplicationCore.Models;

public class PurchaseRequestModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public Guid PurchaseNumber => Guid.NewGuid();
    public decimal TotalPrice { get; set; }
}