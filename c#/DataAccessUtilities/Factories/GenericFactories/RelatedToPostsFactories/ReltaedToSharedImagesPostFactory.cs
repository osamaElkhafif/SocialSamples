using SocialDataAccess.DataAccessUtilities.Factories.NonGenericFactories;
using SocialDataAccess.Models;
using SocialDataAccess.Models.NotificationModel;
using SocialDataAccess.Models.NotificationModel.PostsCommentsDbNotifications;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsComments;
using SocialDataAccess.Models.PostsComments.SharedPostsComments;
using SocialDataAccess.Models.PostsCommentsInteractions;
using SocialDataAccess.Models.PostsCommentsInteractions.SharedPostsCommentsInteractions;
using SocialDataAccess.Models.PostsInteractios;
using SocialDataAccess.Models.PostsInteractios.SharedPostsInteractions;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.Models.PostsModels.SharedPosts;
using SocialDataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.Factories.GenericFactories.RelatedToPostsFactories
{
    public class ReltaedToSharedImagesPostFactory<P>: AbstractRelatedToPostsFactory<P>
        where P :class,IIPost

    {

        public override IIPostInteraction postInteractionFactory(ApplicationUser appUser,
          AddPostInteraction addPostInteraction, IIPost post)
        {

            return new SharedImagesPostInteraction()
            {
                userId = appUser.Id,
                InteractionTypeId = (int)addPostInteraction.postInteractionType,
                PostId = post.Id
            };

        }

        public override IIUserInteractedToPostDbNotification userInteractedToPostDbNotificationFactory
            (ApplicationUser sender, ApplicationUser receiver, IIPost post)
        {

            DbNotification dbNotification = AppFactory.DbNotificationFactory(sender, receiver);

            return 
                new UserInteractedToSharedImagesPostDbNotification()
            {
                dbNotification = dbNotification,
                PostId = post.Id
            };
        }

        public override IIPostComment postCommentFactory(Guid postId, string commentText, ApplicationUser appUser)
        {
            return 
                new SharedImagesPostComment()
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
                new UserCommentedToSharedImagesPostDbNotification()
            {
                dbNotification = dbNotification,
                PostId = post.Id

            };
        }

        public override IISharedPost SharedPostFactory(ApplicationUser appUser, SharedPostModel sharedPostModel)
        {

            throw new Exception("cannot create shared post of shared post this method should not be called here");

        }

        public override IIPostCommentInteraction postCommentInteractionFactory(ApplicationUser sender, AddCommentInteractionModel addCommentInteractionModel)
        {
            return new SharedImagesPostCommentInteraction()
            {
                userId = sender.Id,
                PostCommentId = addCommentInteractionModel.commentId,
                InteractionTypeId = (short)addCommentInteractionModel.postInteractionType,
                dateInteracted = DateTime.UtcNow
            };
        }

    }
}
