using MassTransit;
using Microsoft.Extensions.Logging;
using SatisTalepYonetimi.Application.Events;

namespace SatisTalepYonetimi.Infrastructure.Consumers;

public sealed class PurchaseQuoteApprovedConsumer(
    ILogger<PurchaseQuoteApprovedConsumer> logger) : IConsumer<PurchaseQuoteApprovedEvent>
{
    public Task Consume(ConsumeContext<PurchaseQuoteApprovedEvent> context)
    {
        var message = context.Message;

        logger.LogInformation(
            "Satınalma teklifi onaylandı. QuoteId: {QuoteId}, SalesRequestId: {SalesRequestId}, Tutar: {Amount}",
            message.QuoteId,
            message.SalesRequestId,
            message.Amount);

        // Tedarikçiye bildirim, stok güncelleme vb. işlemler burada yapılabilir

        return Task.CompletedTask;
    }
}
