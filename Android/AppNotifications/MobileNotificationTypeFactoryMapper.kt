package com.app.androidsocialclient.AppNotifications

import android.content.Context
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.FriendRequestAcceptedNotificationFactory
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.FriendRequestNotificationFactory
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.UserMessageNotificationFactory
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.VideoProcessingResultNotificationFactory
import com.app.androidsocialclient.AppNotifications.mobileAndTcpNotificationsModels.FriendRequestAcceptedNotificationModel
import com.app.androidsocialclient.AppNotifications.mobileAndTcpNotificationsModels.FriendRequestNotificationModel
import com.app.androidsocialclient.HttpApiModels.ReceivedModels.UserMessageWrapperModel
import com.app.androidsocialclient.corefunctionalityTypes.ComponentsActiveStatus
import com.google.gson.Gson

class MobileNotificationTypeToFactoryMapper(
    private val context: Context,
    private val componentsActiveStatus: ComponentsActiveStatus
) {

    private val gson = Gson()

    fun mapNotificationTypeToNotificationFactory(notificationData: Map<String, String>) {

        val notificationType = notificationData[CommonNotificationsKeys.NotificationType]

        val data = notificationData["model"]?:notificationData["Model"]

        when (notificationType) {

            NotificationTypes.UserMessage -> {

                val userMessagewrapper = gson.fromJson(
                    data,
                    UserMessageWrapperModel::class.java
                )

                var message = userMessagewrapper.userMessages!![0]
                var userMessageNotificationModel = UserMessageNotificationModel()
                userMessageNotificationModel.Message = message.message
                userMessageNotificationModel.senderUserName = message.messageOwnerUserName
                userMessageNotificationModel.senderFullName = userMessagewrapper.secondUserData!!.userFullName

                UserMessageNotificationFactory(context, componentsActiveStatus).sendNotification(
                    userMessageNotificationModel
                )

            }

            NotificationTypes.FriendRequest -> {
                val friendRequestNotificationModel =
                    gson.fromJson(data, FriendRequestNotificationModel::class.java)
                FriendRequestNotificationFactory(context).sendNotification(
                    friendRequestNotificationModel
                )
            }

            NotificationTypes.FriendRequestAccepted -> {
                val friendRequestAcceptedNotificationModel = gson.fromJson(
                    data, FriendRequestAcceptedNotificationModel::class.java
                )
                FriendRequestAcceptedNotificationFactory(context).sendNotification(
                    friendRequestAcceptedNotificationModel
                )
            }

            NotificationTypes.VideoProcessingResult ->{

                var result = notificationData["ProcessingSucceeded"].toBoolean();

                val videoProcessingResultFactory = VideoProcessingResultNotificationFactory(context)

                videoProcessingResultFactory.sendNotification(result)

            }


        }

    }

}