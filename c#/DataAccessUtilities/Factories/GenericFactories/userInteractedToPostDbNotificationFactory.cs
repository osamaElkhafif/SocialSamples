using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    //static partial class RelatedToPostsFactory<T> where T:class,IPost
    //{

    //    public static IUserInteractedtoPostDbNotification userInteractedToPostDbNotificationFactory(
    //        ApplicationUser appUser,ApplicationUser postOwner,IPost post)
    //    {

    //        DbNotification dbNotification = new DbNotification()
    //        {
    //            ReceiverUserId = postOwner.Id,
    //            SenderUserId = appUser.Id,
    //            HasBeenRead = false

    //        };

    //        if(typeof(T) == typeof(TextPost))
    //        {

    //            return new UserInteractedToTextPostDbNotification()
    //            {
    //                dbNotification = dbNotification,
    //                PostId = post.Id,
    //                dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //            };
    //        }

    //        else if (typeof(T) == typeof(ImagesPost))
    //        {

    //            return new UserInteractedToImagesPostDbNotification()
    //            {
    //                dbNotification = dbNotification,
    //                PostId = post.Id,
    //                dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //            };
    //        }

    //        else if (typeof(T) == typeof(ImagesPost))
    //        {

    //            return new UserInteractedToVideoPostDbNotification()
    //            {
    //                dbNotification = dbNotification,
    //                PostId = post.Id,
    //                dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //            };
    //        }

    //        else if (typeof(T) == typeof(SharedTextPost))
    //        {

    //            return new UserInteractedToSharedTextPostDbNotification()
    //            {
    //                dbNotification = dbNotification,
    //                PostId = post.Id,
    //                dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //            };
    //        }

    //        else if (typeof(T) == typeof(SharedImagesPost))
    //        {

    //            return new UserInteractedToSharedImagesPostDbNotification()
    //            {
    //                dbNotification = dbNotification,
    //                PostId = post.Id,
    //                dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //            };
    //        }

    //        else if (typeof(T) == typeof(SharedVideoPost))
    //        {

    //            return new UserInteractedToSharedVideoPostDbNotification()
    //            {
    //                dbNotification = dbNotification,
    //                PostId = post.Id,
    //                dbNotificationTypeId = (short)DbNotificationTypeEnum.TextPostInteraction
    //            };
    //        }
    //        else
    //        {
    //            throw new Exception("there is invalid data passed to userInteractedToPostDbNotificationFactory function");
    //        }

    //    }

    //}
}
