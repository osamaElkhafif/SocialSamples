package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsFactories

import android.content.Context

class NotificationsChannelsCreator(val context:Context) {

    var notificationsChannelsFactories:MutableList<NotificationChannelFactory> = mutableListOf()

    init {

        notificationsChannelsFactories.add(HighImportanceNotificationChannel(context))
        notificationsChannelsFactories.add(LowImportanceNotificationChannel(context))
        notificationsChannelsFactories.add(MinImportanceNotificationChannel(context))
        notificationsChannelsFactories.add(DefaultImportanceNotificationChannel(context))
    }

}