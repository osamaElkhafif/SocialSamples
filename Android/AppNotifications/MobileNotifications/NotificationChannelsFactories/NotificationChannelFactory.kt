package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsFactories

import android.app.NotificationManager
import android.content.Context

abstract class NotificationChannelFactory(val context:Context) {


    val notificationManager:NotificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager

   abstract fun createNotificationChannel()

}