using Git_system.Notifications.Services;

namespace Git_system.Notifications
{
    public class Sender
    {
        public INotification Notification { get; private set; }
        public IService Service { get; private set; }

        public Sender(INotification notification, IService service)
        {
            this.Notification = notification;
            this.Service = service;
        }

        public void send()
        {
            Service.send(this.Notification);
        }
    }
}