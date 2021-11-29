
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.SignalR;

namespace SocialDataAccess.Models.NotificationData
{




    public class AppNotificationModel
    {
        [Required]
        public string NotificationType { get; set; } = AppNotificationTypes.UserMessage;

        [Required]
        public string ReceiverUserName { get; set; }

        [Required]
        public Dictionary<string, string> NotificationData { get; set; } = new Dictionary<string, string>();

        public string Token { get; set; }
    }

    /// <summary>
    /// this class represtents the notification sent to mobile and tcp
    /// <para>(appNotificationTcpFunction) should be initialized for tcp notification to work </para>
    /// <para>(socialDbNotificationAddCallBack) should be initialized for notificatoin to be added to social data base </para>
    /// </summary>
    public class AppNotification
    {

        public AppNotificationModel AppNotificationModel { get; set; } = new AppNotificationModel();

        /// <summary>
        /// this parameter should be passed for tcp notification to work
        /// </summary>
        public AppNotificationTcpFunction AppNotificationTcpFunction { get; set; } = null;

        /// <summary>
        /// this callbacl should be initialized for notificatoin to be added to social data base 
        /// </summary>
        public SocialDbNotificationAdd SocialDbNotificationAddCallBack { get; set; } = null;

        public AppNotification(
            string notificationType,
            string receieverUserName,
            AppNotificationTcpFunction appNotificationTcpFunction,
            SocialDbNotificationAdd socialDbNotificationAddCallBack
            )
        {
           AppNotificationModel.NotificationType = notificationType;
           AppNotificationModel.ReceiverUserName = receieverUserName;
            AppNotificationTcpFunction = appNotificationTcpFunction;
            SocialDbNotificationAddCallBack = socialDbNotificationAddCallBack;
        }

        public AppNotification(
            AppNotificationModel appNotificationModel,
            AppNotificationTcpFunction appNotificationTcpFunction,
            SocialDbNotificationAdd socialDbNotificationAddCallBack
     )
        {
            AppNotificationModel = appNotificationModel;
            AppNotificationTcpFunction = appNotificationTcpFunction;
            SocialDbNotificationAddCallBack = socialDbNotificationAddCallBack;

        }




    }


}
