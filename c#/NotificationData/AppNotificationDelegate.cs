using Microsoft.AspNetCore.SignalR;
using SocialDataAccess.Hub;
using SocialDataAccess.Models.ModelsRepository.Notificcations;
using SocialDataAccess.Models.NotificationModel.PostsCommentsDbNotifications;
using SocialDataAccess.Models.NotificationModel.PostsInteractoinsDbNotifications;
using SocialDataAccess.Models.PostsModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDataAccess.Models.NotificationData
{

    public delegate void AppNotificationTcpFunction(IHubContext<SocialHub> hubContext,AppNotification appNotification);

    public delegate Task SocialDbNotificationAdd(INotificationRepository notificationRepository,
        INotificationGenericRepository<IIUserInteractedToPostDbNotification
            ,IIUserCommentedToPostDbNotification,IIPost> notificationGenericRepository);

}
