using SocialDataAccess.Models;
using SocialDataAccess.Models.KeyLessModels;
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
    public abstract class AbstractRelatedToPostsFactory<P>
        where P:class,IIPost
    {


        public static AbstractRelatedToPostsFactory<P> GetRelatedToPostsFactory()
        {

   
                if (typeof(P) == typeof(TextPost))
                {
                    return new RelatedToTextPostFactory<P>();
                }

                else if (typeof(P) == typeof(ImagesPost))
                {
                    return new RelatedToImagesPostFactory<P>();
                }

                else if (typeof(P) == typeof(VideoPost))
                {
                    return new RelatedToVideoPostFactory<P>();
                }

                else if (typeof(P) == typeof(SharedTextPost))
                {
                    return new RelatedToSharedTextPostFactory<P>();
                }

                else if (typeof(P) == typeof(SharedImagesPost))
                {
                    return new ReltaedToSharedImagesPostFactory<P>();
                }

                else if (typeof(P) == typeof(SharedVideoPost))
                {
                    return new RelatedToSharedVideoPostFactory<P>();
                }

                else
                {
                    throw new Exception("invalid data passed to method GetRelatedToPostsFactory");
                }



        }

        public abstract IIUserInteractedToPostDbNotification userInteractedToPostDbNotificationFactory
            (ApplicationUser sender, ApplicationUser receiver, IIPost post);

        public abstract IIPostInteraction postInteractionFactory(ApplicationUser appUser,
            AddPostInteraction addPostInteraction, IIPost post);

        public abstract IIPostComment postCommentFactory(Guid postId,string commentText, ApplicationUser appUser);

        public abstract IIUserCommentedToPostDbNotification userCommentedToPostFactory(ApplicationUser sender,
            ApplicationUser receiver, IIPost post);

        public abstract IISharedPost SharedPostFactory(ApplicationUser sender,
           SharedPostModel sharedPostModel);

        public abstract IIPostCommentInteraction postCommentInteractionFactory(ApplicationUser sender,
          AddCommentInteractionModel addCommentInteractionModel);
    }
}
