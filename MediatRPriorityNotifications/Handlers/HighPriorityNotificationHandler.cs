using MediatR;
using MediatRPriorityNotifications.Services;

namespace MediatRPriorityNotifications.Handlers
{
    public class HighPriorityNotificationHandler : INotificationHandler<HighPriorityNotification>
    {
        public Task Handle(HighPriorityNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"High Priority: {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
