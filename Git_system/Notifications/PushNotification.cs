using System;

namespace Git_system.Notifications
{
    public abstract class PushNotification : INotification
    {
        public String Title { get; set; } // **unused
        public String Message { get; set; } // alert
        public String Recipient { get; set; } // device id

        public PushNotification(String deviceIdentifier, String alert)
        {
            this.Message = alert;
            this.Recipient = deviceIdentifier;
        }
    }

    public class IOSNotification : PushNotification
    {
        public String Title { get; private set; }
        public String Message { get; private set; }
        public String Recipient { get; private set; }

        public IOSNotification(String deviceIdentifier, String alert)
            : base(deviceIdentifier, alert)
        {
        }
    }
}