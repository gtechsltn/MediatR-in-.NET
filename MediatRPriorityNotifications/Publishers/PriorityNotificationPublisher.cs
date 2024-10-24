using MediatR;
using MediatRPriorityNotifications.Services;

namespace MediatRPriorityNotifications.Publishers
{
    public class PriorityNotificationPublisher : IPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public PriorityNotificationPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // Implementation for Publish<TNotification>
        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();

            var priorityHandlers = handlers
                .Select(handler => (handler, priority: GetPriority(handler)))
                .OrderByDescending(h => h.priority)
                .ToList();

            foreach (var (handler, _) in priorityHandlers)
            {
                await handler.Handle(notification, cancellationToken);
            }
        }

        // Implementation for Publish(object)
        public async Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            var notificationType = notification.GetType();
            var notificationHandlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);
            var handlers = _serviceProvider.GetServices(notificationHandlerType);

            var priorityHandlers = handlers
                .Cast<object>()
                .Select(handler => (handler, priority: GetPriority(handler)))
                .OrderByDescending(h => h.priority)
                .ToList();

            foreach (var (handler, _) in priorityHandlers)
            {
                var handleMethod = handler.GetType().GetMethod("Handle", new[] { notificationType, typeof(CancellationToken) });
                if (handleMethod != null)
                {
                    await (Task)handleMethod.Invoke(handler, new[] { notification, cancellationToken });
                }
            }
        }

        // Assign priorities to handlers based on their type
        private int GetPriority(object handler)
        {
            return handler switch
            {
                INotificationHandler<HighPriorityNotification> => 1,
                INotificationHandler<LowPriorityNotification> => 0,
                _ => -1
            };
        }
    }
}
