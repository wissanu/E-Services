using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Git_system.Notifications.Services
{
    /// <summary>
    /// iOS push notification.
    /// </summary>
    public class Apns : IService
    {
        private JObject genPayload(String message = "")
        {
            return JObject.FromObject(new
            {
                aps = new
                {
                    badge = 0,
                    alert = message
                }
            });
        }

        private void send(ApnsConfiguration config, String deviceToken, String message = "")
        {
            var apnsBroker = genApnsBroker(config);

            // Start the broker
            apnsBroker.Start();

            // notification to send
            apnsBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = deviceToken,
                Payload = genPayload(message)
            });

            // Stop the broker, wait for it to finish
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();
        }

        public void send(String title = "", String message = null, String recipient = null)
        {
            send(genApnsConfiguration(), recipient, message);
        }

        public void send(INotification notification)
        {
            send(notification.Title, notification.Message, notification.Recipient);
        }

        private ApnsConfiguration genApnsConfiguration()
        {
            var certificatPath = AppDomain.CurrentDomain.BaseDirectory + "Certificat\\APNS\\";

#if DEBUG
            var publicCertPath = certificatPath + "Development\\aps_development.cer";
            var privateKeyPaht = certificatPath + "Development\\PushKey.pem";
            var apnsServerEnvironment = ApnsConfiguration.ApnsServerEnvironment.Sandbox;
#else
            var publicCertPath = certificatPath + "Production\\aps.cer";
            var privateKeyPaht = certificatPath + "Production\\PushKey.pem";
            var apnsServerEnvironment = ApnsConfiguration.ApnsServerEnvironment.Production;
#endif

            // Configuration (NOTE: .pfx can also be used here)

            /*
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "Certificat\\" + "aps_development.cer";

            var cert = new X509Certificate2(filePath, "", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            var cer = PEMToX509.Convert(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Certificat\\" + "PushKey.pem"), cert);

            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, cer);
             */

            RSACryptoServiceProvider privateRSAkey = CSharp_easy_RSA_PEM.Crypto.DecodeRsaPrivateKey(System.IO.File.ReadAllText(privateKeyPaht));
            var cert = new X509Certificate2(publicCertPath, "", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            cert.PrivateKey = privateRSAkey;

            var config = new ApnsConfiguration(apnsServerEnvironment, cert);

            return config;
        }

        private ApnsServiceBroker genApnsBroker(ApnsConfiguration config)
        {
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        //Console.WriteLine ($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                        System.Diagnostics.Debug.WriteLine(string.Format("Apple Notification Failed: ID={0}, Code={1}", apnsNotification.Identifier, statusCode));
                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException
                        //Console.WriteLine ($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                        System.Diagnostics.Debug.WriteLine(string.Format("Apple Notification Failed for some unknown reason : {0}", ex.InnerException));
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Apple Notification : {0} => {1}", notification.DeviceToken, notification.Payload));
                System.Diagnostics.Debug.WriteLine("Apple Notification Sent!");
            };

            return apnsBroker;
        }
    }
}