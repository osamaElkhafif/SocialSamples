package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory

import android.app.PendingIntent
import android.content.ContentResolver
import android.content.Context
import android.content.Intent
import android.media.AudioAttributes
import android.net.Uri
import android.service.notification.StatusBarNotification
import androidx.core.app.NotificationCompat
import com.app.androidsocialclient.AppNotifications.*
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsIds
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.MobileNotificationsExtras.UserMessageNotificationExtrasKeys
import com.app.androidsocialclient.R
import com.app.androidsocialclient.corefunctionalityTypes.AppComponentsNames
import com.app.androidsocialclient.corefunctionalityTypes.ComponentsActiveStatus
import com.app.androidsocialclient.mainActivity.MainActivity

class UserMessageNotificationFactory(
    context: Context,
    private val componentsActiveStatus: ComponentsActiveStatus
) : MobileNotificationFactory(context) {

    var soundUrl: Uri =
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    private val mobileNotificationsUtilities = MobileNotificationsUtilities(notificationManager)

    private fun getNotificationBySenderUserName(senderUserName: String): StatusBarNotification? {


        return mobileNotificationsUtilities.getNotificationsOfType(NotificationTypes.UserMessage)
            .filter {
                it.notification.extras[UserMessageNotificationExtrasKeys.SenderUserName] == senderUserName
            }.firstOrNull()


    }


    private fun buildNotification(userMessageNotificationModel: UserMessageNotificationModel): NotificationCompat.Builder {

        val intent = Intent(context, MainActivity::class.java)

        val pendingIntent =
            PendingIntent.getActivity(context, 2, intent, PendingIntent.FLAG_ONE_SHOT)

        val notificationBuilder = NotificationCompat.Builder(
            context,
            NotificationChannelsIds.HighImportanceNotificationChannel
        )
            .setContentTitle(userMessageNotificationModel.senderFullName)
            .setContentText(userMessageNotificationModel.Message)
            .setSmallIcon(R.drawable.ic_socialtransparent)
            .setContentIntent(pendingIntent)
            .setAutoCancel(true)
            .setSound(soundUrl)

        notificationBuilder.extras.putString(
            UserMessageNotificationExtrasKeys.SenderUserName,
            userMessageNotificationModel.senderUserName
        )

        return notificationBuilder
    }


    fun sendNotification(userMessageNotificationModel: UserMessageNotificationModel) {

        if (componentsActiveStatus.userNameOfMessagingActiveUser != userMessageNotificationModel.senderUserName) {

            val statusBarNotification =
                getNotificationBySenderUserName(userMessageNotificationModel.senderUserName!!)
            val notification = statusBarNotification?.notification

            if (notification != null) {

                val numberOfMessages =
                    notification.extras.getInt(UserMessageNotificationExtrasKeys.NumberOfMessages)

                val notificationBuilder = buildNotification(userMessageNotificationModel)

                notificationBuilder.setContentText((numberOfMessages + 1).toString()
                        + ' ' + context.resources.getString(R.string.messages))
                notificationBuilder.extras.putInt(
                    UserMessageNotificationExtrasKeys.NumberOfMessages,
                    numberOfMessages + 1
                )

                notificationManager.notify(statusBarNotification.id, notificationBuilder.build())

            } else {

                val id = mobileNotificationsUtilities.newId(NotificationTypes.UserMessage)

                val notificationBuilder = buildNotification(userMessageNotificationModel)

                notificationBuilder.extras
                    .putInt(UserMessageNotificationExtrasKeys.NumberOfMessages, 1)

                notificationManager.notify(id, notificationBuilder.build())

            }

        }


    }


}