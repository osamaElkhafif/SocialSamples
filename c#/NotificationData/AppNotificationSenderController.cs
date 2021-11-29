using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialDataAccess.ConfigurationClasses;
using SocialDataAccess.DataAccessUtilities.RelatedToPostsActionsProvider;
using SocialDataAccess.Hub;
using SocialDataAccess.Models.ModelsRepository;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using SocialDataAccess.Models.NotificationModel.PostsCommentsDbNotifications;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsModels;
using SocialDataAccess.MyTransietServices;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialDataAccess.Models.NotificationData
{
     public class AppNotificationSenderController
    {
        public AppNotificationSenderControllerServicesWrapper AppNotSerContrServicesWrapper { get; }
        public ApplicationUser AppUser { get; }
        public INotificationGenericRepository<IIUserInteractedToPostDbNotification
            ,IIUserCommentedToPostDbNotification,IIPost> NotificationGenericRepository { get; }
        public TransietSevicesWrapper TransietSevicesWrapper { get; }

        public AppNotificationSenderController(
            AppNotificationSenderControllerServicesWrapper AppNotSerContrServicesWrapper,
            ApplicationUser appUser,
            INotificationGenericRepository<IIUserInteractedToPostDbNotification
                ,IIUserCommentedToPostDbNotification, IIPost>
              notificationGenericRepository,
            TransietSevicesWrapper transietSevicesWrapper = null
           )
        {
            this.AppNotSerContrServicesWrapper = AppNotSerContrServicesWrapper;
            AppUser = appUser;
            NotificationGenericRepository = notificationGenericRepository;
            TransietSevicesWrapper = transietSevicesWrapper;
        }


        public async Task sendNotification(AppNotification appNotification,bool sendToTCP = true, bool sendToMobile = true,bool AddToDataBase=true
            ,ApplicationUser userToSendTo = null)
        {


            appNotification.AppNotificationModel.NotificationData.Add(CommonNotificationKeys.NotificationType,appNotification.AppNotificationModel.NotificationType);

            if (sendToTCP)
            {
                sendToTcp(appNotification);
            }

            if (AddToDataBase)
            {
               await AddNotificationToDataBase(appNotification);
            }

            if (sendToMobile)
            {
                await this.sendToMobile(appNotification);
            }

        }

        private void sendToTcp(AppNotification AppNotification)
        {
            if (AppNotification.AppNotificationTcpFunction != null)
            {
                AppNotification.AppNotificationTcpFunction(AppNotSerContrServicesWrapper.HubContext,AppNotification);
            }
        }

        private async Task sendToMobile(AppNotification AppNotification)
        {

            ApplicationUser ReceiverUser = await AppNotSerContrServicesWrapper.UserManager
                    .FindByNameAsync(AppNotification.AppNotificationModel.ReceiverUserName);

            MobileNotificationSender mobileNotificationSender = new MobileNotificationSender();

            List<UserDeviceToken> userDevicesTokens = await AppNotSerContrServicesWrapper
                .UserDevicesTokensManipulation.GetUserDevicesTokens(ReceiverUser,AppType.nativeAndroid);

            List<UserDeviceToken> userDevicesTokensFlutter = await AppNotSerContrServicesWrapper
                 .UserDevicesTokensManipulation.GetUserDevicesTokens(ReceiverUser, AppType.Flutter);

            if (userDevicesTokens.Count > 0)
            {

                foreach (UserDeviceToken userDeviceToken in userDevicesTokens)
                {
                    AppNotification.AppNotificationModel.Token = userDeviceToken.DeviceToken;

                    await mobileNotificationSender.SendNotificationToDevice
                        (AppNotification.AppNotificationModel,AppNotSerContrServicesWrapper.Environment,
                                                   AppNotSerContrServicesWrapper.ApiAppConstants,
                                                   AppNotSerContrServicesWrapper.LoggerMNS, 
                                                   ReceiverUser,AppNotSerContrServicesWrapper.UserDevicesTokensManipulation,
                                                   AppType.nativeAndroid);
                }

            }

            if (userDevicesTokensFlutter.Count > 0)
            {
                foreach (UserDeviceToken userDeviceToken in userDevicesTokensFlutter)
                {
                    AppNotification.AppNotificationModel.Token = userDeviceToken.DeviceToken;

                    await mobileNotificationSender.SendNotificationToDevice
                        (AppNotification.AppNotificationModel, AppNotSerContrServicesWrapper.Environment,
                                                   AppNotSerContrServicesWrapper.ApiAppConstants,
                                                   AppNotSerContrServicesWrapper.LoggerMNS,
                                                   ReceiverUser, AppNotSerContrServicesWrapper.UserDevicesTokensManipulation,
                                                   AppType.Flutter);
                }
            }
        }

        private async Task<bool> AddNotificationToDataBase(AppNotification AppNotification)
        {


            if(AppNotification.SocialDbNotificationAddCallBack != null)
            {
                await AppNotification.SocialDbNotificationAddCallBack(AppNotSerContrServicesWrapper.NotificationManipulation
                    ,NotificationGenericRepository);
            }

            return true;

        }

       
    }
}




//if (appNotification.AppNotificationModel.NotificationType == AppNotificationTypes.UserMessage)
//{
//    appNotification.AppNotificationModel.NotificationData.Add(NotificationUserMessageTypeKeys.senderFullName
//        , AppUser.FirstName + " " + AppUser.LastName);

//    appNotification.AppNotificationModel.NotificationData.Add(NotificationUserMessageTypeKeys.senderUserName, AppUser.UserName);

//}