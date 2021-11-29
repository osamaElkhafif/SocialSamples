package com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsFactories

import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.ContentResolver
import android.content.Context
import android.graphics.Color
import android.media.AudioAttributes
import android.net.Uri
import android.os.Build
import androidx.annotation.RequiresApi
import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationChannelsIds
import com.app.androidsocialclient.R



class HighImportanceNotificationChannel(context:Context) : NotificationChannelFactory(context) {

    var soundUrl:Uri=
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    @RequiresApi(Build.VERSION_CODES.O)
    override fun createNotificationChannel() {


        val notificationChannel = NotificationChannel(
           NotificationChannelsIds.HighImportanceNotificationChannel,NotificationChannelsIds.HighImportanceNotificationChannel,
            NotificationManager.IMPORTANCE_HIGH
        ).apply {

            enableLights(true)
            lightColor = Color.rgb(255, 69, 0)
            setSound(soundUrl,theAudioAttributes)

        }

        notificationManager.createNotificationChannel(notificationChannel)

    }

}


class LowImportanceNotificationChannel(context:Context) : NotificationChannelFactory(context) {

    var soundUrl:Uri=
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    @RequiresApi(Build.VERSION_CODES.O)
    override fun createNotificationChannel() {


        val notificationChannel = NotificationChannel(
            NotificationChannelsIds.LowImportanceNotificationChannel,NotificationChannelsIds.LowImportanceNotificationChannel,
            NotificationManager.IMPORTANCE_LOW
        ).apply {

            enableLights(true)
            lightColor = Color.rgb(255, 69, 0)
            setSound(soundUrl,theAudioAttributes)

        }

        notificationManager.createNotificationChannel(notificationChannel)

    }

}


class DefaultImportanceNotificationChannel(context:Context) : NotificationChannelFactory(context) {

    var soundUrl:Uri=
        Uri.parse(ContentResolver.SCHEME_ANDROID_RESOURCE + "://"+ context.packageName + "/" + R.raw.notificaion)

    var theAudioAttributes = AudioAttributes.Builder()
        .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
        .setUsage(AudioAttributes.USAGE_NOTIFICATION)
        .build()

    @RequiresApi(Build.VERSION_CODES.O)
    override fun createNotificationChannel() {


        val notificationChannel = NotificationChannel(
            NotificationChannelsIds.DefaultImportanceNotificationChannel,NotificationChannelsIds.DefaultImportanceNotificationChannel,
            NotificationManager.IMPORTANCE_DEFAULT
        ).apply {

            enableLights(true)
            lightColor = Color.rgb(255, 69, 0)
            setSound(soundUrl,theAudioAttributes)
        }

        notificationManager.createNotificationChannel(notificationChannel)

    }

}


class MinImportanceNotificationChannel(context:Context) : NotificationChannelFactory(context) {

    @RequiresApi(Build.VERSION_CODES.O)
    override fun createNotificationChannel() {


        val notificationChannel = NotificationChannel(
            NotificationChannelsIds.MinImportanceNotificationChannel,NotificationChannelsIds.MinImportanceNotificationChannel,
            NotificationManager.IMPORTANCE_MIN
        ).apply {

            enableLights(true)
            lightColor = Color.rgb(255, 69, 0)

        }

        notificationManager.createNotificationChannel(notificationChannel)

    }

}