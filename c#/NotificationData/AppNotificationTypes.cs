using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.Models.NotificationData
{
     public static class AppNotificationTypes
    {
      

        public static string UserMessage { get; set; } = "UserMessage";

        public static string VideoProcessingResult { get; set; } = "VideoProcessingResult";

        public static string UserInteractedToPost { get; set; } = "UserInteractedToPost";

        public static string UserCommentedToPost { get; set; } = "UserCommentedToPost";

        public static string FriendRequest { get; set; } = "FriendRequest";

        public static string FriendRequestAccepted { get; set; } = "FriendRequestAccepted";




    }
}
