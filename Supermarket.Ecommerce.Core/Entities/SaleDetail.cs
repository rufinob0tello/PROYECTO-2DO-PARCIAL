namespace Supermarket.Ecommerce.Core.Entities;

public class SaleDetail : EntityBase
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
