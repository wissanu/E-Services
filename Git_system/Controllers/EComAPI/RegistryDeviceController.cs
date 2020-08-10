using Git_system.Models;
using Git_system.Models.ECom.API;
using Git_system.Notifications;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Git_system.Controllers.EComAPI
{
    public class RegistryDeviceController : MainController
    {
        [AllowCrossSite]
        public Device Get()
        {
            var push = new Notifications.Services.Apns();
            var devicesToken = (new List<string>(new string[] { "ad65bb1d e84db0f7 e0ef6b9a ebf0d804 b5ab2bd5 55aa5896 c80a22ac 845317c5", "ad65bb1d e84db0f7 e0ef6b9a ebf0d804 b5ab2bd5 55aa5896 c80a22ac 845317c5" })).ConvertAll(c => c.Replace(" ", ""));
            //push.send(devicesToken);

            var notification = new IOSNotification("ad65bb1de84db0f7e0ef6b9aebf0d804b5ab2bd555aa5896c80a22ac845317c5", "Test Sender");
            var service = new Notifications.Services.Apns();
            var sender = new Sender(notification, service);
            sender.send();

            return new Device("df", "1");
        }

        [AllowCrossSite]
        public HttpResponseMessage Post(Device device)
        {
            return Request.CreateResponse<DataAndStatus>(HttpStatusCode.OK, new DataAndStatus().authen(_MembershipId, () =>
            {
                var bDevice = db.BaseDevices.Where(d => d.DeviceIdentifier == device.DeviceIdentifier).Take(1);
                if (bDevice.Count() == 0)
                {
                    BaseDevice baseDevice;
                    baseDevice = device.getBaseDevice(_MembershipId);
                    db.BaseDevices.Add(baseDevice);
                    db.SaveChanges();
                }
                else if (bDevice.FirstOrDefault().MembershipId != _MembershipId)
                {
                    BaseDevice baseDevice = bDevice.FirstOrDefault();
                    db.Entry(baseDevice).State = EntityState.Modified;
                    device.setBaseDevice(baseDevice);
                    db.SaveChanges();
                }
                return new DataAndStatus(1, new { status = "registered" });
            }));
        }

        [AllowCrossSite]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}