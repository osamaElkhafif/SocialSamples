using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SocialDataAccess.ConfigurationClasses;
using SocialDataAccess.Models.ModelsRepository;
using SocialDataAccess.Models.NotificationModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDataAccess.Models.NotificationData
{
     public class VideoProcessedMobileNotification
    {

        public UserManager<ApplicationUser> UserManager { get; }
        public IUsersDevicesTokensRepository UserDevicesTokensManipulation { get; }
        public ApiAppConstants ApiAppConstants { get; }
        public ILogger<MobileNotificationSender> LoggerMNS { get; }
        public IHostingEnvironment Environment { get; }

        public VideoProcessedMobileNotification(UserManager<ApplicationUser> UserManager,
            IUsersDevicesTokensRepository UserDevicesTokensManipulation,
            ApiAppConstants ApiAppConstants,
            ILogger<MobileNotificationSender> LoggerMNS,
            IHostingEnvironment Environment)
        {
            this.UserManager = UserManager;
            this.UserDevicesTokensManipulation = UserDevicesTokensManipulation;
            this.ApiAppConstants = ApiAppConstants;
            this.LoggerMNS = LoggerMNS;
            this.Environment = Environment;
        }

       

        public async Task SendNotification(AppNotificationModel mobileNotification)
        {
        

            mobileNotification.NotificationData.Add("MobileNotificationType", mobileNotification.NotificationType);

            ApplicationUser ReceiverUser = await UserManager.FindByNameAsync(mobileNotification.ReceiverUserName);

            MobileNotificationSender mobileNotificationSender = new MobileNotificationSender();

            List<UserDeviceToken> userDevicesTokens = 
                await UserDevicesTokensManipulation.GetUserDevicesTokens(ReceiverUser,AppType.nativeAndroid);
            List<UserDeviceToken> userDevicesTokensFlutter = 
                await UserDevicesTokensManipulation.GetUserDevicesTokens(ReceiverUser, AppType.Flutter);

            if (userDevicesTokens.Count > 0)
            {

                foreach (UserDeviceToken userDeviceToken in userDevicesTokens)
                {
                    mobileNotification.Token = userDeviceToken.DeviceToken;

                    _ = mobileNotificationSender.SendNotificationToDevice(mobileNotification, Environment,
                                                    ApiAppConstants, LoggerMNS, ReceiverUser, 
                                                    UserDevicesTokensManipulation,AppType.nativeAndroid);
                }

                foreach (UserDeviceToken userDeviceToken in userDevicesTokensFlutter)
                {
                    mobileNotification.Token = userDeviceToken.DeviceToken;

                    _ = mobileNotificationSender.SendNotificationToDevice(mobileNotification, Environment,
                                                    ApiAppConstants, LoggerMNS, ReceiverUser,
                                                    UserDevicesTokensManipulation, AppType.Flutter);
                }

            }
        }

    }
}
