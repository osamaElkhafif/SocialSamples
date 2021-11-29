using SocialDataAccess.DataAccessUtilities.Factories.NonGenericFactories;
using SocialDataAccess.Models;
using SocialDataAccess.Models.NotificationModel;
using SocialDataAccess.Models.NotificationModel.PostsCommentsDbNotifications;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsComments;
using SocialDataAccess.Models.PostsCommentsInteractions;
using SocialDataAccess.Models.PostsInteractios;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.Models.PostsModels.SharedPosts;
using SocialDataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.Factories.GenericFactories.RelatedToPostsFactories
{
    public class RelatedToImagesPostFactory<P> : AbstractRelatedToPostsFactory<P>
        where P: class,IIPost
    {


        public override IIPostInteraction postInteractionFactory(ApplicationUser appUser,
            AddPostInteraction addPostInteraction,IIPost post)
        {

            return new ImagesPostInteraction()
            {
                userId = appUser.Id,
                InteractionTypeId = (int)addPostInteraction.postInteractionType,
                PostId = post.Id
            };

        }

        public override IIUserInteractedToPostDbNotification userInteractedToPostDbNotificationFactory
            (ApplicationUser sender,ApplicationUser receiver,IIPost post)
        {

           DbNotification dbNotification = AppFactory.DbNotificationFactory(sender, receiver);


            return 
            new UserInteractedToImagesPostDbNotification()
            {
                dbNotification = dbNotification,
                PostId = post.Id
            };
        }

        public override IIPostComment postCommentFactory(Guid postId, string commentText, ApplicationUser appUser)
        {
            return 
                new ImagesPostComment()
            {
                PostId = postId,
                commentText = commentText,
                userId = appUser.Id
            };
        }

        public override IIUserCommentedToPostDbNotification userCommentedToPostFactory(ApplicationUser sender, ApplicationUser receiver
            , IIPost post)
        {

            DbNotification dbNotification = AppFactory.DbNotificationFactory(sender, receiver);

            return 
                new UserCommentedToImagesPostDbNotification()
            {
                dbNotification = dbNotification,
                PostId = post.Id

            };
        }

        public override IISharedPost SharedPostFactory(ApplicationUser appUser, SharedPostModel sharedPostModel)
        {

            return new SharedImagesPost()
            {

                actualImagesPostId = sharedPostModel.postId,
                Privacy = (short)sharedPostModel.postPrivacy,
                PostText = sharedPostModel.postText,
                UserId = appUser.Id,

            };

        }

        public override IIPostCommentInteraction postCommentInteractionFactory(ApplicationUser sender, AddCommentInteractionModel addCommentInteractionModel)
        {
            return new ImagesPostCommentInteraction()
            {
                userId = sender.Id,
                PostCommentId = addCommentInteractionModel.commentId,
                InteractionTypeId = (short)addCommentInteractionModel.postInteractionType,
                dateInteracted = DateTime.UtcNow
            };
        }
    }
}
