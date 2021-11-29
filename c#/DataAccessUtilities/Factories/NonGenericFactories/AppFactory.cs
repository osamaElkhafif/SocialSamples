using SocialDataAccess.Models;
using SocialDataAccess.Models.NotificationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.Factories.NonGenericFactories
{
    public static partial class AppFactory
    {

        public static DbNotification DbNotificationFactory(ApplicationUser sender, ApplicationUser receiver)
        {

            return new DbNotification()
            {
                SenderUserId = sender.Id,
                ReceiverUserId = receiver.Id,
                HasBeenRead = false,

            };
        }

    }

}

