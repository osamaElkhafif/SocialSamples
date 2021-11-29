using SocialDataAccess.DataAccessUtilities.Factories.NonGenericFactories;
using SocialDataAccess.Models;
using SocialDataAccess.Models.NotificationModel;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.Models.PostsModels.SharedPosts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.Factories
{
    //static partial class RelatedToPostsFactory<T> where T : class, IPost
    //{


    //    public static IUserInteractedtoPostDbNotification UserInteractedToPostDbNotification(
    //        ApplicationUser sender, ApplicationUser receiver, IPost post)
    //    {



    //        DbNotification dbNotification = AppFactory.DbNotificationFactory(sender, receiver);

    //        if (typeof(T) == typeof(TextPost))
    //        {

    //            return UserInteractedToPostDbNotification<UserInteractedToTextPostDbNotification>
    //             (post, dbNotification);

    //        }

    //        else if (typeof(T) == typeof(ImagesPost))
    //        {

    //            return UserInteractedToPostDbNotification<UserInteractedToImagesPostDbNotification>
    //             (post, dbNotification);

    //        }

    //        else if (typeof(T) == typeof(VideoPost))
    //        {

    //            return UserInteractedToPostDbNotification<UserInteractedToVideoPostDbNotification>
    //             (post, dbNotification);

    //        }

    //        else if (typeof(T) == typeof(SharedTextPost))
    //        {

    //            return UserInteractedToPostDbNotification<UserInteractedToSharedTextPostDbNotification>
    //             (post, dbNotification);

    //        }

    //        else if (typeof(T) == typeof(SharedImagesPost))
    //        {

    //            return UserInteractedToPostDbNotification<UserInteractedToSharedImagesPostDbNotification>
    //             (post, dbNotification);

    //        }

    //        else if (typeof(T) == typeof(SharedVideoPost))
    //        {

    //            return UserInteractedToPostDbNotification<UserInteractedToSharedVideoPostDbNotification>
    //             (post, dbNotification);

    //        }

    //        else
    //        {
    //            throw new Exception("invalid data passed to method UserInteractedToPostDbNotification");
    //        }



    //    }


    //    private static IUserInteractedtoPostDbNotification UserInteractedToPostDbNotification<UITPDbN>(IPost post,DbNotification dbNotification)
    //        where UITPDbN : class, IUserInteractedtoPostDbNotification, new()
    //    {

    //        return new UITPDbN()
    //        {
    //            dbNotification = dbNotification,
    //            PostId = post.Id,
    //            dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //        };

    //    }


    //}
}
