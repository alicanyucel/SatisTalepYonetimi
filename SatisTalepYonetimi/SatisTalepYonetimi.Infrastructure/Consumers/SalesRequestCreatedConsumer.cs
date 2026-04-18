using MassTransit;
using Microsoft.Extensions.Logging;
using SatisTalepYonetimi.Application.Events;

namespace SatisTalepYonetimi.Infrastructure.Consumers;

public sealed class SalesRequestCreatedConsumer(
    ILogger<SalesRequestCreatedConsumer> logger) : IConsumer<SalesRequestCreatedEvent>
{
    public Task Consume(ConsumeContext<SalesRequestCreatedEvent> context)
    {
        var message = context.Message;

        logger.LogInformation(
            "Yeni satış talebi oluşturuldu. RequestId: {RequestId}, RequestNumber: {RequestNumber}, Tutar: {Amount}",
            message.SalesRequestId,
            message.RequestNumber,
            message.TotalAmount);

        // Yöneticiye bildirim gönderme vb. işlemler burada yapılabilir

        return Task.CompletedTask;
    }
}
