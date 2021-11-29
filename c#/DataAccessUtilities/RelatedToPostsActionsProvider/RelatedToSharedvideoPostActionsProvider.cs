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
     public class RelatedToSharedVideoPostActionsProvider:AbstractRelatedToPostsActionsProvider
    {

        public IServiceProvider ServiceProvider { get; }

        public RelatedToSharedVideoPostActionsProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }


        public override IPostsInteractionsRepository<IIPostInteraction, IIPost> getPostsInteractionRepository()
        {

            return
                   (IPostsInteractionsRepository<IIPostInteraction, IIPost>)
                   ServiceProvider.
                   GetService(typeof(IPostsInteractionsRepository<SharedVideoPostInteraction, SharedVideoPost>));

        }

        public override IGetPostsGenericRepository<IIPost> getPostsGenericRepository()
        {
            return
             (IGetPostsGenericRepository<IIPost>)
             ServiceProvider.GetService(typeof(IGetPostsGenericRepository<SharedVideoPost>));
        }

        public override INotificationGenericRepository<IIUserInteractedToPostDbNotification,
            IIUserCommentedToPostDbNotification, IIPost> GetNotificationGenericRepository()
        {
            return (INotificationGenericRepository<IIUserInteractedToPostDbNotification,
                IIUserCommentedToPostDbNotification, IIPost>)
                ServiceProvider.
                GetService(typeof(INotificationGenericRepository
                <UserInteractedToSharedVideoPostDbNotification, UserCommentedToSharedVideoPostDbNotification, SharedVideoPost>));
        }

        public override IPostsCommentsRepository<IIPost, IIPostComment> GetPostsCommentRepository()
        {
            return
                (IPostsCommentsRepository<IIPost, IIPostComment>)
                 ServiceProvider
                .GetService(typeof(IPostsCommentsRepository<SharedVideoPost, SharedVideoPostComment>));
        }

        public override ISharedPostsRepository<IIPost,IISharedPost> GetSharedPostsRepository()
        {

            throw new Exception("cannot create shared post for a shared post this method should not be called here");

        }

        public override ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction>
            getPostCommentInteractionRepository()
        {
            return
                    (ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction>)
                     ServiceProvider
                     .GetService(typeof(ICommentsInteractionRepository<SharedVideoPost, SharedVideoPostComment
                     , SharedVideoPostCommentInteraction>));
        }

    }
}
