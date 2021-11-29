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
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.MobileNotificationsExtras.UserMessageNotificationExtrasKeys
import com.app.androidsocialclient.AppNotifications.NotificationTypes
import com.app.androidsocialclient.AppNotifications.mobileAndTcpNotificationsModels.FriendRequestNotificationModel
import com.app.androidsocialclient.R
import com.app.androidsocialclient.broadcastReceivers.NewNotificationToLaunchAppBroadcast
import com.app.androidsocialclient.mainActivity.MainActivity

class VideoProcessingResultNotificationFactory(context:Context):MobileNotificationFactory(context) {

    var soundUrl: Uri =
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    private val mobileNotificationsUtilities=MobileNotificationsUtilities(notificationManager)

    private fun buildNotification(processingResult:Boolean)
            :NotificationCompat.Builder{

        var notificationContent = if(processingResult)
        context.resources.getString(R.string.processingSucceeded)
        else context.resources.getString(R.string.processingFailed)

        var notificationTitle = if(processingResult)
            context.resources.getString(R.string.VideoProcessedSuccessfully)
        else context.resources.getString(R.string.VideoFailedProcessing)

        val notificationBuilder = NotificationCompat.Builder(context, NotificationChannelsIds.HighImportanceNotificationChannel )
            .setContentTitle(notificationTitle)
            .setSmallIcon(R.drawable.ic_socialtransparent)
            .setAutoCancel(true)
            .setStyle(NotificationCompat.BigTextStyle()
            .bigText(notificationContent))
            .setContentTitle(notificationTitle)
            .setSound(soundUrl)

        return notificationBuilder

    }

    fun sendNotification(processingResult:Boolean){

        var notification = buildNotification(processingResult).build()
        var notificationId = mobileNotificationsUtilities.newId(NotificationTypes.VideoProcessingResult)
        notificationManager.notify(notificationId,notification)

    }




}