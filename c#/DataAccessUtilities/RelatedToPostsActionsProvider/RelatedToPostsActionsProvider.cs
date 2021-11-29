using SocialDataAccess.Models.KeyLessModels;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using SocialDataAccess.Models.ModelsRepository.PostsRepositories;
using SocialDataAccess.Models.NotificationModel.PostsCommentsDbNotifications;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsComments;
using SocialDataAccess.Models.PostsCommentsInteractions;
using SocialDataAccess.Models.PostsInteractios;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.Models.PostsModels.SharedPosts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.RelatedToPostsActionsProvider
{
    public abstract class AbstractRelatedToPostsActionsProvider
    {

        public static AbstractRelatedToPostsActionsProvider getRelatedToPostsActionsProvider(
            PostTypeEnum postTypeEnum, IServiceProvider serviceProvider)
        {

            if (postTypeEnum == PostTypeEnum.Text)
            {
                return new RelatedToTextPostActionsProvider(serviceProvider);
            }

            else if (postTypeEnum == PostTypeEnum.Images)
            {
                return new RelatedToImagesPostActionsProvider(serviceProvider);
            }

            else if (postTypeEnum == PostTypeEnum.Video)
            {
                return new RelatedToVideoPostActionsProvider(serviceProvider);
            }

            else if (postTypeEnum == PostTypeEnum.SharedText)
            {
                return new RelatedToSharedTextPostActionsProvider(serviceProvider);
            }

            else if (postTypeEnum == PostTypeEnum.SharedImages)
            {
                return new RelatedToSharedImagesPostActionsProvdier(serviceProvider);
            }

            else if (postTypeEnum == PostTypeEnum.SharedVideo)
            {
                return new RelatedToSharedVideoPostActionsProvider(serviceProvider);
            }

            else
            {
                throw new Exception("invalid data passed to method getRelatedToPostsActionsProvider");
            }

        }


        public abstract IPostsInteractionsRepository<IIPostInteraction, IIPost> getPostsInteractionRepository();

        public abstract IGetPostsGenericRepository<IIPost> getPostsGenericRepository();

        public abstract INotificationGenericRepository<IIUserInteractedToPostDbNotification
            ,IIUserCommentedToPostDbNotification ,IIPost>
            GetNotificationGenericRepository();

        public abstract IPostsCommentsRepository<IIPost, IIPostComment> GetPostsCommentRepository();

        public abstract ISharedPostsRepository<IIPost,IISharedPost> GetSharedPostsRepository();

        public abstract ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction>
            getPostCommentInteractionRepository();



    }
}
