using SocialDataAccess.Models;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using SocialDataAccess.Models.ModelsRepository.PostsRepositories;
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
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.RelatedToPostsActionsProvider
{
    public class RelatedToSharedImagesPostActionsProvdier:AbstractRelatedToPostsActionsProvider
    {

        public IServiceProvider ServiceProvider { get; }

        public RelatedToSharedImagesPostActionsProvdier(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public override IPostsInteractionsRepository<IIPostInteraction,IIPost> getPostsInteractionRepository()
        {

            return
                   (IPostsInteractionsRepository<IIPostInteraction, IIPost>)
                   ServiceProvider.
                   GetService(typeof(IPostsInteractionsRepository<SharedImagesPostInteraction, SharedImagesPost>));

        }

        public override IGetPostsGenericRepository<IIPost> getPostsGenericRepository()
        {
            return
             (IGetPostsGenericRepository<IIPost>)
             ServiceProvider.GetService(typeof(IGetPostsGenericRepository<SharedImagesPost>));
        }

        public override INotificationGenericRepository
            <IIUserInteractedToPostDbNotification,
            IIUserCommentedToPostDbNotification ,IIPost> GetNotificationGenericRepository()
        {
            return
                (INotificationGenericRepository<IIUserInteractedToPostDbNotification,
                IIUserCommentedToPostDbNotification, IIPost>)
                ServiceProvider
                .GetService(typeof(INotificationGenericRepository
                <UserInteractedToSharedImagesPostDbNotification, UserCommentedToSharedImagesPostDbNotification,SharedImagesPost>));
        }

        public override IPostsCommentsRepository<IIPost, IIPostComment> GetPostsCommentRepository()
        {
            return
                (IPostsCommentsRepository<IIPost, IIPostComment>)
                 ServiceProvider
                .GetService(typeof(IPostsCommentsRepository<SharedImagesPost, SharedImagesPostComment>));
        }

        public override ISharedPostsRepository<IIPost,IISharedPost> GetSharedPostsRepository()
        {

            throw new Exception("cannot create shared post for a shared post this method should not be called here");

        }

        public override ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction> getPostCommentInteractionRepository()
        {
            return
                    (ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction>)
                     ServiceProvider
                     .GetService(typeof(ICommentsInteractionRepository<SharedImagesPost, 
                     SharedImagesPostComment, SharedImagesPostCommentInteraction>));
        }


    }
}
