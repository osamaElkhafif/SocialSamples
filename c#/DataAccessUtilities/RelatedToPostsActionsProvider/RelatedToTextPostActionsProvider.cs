using SocialDataAccess.Models;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using SocialDataAccess.Models.ModelsRepository.PostsRepositories;
using SocialDataAccess.Models.NotificationModel;
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
     public class RelatedToTextPostActionsProvider:AbstractRelatedToPostsActionsProvider
    {

        public IServiceProvider ServiceProvider { get; }

        public RelatedToTextPostActionsProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }


        public override IPostsInteractionsRepository<IIPostInteraction, IIPost> getPostsInteractionRepository()
        {

            return
                   (IPostsInteractionsRepository<IIPostInteraction, IIPost>)
                   ServiceProvider.GetService(typeof(IPostsInteractionsRepository<TextPostInteraction, TextPost>));

        }

        public override IGetPostsGenericRepository<IIPost> getPostsGenericRepository()
        {
            return
             (IGetPostsGenericRepository<IIPost>)
             ServiceProvider.GetService(typeof(IGetPostsGenericRepository<TextPost>));
        }

        public override INotificationGenericRepository
            <IIUserInteractedToPostDbNotification, 
            IIUserCommentedToPostDbNotification, IIPost> GetNotificationGenericRepository()
        {
            return (INotificationGenericRepository<IIUserInteractedToPostDbNotification,
                IIUserCommentedToPostDbNotification, IIPost>)
                ServiceProvider.GetService(typeof(INotificationGenericRepository
                <UserInteractedToTextPostDbNotification, UserCommentedToTextPostDbNotification, TextPost>));
        }

        public override IPostsCommentsRepository<IIPost, IIPostComment> GetPostsCommentRepository()
        {
            return
                (IPostsCommentsRepository<IIPost, IIPostComment>)
                 ServiceProvider
                .GetService(typeof(IPostsCommentsRepository<TextPost, TextPostComment>));
        }

        public override ISharedPostsRepository<IIPost,IISharedPost> GetSharedPostsRepository()
        {
            return
                (ISharedPostsRepository<IIPost,IISharedPost>)
                 ServiceProvider
                 .GetService(typeof(ISharedPostsRepository<TextPost,SharedTextPost>));

        }

        public override ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction> 
            getPostCommentInteractionRepository()
        {
            return
                    (ICommentsInteractionRepository<IIPost, IIPostComment, IIPostCommentInteraction>)
                     ServiceProvider
                     .GetService(typeof(ICommentsInteractionRepository<TextPost, TextPostComment
                     , TextPostCommentInteraction>));
        }


    }
}
