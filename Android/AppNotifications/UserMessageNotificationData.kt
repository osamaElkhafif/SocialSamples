package com.app.androidsocialclient.AppNotifications


class UserMessageNotificationModel {

     var senderUserName:String? = null
    var senderFullName:String? = null
    var Title:String? = null
    var Message:String? = null
}

object UserMessageNotificationKeys{

    const val senderUserName = "senderUserName"
    const val senderFullName = "senderFullName"
    const val Title = "Title"
    const val Message = "Message"

}

class UserMessageNotificationSender:NotificationData{
    var Title:String? = null
    var Message:String? = null
}


