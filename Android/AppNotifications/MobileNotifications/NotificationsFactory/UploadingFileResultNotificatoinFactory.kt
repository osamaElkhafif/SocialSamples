package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory

import android.content.ContentResolver
import android.content.Context
import android.media.AudioAttributes
import android.net.Uri
import androidx.core.app.NotificationCompat
import com.app.androidsocialclient.AppNotifications.MobileNotifications.MobileSpecificModels.FileUploadingResultData
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsIds
import com.app.androidsocialclient.AppNotifications.NotificationTypes
import com.app.androidsocialclient.R

class UploadingFileResultNotificatoinFactory(context: Context):MobileNotificationFactory(context) {

    var soundUrl: Uri =
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    private lateinit var fileUploadingResultData:FileUploadingResultData

     private val mobileNotificationsUtilities = MobileNotificationsUtilities(notificationManager)

    private fun customNotificationBuilderAccordingToFileType(notificationBuilder:NotificationCompat.Builder){

        if(fileUploadingResultData.fileType == FileType.videoFile){
            if(fileUploadingResultData.succeeded!!){

                val text = context.resources.getString(R.string.videoWithTitle)+
                        ":\n\"" + fileUploadingResultData.VideoTitle + "\"\n" +
                        context.resources.getString(R.string.AddedSuccessfully)+"\"\n" +
                        context.resources.getString(R.string.ItWillBeAvailable)


                notificationBuilder.setStyle(NotificationCompat.BigTextStyle()
                    .bigText(text))
                    .setContentTitle(context.resources.getString(R.string.VideoAddedSuccessfully))

            }
            else{

                val text = context.resources.getString(R.string.videoWithTitle)+
                        ":\n\" " + fileUploadingResultData.VideoTitle + "\"\n" +
                        context.resources.getString(R.string.FailedAdding)

                notificationBuilder.setStyle(NotificationCompat.BigTextStyle()
                    .bigText(text))
                    .setContentTitle(context.resources.getString(R.string.VideoFailedAdding))
            }


        }

    }

    private fun buildNotification():NotificationCompat.Builder{

        var notificationBuilder = NotificationCompat.Builder(context,
            NotificationChannelsIds.HighImportanceNotificationChannel)
            .apply {
                setSmallIcon(R.drawable.iconfinder_globe_01_186398)
                setAutoCancel(true)
                if(fileUploadingResultData.succeeded!!){
                    setSmallIcon(R.drawable.iconfinder_check_6586148)
                }
                else{
                    setSmallIcon(R.drawable.icon_error)
                }
                setSound(soundUrl)
            }

            customNotificationBuilderAccordingToFileType(notificationBuilder)

        return notificationBuilder
    }

     fun sendNotification(fileUploadingResultData: FileUploadingResultData){

        this.fileUploadingResultData = fileUploadingResultData

      val notificationId = mobileNotificationsUtilities.newId(NotificationTypes.UploadingFileResult)

        notificationManager.notify(notificationId,buildNotification().build())

    }

}