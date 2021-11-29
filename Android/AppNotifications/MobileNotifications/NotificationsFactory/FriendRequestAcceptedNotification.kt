package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory

import android.app.PendingIntent
import android.content.ContentResolver
import android.content.Context
import android.content.Intent
import android.media.AudioAttributes
import android.net.Uri
import androidx.core.app.NotificationCompat
import com.app.androidsocialclient.AppNotifications.MobileNotifications.AcivityIntentsTypes
import com.app.androidsocialclient.AppNotifications.MobileNotifications.ActivityIntentsExtrasKeys
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsIds
import com.app.androidsocialclient.AppNotifications.NotificationTypes
import com.app.androidsocialclient.AppNotifications.mobileAndTcpNotificationsModels.FriendRequestAcceptedNotificationModel
import com.app.androidsocialclient.AppNotifications.mobileAndTcpNotificationsModels.FriendRequestNotificationModel
import com.app.androidsocialclient.R
import com.app.androidsocialclient.broadcastReceivers.NewNotificationToLaunchAppBroadcast

class FriendRequestAcceptedNotificationFactory(context: Context):MobileNotificationFactory(context) {


    var soundUrl: Uri =
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    private val mobileNotificationsUtilities=MobileNotificationsUtilities(notificationManager)

    private fun buildNotification(friendRequestNotificationModel: FriendRequestAcceptedNotificationModel)
            : NotificationCompat.Builder{

        val intent = Intent(context, NewNotificationToLaunchAppBroadcast::class.java)
        intent.action = "newNotification"
        intent.putExtra(ActivityIntentsExtrasKeys.IntentType, AcivityIntentsTypes.FriendRequestAccepted.name)
        intent.putExtra(
            ActivityIntentsExtrasKeys.userName,
            friendRequestNotificationModel.requestAcceptorUserData?.userUserName)

        val pendingIntent = PendingIntent.getBroadcast(context, 2, intent, PendingIntent.FLAG_ONE_SHOT)

        val notificationBuilder = NotificationCompat.Builder(context, NotificationChannelsIds.HighImportanceNotificationChannel )
            .setContentTitle(context.resources.getString(R.string.FriendRequestApproval))
            .setContentText(context.resources.getString(R.string.YourFriendRequestAcceptedBy)+
                    " ${friendRequestNotificationModel.requestAcceptorUserData?.userFullName}")
            .setSmallIcon(R.drawable.ic_socialtransparent)
            .setContentIntent(pendingIntent)
            .setAutoCancel(true)
            .setSound(soundUrl)

        return notificationBuilder

    }

    fun sendNotification(friendRequestNotificationModel: FriendRequestAcceptedNotificationModel){

        var notification = buildNotification(friendRequestNotificationModel).build()
        var notificationId = mobileNotificationsUtilities.newId(NotificationTypes.FriendRequestAccepted)
        notificationManager.notify(notificationId,notification)

    }

}