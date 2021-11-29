using SocialDataAccess.Models;
using SocialDataAccess.Models.PostsInteractios;
using SocialDataAccess.Models.PostsInteractios.SharedPostsInteractions;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.Models.PostsModels.SharedPosts;
using SocialDataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.Factories
{
    //static partial class RelatedToPostsFactory<T> where T : class, IPost
    //{

    //    public static IPostInteraction PostInteractionFactory(ApplicationUser appUser,
    //        AddPostInteraction addPostInteraction,IPost post)
    //    {
    //        if(typeof(T) == typeof(TextPost))
    //        {

    //            return new TextPostInteraction()
    //            {
    //                userId = appUser.Id,
    //                InteractionTypeId = (int)addPostInteraction.postInteractionType,
    //                PostId = post.Id
    //            };

    //        }

    //        else if (typeof(T) == typeof(ImagesPost))
    //        {

    //            return new ImagesPostInteraction()
    //            {
    //                userId = appUser.Id,
    //                InteractionTypeId = (int)addPostInteraction.postInteractionType,
    //                PostId = post.Id
    //            };

    //        }

    //        else if (typeof(T) == typeof(VideoPost))
    //        {

    //            return new VideoPostInteraction()
    //            {
    //                userId = appUser.Id,
    //                InteractionTypeId = (int)addPostInteraction.postInteractionType,
    //                PostId = post.Id
    //            };

    //        }

    //        else if (typeof(T) == typeof(SharedTextPost))
    //        {

    //            return new SharedTextPostInteraction()
    //            {
    //                userId = appUser.Id,
    //                InteractionTypeId = (int)addPostInteraction.postInteractionType,
    //                PostId = post.Id
    //            };

    //        }

    //        else if (typeof(T) == typeof(SharedImagesPost))
    //        {

    //            return new SharedImagesPostInteraction()
    //            {
    //                userId = appUser.Id,
    //                InteractionTypeId = (int)addPostInteraction.postInteractionType,
    //                PostId = post.Id
    //            };

    //        }


    //        else if (typeof(T) == typeof(SharedVideoPostInteraction))
    //        {

    //            return new SharedVideoPostInteraction()
    //            {
    //                userId = appUser.Id,
    //                InteractionTypeId = (int)addPostInteraction.postInteractionType,
    //                PostId = post.Id
    //            };

    //        }

    //        else
    //        {
    //            throw new Exception("invalid data passed to method PostInteractionFactory");
    //        }

    //    }

    //}
}
