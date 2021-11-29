package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory

import android.app.PendingIntent
import android.content.ContentResolver
import android.content.Context
import android.content.Intent
import android.media.AudioAttributes
import android.net.Uri
import android.service.notification.StatusBarNotification
import androidx.core.app.NotificationCompat
import com.app.androidsocialclient.AppNotifications.MobileNotifications.MobileNotificationWrapper
import com.app.androidsocialclient.AppNotifications.MobileNotifications.MobileSpecificModels.FileUploadingProgressData
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsIds
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.MobileNotificationsExtras.FileUploadingProgressMobileNotificationExtrasKeys
import com.app.androidsocialclient.AppNotifications.NotificationTypes
import com.app.androidsocialclient.R
import com.app.androidsocialclient.broadcastReceivers.NotificationCloseBroadcastReceiver
import com.app.androidsocialclient.broadcastReceivers.TestBroadcastReceiver

enum class FileType{
    videoFile
}

class UploadingFileProgressNotificationFactory(context: Context):MobileNotificationFactory(context) {

    var soundUrl: Uri =
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    private val mobileNotificationsUtilities=MobileNotificationsUtilities(notificationManager)
    var notificationId:Int? = null

    private fun getUploadingFileProgressActiveNotificatoinsByVideoTitle(videoTitle:String):StatusBarNotification?{

          return  mobileNotificationsUtilities.getNotificationsOfType(NotificationTypes.UploadingFileProgress)
                .filter {
                    it.notification.
                    extras[FileUploadingProgressMobileNotificationExtrasKeys.VideoTitle] == videoTitle
                }.firstOrNull()

    }


    private fun customizeBuilderAccordingToType(notificationBuilder:NotificationCompat.Builder,fileUploadingProgressData: FileUploadingProgressData)
    :NotificationCompat.Builder{

        if(fileUploadingProgressData.fileType == FileType.videoFile){

//            val intent = Intent(context,EndWorkersService::class.java)
//
//            intent.putExtra(EndWorkersServiceConstants.workerId,fileUploadingProgressData.workerId)
//            intent.putExtra(EndWorkersServiceConstants.notificationId,notificationId)
//
//            val pendingIntent = PendingIntent.getService(context,0,intent,0)

//            notificationBuilder.addAction(R.drawable.iconfinder___gear_settings_setting_wheel__3844439,
//            context.resources.getString(R.string.Cancel),pendingIntent)

            notificationBuilder.extras.putString(FileUploadingProgressMobileNotificationExtrasKeys.VideoTitle,
                fileUploadingProgressData.videoTitle)

        }

        return notificationBuilder

    }

    private fun getNotificationTitleFromFileType(fileType: FileType):String{
      return  if(fileType == FileType.videoFile){
           context.resources.getString(R.string.videoUploading)
        }
        else{
            return ""
        }
    }

    private fun buildNotification(fileUploadingProgressData: FileUploadingProgressData): NotificationCompat.Builder{

        val completedString = context.resources.getString(R.string.FileUploadPercentageCompleted)

        var intent = Intent(context,TestBroadcastReceiver::class.java)
            .apply {
                action = "notificationTest"

            }

        var pendingIntent = PendingIntent.getBroadcast(context,0,intent,0)

        var notificationBuilder = NotificationCompat.Builder(context, NotificationChannelsIds.HighImportanceNotificationChannel)
            .apply {
                setContentTitle(getNotificationTitleFromFileType(fileUploadingProgressData.fileType!!))
                setSmallIcon(R.drawable.iconfinder_video_call_2639945)
                setOngoing(true)
                setOnlyAlertOnce(true)
                setProgress(100,fileUploadingProgressData.fileProgressPercentage,false)
                setContentText(completedString +":${fileUploadingProgressData.fileProgressPercentage}")
                setTicker(getNotificationTitleFromFileType(fileUploadingProgressData.fileType!!))
                addAction(R.drawable.icon_close,"close",pendingIntent)
                setSound(soundUrl)

            }

       var modifiedNotificationBuilder = customizeBuilderAccordingToType(notificationBuilder,fileUploadingProgressData)

        return modifiedNotificationBuilder
    }


    fun getNotificationToSend(fileUploadingProgressData: FileUploadingProgressData):MobileNotificationWrapper{

        val statusBarNotification = getUploadingFileProgressActiveNotificatoinsByVideoTitle(fileUploadingProgressData.videoTitle!!)

        val notification = statusBarNotification?.notification

        if(notification!= null){
            notificationId = statusBarNotification.id
        }
        else{

            notificationId = MobileNotificationsUtilities(notificationManager).newId(NotificationTypes.UploadingFileProgress)

        }

        val notificationBuilder = buildNotification(fileUploadingProgressData)

        return MobileNotificationWrapper(notificationId!!,notificationBuilder.build())
    }

    fun sendNotificaiton(fileUploadingProgressData: FileUploadingProgressData){

         val notificationWrapper = getNotificationToSend(fileUploadingProgressData)

        notificationManager.notify(notificationWrapper.notificationId,notificationWrapper.notification)

    }


}