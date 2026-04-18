using MassTransit;
using Microsoft.Extensions.Logging;
using SatisTalepYonetimi.Application.Events;

namespace SatisTalepYonetimi.Infrastructure.Consumers;

public sealed class SalesRequestStatusChangedConsumer(
    ILogger<SalesRequestStatusChangedConsumer> logger) : IConsumer<SalesRequestStatusChangedEvent>
{
    public Task Consume(ConsumeContext<SalesRequestStatusChangedEvent> context)
    {
        var message = context.Message;

        logger.LogInformation(
            "Satış talebi durum değişikliği alındı. RequestId: {RequestId}, Eski Durum: {OldStatus}, Yeni Durum: {NewStatus}",
            message.SalesRequestId,
            message.OldStatus,
            message.NewStatus);

        // Bildirim gönderme, e-posta, SMS vb. işlemler burada yapılabilir

        return Task.CompletedTask;
    }
}
