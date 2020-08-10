using System;

namespace Git_system.Notifications
{
    public interface INotification
    {
        String Title { get; set; }
        String Message { get; set; }
        String Recipient { get; set; }
    }
}