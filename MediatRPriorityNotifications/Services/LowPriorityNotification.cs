using MediatR;

namespace MediatRPriorityNotifications.Services
{
    public class LowPriorityNotification : INotification
    {
        public string Message { get; }

        public LowPriorityNotification(string message)
        {
            Message = message;
        }
    }
}
