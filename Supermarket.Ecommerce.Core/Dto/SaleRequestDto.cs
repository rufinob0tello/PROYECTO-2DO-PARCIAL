namespace Supermarket.Ecommerce.Core.Dto;

public class SaleRequestDto
{
    public int CustomerId { get; set; }
    public List<SaleItemDto> Products { get; set; }
}