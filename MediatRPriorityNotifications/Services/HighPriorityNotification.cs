using MediatR;

namespace MediatRPriorityNotifications.Services
{
    public class HighPriorityNotification : INotification
    {
        public string Message { get; }

        public HighPriorityNotification(string message)
        {
            Message = message;
        }
    }
}
