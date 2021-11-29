using Microsoft.AspNetCore.SignalR;
using SocialDataAccess.Hub;
using SocialDataAccess.Models.KeyLessModels;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using SocialDataAccess.Models.NotificationData.NotificationsKeys;
using SocialDataAccess.Models.NotificationModel.PostsCommentsDbNotifications;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.Models.UsersRelationshipsModels;
using SocialDataAccess.MyTransietServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDataAccess.Models.NotificationData
{
    public class NotificationSender
    {

        public IHubContext<SocialHub> HubContext { get; }
        public AppNotificationSenderControllerServicesWrapper AppNotSenContrServicesWrapper { get; }
        public ApplicationUser AppUser { get; }
        AppNotificationSenderController appNotificationSenderController { get; }


        public NotificationSender(
             IHubContext<SocialHub> hubContext,
             AppNotificationSenderControllerServicesWrapper AppNotSenContrServicesWrapper,
             ApplicationUser appUser,
             INotificationGenericRepository<IIUserInteractedToPostDbNotification
                 ,IIUserCommentedToPostDbNotification,IIPost>
              notificationGenericRepository,
             TransietSevicesWrapper transietSevicesWrapper = null
            )
        {
            HubContext = hubContext;
            this.AppNotSenContrServicesWrapper = AppNotSenContrServicesWrapper;
            AppUser = appUser;
            appNotificationSenderController = new AppNotificationSenderController(AppNotSenContrServicesWrapper, AppUser,
                notificationGenericRepository,transietSevicesWrapper);
        }



        public async Task sendVideoProcessingResultNotification(bool isSuccessful)
        {



            AppNotificationTcpFunction appNotificationTcpFunction = (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
            {

                hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                SendAsync(HubClientsFunctionsNames.VideoProcessingResult, appNotification.AppNotificationModel.NotificationData);

            };

            AppNotification appNotification =
                new AppNotification(AppNotificationTypes.VideoProcessingResult,
                 AppUser.UserName, appNotificationTcpFunction, null);


            appNotification.AppNotificationModel.NotificationType = AppNotificationTypes.VideoProcessingResult;
            appNotification.AppNotificationModel.NotificationData.
                Add(NotificationVideoProcessingResultTypeKeys.ProcessingSucceeded, isSuccessful.ToString());

            await appNotificationSenderController.sendNotification(appNotification);

            return ;
        }

        public async Task<bool> sendUserMessageNotification(AppNotificationModel appNotificationModel)
        {

            AppNotificationTcpFunction appNotificationTcpFunction =
               async (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
                {

                    _ = hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                     SendAsync(HubClientsFunctionsNames.UserMessage,
                     appNotification.AppNotificationModel.NotificationData
                     );

                };


            AppNotification appNotification =
           new AppNotification(appNotificationModel
           , appNotificationTcpFunction, null);

            await appNotificationSenderController.sendNotification(appNotification);

            return true;

        }


        public async Task sendFriendRequestNotification(AppNotificationModel appNotificationModel)
        {

               AppNotificationTcpFunction appNotificationTcpFunction =
               async (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
               {

                   _ = hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                                SendAsync(HubClientsFunctionsNames.FriendRequest,
                                appNotification.AppNotificationModel.NotificationData
                                );

               };


            AppNotification appNotification =
            new AppNotification(appNotificationModel
            , appNotificationTcpFunction, null);

            await appNotificationSenderController.sendNotification(appNotification,AddToDataBase:false);

            return;

        }


        public async Task sendPostInteractionNotification(AppNotificationModel appNotificationModel)
        {



            Guid postId = Guid.Parse(appNotificationModel.NotificationData[UserInteractedToPostKeys.postId]);

            SocialDbNotificationAdd socialDbNotificationAdd =
                (notificationRep, notificationGenericRep) =>
                {
                    return notificationGenericRep.AddPostInteractionDbNotification(AppUser, postId);
                };


            AppNotificationTcpFunction appNotificationTcpFunction =
                async (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
                {

                    _ = hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                    SendAsync(HubClientsFunctionsNames.UserInteractedToPost,
                    appNotification.AppNotificationModel.NotificationData
                    );

                };


            AppNotification appNotification = new AppNotification(appNotificationModel,
                appNotificationTcpFunction, socialDbNotificationAdd);

            await appNotificationSenderController.sendNotification(appNotification,sendToMobile:false);

        }

        public async Task sendPostCommentNotification(AppNotificationModel appNotificationModel)
        {

            Guid postId = Guid.Parse(appNotificationModel.NotificationData[UserCommentedToPostKeys.postId]);

            SocialDbNotificationAdd socialDbNotificationAdd =
                (notificationRep, notificationGenericRep) =>
                {
                    return notificationGenericRep.AddPostCommentDbNotificaion(AppUser, postId);
                };


            AppNotificationTcpFunction appNotificationTcpFunction =
                async (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
                {

                    _ = hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                    SendAsync(HubClientsFunctionsNames.UserCommentedToPost,
                    appNotification.AppNotificationModel.NotificationData
                    );

                };


            AppNotification appNotification = new AppNotification(appNotificationModel,
                appNotificationTcpFunction, socialDbNotificationAdd);

            await appNotificationSenderController.sendNotification(appNotification,sendToMobile:false);

        }


        public async Task sendUserFriendRequestNotification(UserFriendshipRequest friendshipRequest,AppNotificationModel appNotificationModel)
        {


            SocialDbNotificationAdd socialDbNotificationAdd =
                        (notificationRep, notificationGenericRep) =>
                        {
                            return notificationRep.addFriendRequestNotification(friendshipRequest);
                        };

            AppNotificationTcpFunction appNotificationTcpFunction =
                async (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
                {

                    _ = hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                    SendAsync(HubClientsFunctionsNames.FriendRequest,
                    appNotification.AppNotificationModel.NotificationData
                    );

                };

            AppNotification appNotification = new AppNotification(appNotificationModel,
                appNotificationTcpFunction, socialDbNotificationAdd);

            await appNotificationSenderController.sendNotification(appNotification);


        }

        public async Task sendUserFriendRequestAcceptedNotification(FriendRelationship friendRelationship
            , AppNotificationModel appNotificationModel)
        {


            SocialDbNotificationAdd socialDbNotificationAdd =
                        (notificationRep, notificationGenericRep) =>
                        {
                            return notificationRep.addFriendRequestAcceptedNotification(friendRelationship,AppUser);
                        };

            AppNotificationTcpFunction appNotificationTcpFunction =
                async (IHubContext<SocialHub> hubContext, AppNotification appNotification) =>
                {

                    _ = hubContext.Clients.User(appNotification.AppNotificationModel.ReceiverUserName).
                    SendAsync(HubClientsFunctionsNames.FriendRequestAccepted,
                    appNotification.AppNotificationModel.NotificationData
                    );

                };

            AppNotification appNotification = new AppNotification(appNotificationModel,
                appNotificationTcpFunction, socialDbNotificationAdd);

            await appNotificationSenderController.sendNotification(appNotification);


        }

    }
}
