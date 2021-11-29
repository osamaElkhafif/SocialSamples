using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SocialDataAccess.ConfigurationClasses;
using SocialDataAccess.Models.ModelsRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDataAccess.Models.NotificationData
{
    public class MobileNotificationSender
    {

        public async Task SendNotificationToDevice(AppNotificationModel notification,IHostingEnvironment environment,ApiAppConstants apiAppConstants,
                                                    ILogger<MobileNotificationSender> logger,
                                                    ApplicationUser receiverUser,
                                                    IUsersDevicesTokensRepository usersDevicesTokensManipulation,
                                                    AppType appType)
        {


            try
            {

                Message message = new Message()
                {

                    Data = notification.NotificationData,
                    Token = notification.Token

                };

                if (appType == AppType.nativeAndroid)
                {
                    FirebaseApp firebaseApp = null;

                    string CredentialFilePath = environment.ContentRootPath + @"\" +
                                                apiAppConstants.ImportantProductionFilesDirectoryName +
                                                @"\" + apiAppConstants.FireBaseAuthorizationFileName;



                    firebaseApp = FirebaseApp.GetInstance("MyApp");

                    if (firebaseApp == null)
                    {
                        firebaseApp = FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile(CredentialFilePath)
                        }, "MyApp");
                    }



                    FirebaseMessaging firebaseMessaging = FirebaseMessaging.GetMessaging(firebaseApp);



                    var result = await firebaseMessaging.SendAsync(message);
                }
                else if (appType == AppType.Flutter)
                {
                    FirebaseApp firebaseAppFlutter = null;

                    string CredentialFilePathFlutter = environment.ContentRootPath + @"\" +
                                                apiAppConstants.ImportantProductionFilesDirectoryName +
                                                @"\" + apiAppConstants.FireBaseFlutterFile;



                    firebaseAppFlutter = FirebaseApp.GetInstance("MyAppFlutter");

                    if (firebaseAppFlutter == null)
                    {
                        firebaseAppFlutter = FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile(CredentialFilePathFlutter)
                        }, "MyAppFlutter");
                    }



                    FirebaseMessaging firebaseMessagingFlutter = FirebaseMessaging.GetMessaging(firebaseAppFlutter);


                    var resultFlutter = await firebaseMessagingFlutter.SendAsync(message);
                }
                

               
            }

            catch(FirebaseMessagingException ex)
            {

                if (ex.Message.Contains("Requested entity was not found"))
                {

                   await usersDevicesTokensManipulation.DeleteUserDeviceToken(receiverUser, notification.Token,appType);

                }

            }

            

        }

    }
}
