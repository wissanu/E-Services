using Git_system.Models;
using Git_system.Notifications.Services;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Git_system.Notifications
{
    public class Device
    {
        [DataMember(Name = "device_identifier")]
        [JsonProperty(PropertyName = "device_identifier")]
        public string DeviceIdentifier { get; private set; }

        [DataMember(Name = "type")]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

        public Device(string deviceIdentifier, string type)
        {
            this.DeviceIdentifier = deviceIdentifier;
            this.Type = type;
        }

        public BaseDevice getBaseDevice(int membershipId)
        {
            Models.BaseDevice bDevice = new BaseDevice();
            bDevice.MembershipId = membershipId;
            bDevice.DeviceIdentifier = this.DeviceIdentifier;
            bDevice.Type = this.Type;
            return bDevice;
        }

        public BaseDevice setBaseDevice(BaseDevice bDevice)
        {
            bDevice.DeviceIdentifier = this.DeviceIdentifier;
            bDevice.Type = this.Type;
            return bDevice;
        }

        public Sender getSender(String alert)
        {
            try
            {
                var notification = getNotification(alert);
                var service = getService();
                return new Sender(notification, service);
            }
            catch
            {
                return null;
            }
        }

        // getService from string type
        private IService getService()
        {
            if (this.Type == "APNS")
                return new Notifications.Services.Apns();

            throw new Exception("Not found service type.");
        }

        private INotification getNotification(String alert)
        {
            if (this.Type == "APNS")
                return new Notifications.IOSNotification(this.DeviceIdentifier, alert);

            throw new Exception("Not found service type.");
        }
    }
}