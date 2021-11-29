using SocialDataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.Models.NotificationData.AppNotificatoinModels
{
    public class AppFriendRequestNotificationModel
    {

        public UserDataViewModel requestSenderUserData { get; set; }

        public DateTime dateAdded { get; set; }

    }
}
