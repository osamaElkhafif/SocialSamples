using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SocialDataAccess.ConfigurationClasses;
using SocialDataAccess.Hub;
using SocialDataAccess.Models.ModelsRepository;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using System.Security.Claims;
namespace SocialDataAccess.Models.NotificationData
{
    public class AppNotificationSenderControllerServicesWrapper
    {

        public ILogger<AppNotificationSenderController> Logger { get; }
        public IHostingEnvironment Environment { get; }
        public ApiAppConstants ApiAppConstants { get; }
        public IUsersDevicesTokensRepository UserDevicesTokensManipulation { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public ILogger<MobileNotificationSender> LoggerMNS { get; }
        public IHubContext<SocialHub> HubContext { get; }
        public INotificationRepository NotificationManipulation { get; }
        public IConfiguration Configuration { get; }

        public AppNotificationSenderControllerServicesWrapper(
            ILogger<AppNotificationSenderController> logger,
                    IHostingEnvironment environment,
                    ApiAppConstants apiAppConstants,
                     IUsersDevicesTokensRepository userDevicesTokensManipulation,
                     UserManager<ApplicationUser> userManager,
                     ILogger<MobileNotificationSender> loggerMNS,
                     IHubContext<SocialHub> hubContext,
                     INotificationRepository notificationManipulation,
                     IConfiguration configuration
                     )
        {
            Logger = logger;
            Environment = environment;
            ApiAppConstants = apiAppConstants;
            UserDevicesTokensManipulation = userDevicesTokensManipulation;
            UserManager = userManager;
            LoggerMNS = loggerMNS;
            HubContext = hubContext;
            NotificationManipulation = notificationManipulation;
            Configuration = configuration;
        }

    }
}
