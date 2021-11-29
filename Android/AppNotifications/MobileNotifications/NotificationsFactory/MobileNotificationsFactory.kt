package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory

import android.app.NotificationManager
import android.content.Context

abstract class MobileNotificationFactory(val context: Context) {

   val notificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager

}