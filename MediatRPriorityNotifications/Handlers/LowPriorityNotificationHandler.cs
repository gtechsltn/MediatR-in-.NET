using MediatR;
using MediatRPriorityNotifications.Services;

namespace MediatRPriorityNotifications.Handlers
{
    public class LowPriorityNotificationHandler : INotificationHandler<LowPriorityNotification>
    {
        public Task Handle(LowPriorityNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Low Priority: {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
