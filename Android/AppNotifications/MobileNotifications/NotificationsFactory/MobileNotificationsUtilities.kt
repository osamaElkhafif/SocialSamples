package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory

import android.app.NotificationManager
import android.service.notification.StatusBarNotification
import com.app.androidsocialclient.AppNotifications.NotificationTypes

object NotificationsIdsRanges {


   var NotificationIdsRanges:MutableList<NotificationIdsRange> = mutableListOf()


    init {

        val userMessageNotificationIdsRange = NotificationIdsRange(NotificationTypes.UserMessage,1,50)
       NotificationIdsRanges.add(userMessageNotificationIdsRange)

        val uploadingFileNotificationIdsRange = NotificationIdsRange(NotificationTypes.UploadingFileProgress,51,100)
        NotificationIdsRanges.add(uploadingFileNotificationIdsRange)

        val uploadingFileResultNotificationIdsRange = NotificationIdsRange(NotificationTypes.UploadingFileResult,101,150)
        NotificationIdsRanges.add(uploadingFileResultNotificationIdsRange)

        val friendRequestNotificationIdsRange = NotificationIdsRange(NotificationTypes.FriendRequest,151,200)
        NotificationIdsRanges.add(friendRequestNotificationIdsRange)

        val friendRequestAcceptedNotificationRange = NotificationIdsRange(NotificationTypes.FriendRequestAccepted,201,250)
        NotificationIdsRanges.add(friendRequestAcceptedNotificationRange)

        val videoProcessingResultRange = NotificationIdsRange(NotificationTypes.VideoProcessingResult,251,300)
        NotificationIdsRanges.add(videoProcessingResultRange)

    }


}

class NotificationIdsRange(val notificationType:String,val min:Int,val max:Int){

}

class MobileNotificationsUtilities(private val notificationManager:NotificationManager){


        fun newId(notificationType: String):Int{

            val notificationIdsRange =
                NotificationsIdsRanges.NotificationIdsRanges.first { it.notificationType == notificationType }

            var currentMaxId:Int = 0

            //Get Current Max active notification Id for the notification type ----
              val statusBarNotifications = getNotificationsOfType(notificationType)
            if(statusBarNotifications.isEmpty()){
                return notificationIdsRange.min
            }
            else{
                currentMaxId = getNotificationsOfType(notificationType).maxOf {
                    it.id
                }
            }

            return if(currentMaxId==notificationIdsRange.max){
                notificationIdsRange.min
            } else {
                currentMaxId + 1
            }

        }

         fun getNotificationsOfType(notificationType: String):List<StatusBarNotification>{

            val notificationIdsRange =
                NotificationsIdsRanges.NotificationIdsRanges.first { it.notificationType == notificationType }

                 return notificationManager.activeNotifications.filter { it ->
                     it.id >= notificationIdsRange.min && it.id <= notificationIdsRange.max
                 }

        }



}