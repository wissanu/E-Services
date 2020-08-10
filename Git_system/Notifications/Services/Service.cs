using System;

namespace Git_system.Notifications.Services
{
    public interface IService
    {
        /// <summary>
        /// Send notification to client.
        /// </summary>
        /// <param name="title">Title of notification</param>
        /// <param name="message">Message of notification</param>
        /// <param name="recipient">Recipient address of notification</param>
        void send(String title, String message, String recipient);

        /// <summary>
        /// Send notification to client.
        /// </summary>
        /// <param name="notification">notification will send</param>
        void send(INotification notification);
    }
}