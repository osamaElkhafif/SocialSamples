package com.app.androidsocialclient.AppNotifications

//this class is a model used to send notifications to server
class AppNotification(    var ReceiverUserName:String,
                       var NotificationData:NotificationData,
                       var NotificationType:String = NotificationTypes.UserMessage
) {
}

// classes inherit from this interface to represent notification data
interface NotificationData{
}


