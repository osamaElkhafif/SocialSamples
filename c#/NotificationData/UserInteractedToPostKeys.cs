using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.Models.NotificationData
{
    public static class UserInteractedToPostKeys
    {


        public static string postId { get; set; } = "postId";

        public static string postType { get; set; } = "postType";

        public static string recentUsersInteractedFullNamesSeparatedByCommas { get; set; } = "recentUsersInteractedFullNames";

        public static string LatestUserInteractedProfilePictureUrl { get; set; } = "LatestUserInteractedProfilePictureUrl";

        public static string LatestUserInteractedProfilePictuePosition { get; set; } = "LatestUserInteractedProfilePictuePosition";

        public static string LatestUserInteractedProfilePictureType { get; set; } = "LatestUserInteractedProfilePictureType";

        public static string LatestUserInteractedUserName { get; set; } = "LatestUserInteractedUserName";

        public static string totalNumbersOfUsersInteracted { get; set; } = "totalNumbersOfUsersInteracted";


    }
}
