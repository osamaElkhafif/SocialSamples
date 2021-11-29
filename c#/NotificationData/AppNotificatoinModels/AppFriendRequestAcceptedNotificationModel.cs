using SocialDataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.Models.NotificationData.AppNotificatoinModels
{
    public class AppFriendRequestAcceptedNotificationModel
    {

        public UserDataViewModel requestAcceptorUserData { get; set; }

        public DateTime dateAccepted { get; set; }
    }
}
