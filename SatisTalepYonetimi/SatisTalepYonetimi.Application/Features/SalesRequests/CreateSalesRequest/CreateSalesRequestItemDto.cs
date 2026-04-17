namespace SatisTalepYonetimi.Application.Features.SalesRequests.CreateSalesRequest
{
    public sealed record CreateSalesRequestItemDto(
        Guid ProductId,
        int Quantity);
}
