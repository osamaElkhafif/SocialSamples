package com.app.androidsocialclient.AppNotifications

import android.content.SharedPreferences
import android.util.Log
import com.app.androidsocialclient.DependencyInjection.NotificationChannelIdsSharedPreferences
import com.app.androidsocialclient.DependencyInjection.TokensSharedPreferences
import com.app.androidsocialclient.RetrofitHttpApi.MobileNotificationApiHttp
import com.app.androidsocialclient.RetrofitHttpApi.UpdateToken
import com.app.androidsocialclient.constants.AppPreferencesKeys
import com.app.androidsocialclient.corefunctionalityTypes.ComponentsActiveStatus
import com.google.firebase.messaging.FirebaseMessagingService
import com.google.firebase.messaging.RemoteMessage
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import javax.inject.Inject

@AndroidEntryPoint
class MobileNotificationReceiverService : FirebaseMessagingService() {


    @Inject
    lateinit var mobileNotificationApiHttp:MobileNotificationApiHttp

    @NotificationChannelIdsSharedPreferences
    @Inject
    lateinit var NotificationChannelsIdssharedPreferences: SharedPreferences

    @TokensSharedPreferences
    @Inject
    lateinit var tokensSharedPreferences:SharedPreferences

    @Inject
    lateinit var componentsActiveStatus:ComponentsActiveStatus

    override fun onNewToken(newToken: String) {
        super.onNewToken(newToken)

        CoroutineScope(Dispatchers.IO).launch {

           val oldToken = tokensSharedPreferences.getString(AppPreferencesKeys.NotificationDeviceToken,null)

            if(oldToken != null){

              val updateToken = UpdateToken().apply {
                  OldToken = oldToken
                  NewToken = newToken
              }

                tokensSharedPreferences.edit().putString(AppPreferencesKeys.NotificationDeviceToken,newToken)
                    .apply()

                mobileNotificationApiHttp.UpdateToken(updateToken)
            }

        }

    }

    override fun onMessageReceived(ReceivedNotification: RemoteMessage) {
        super.onMessageReceived(ReceivedNotification)





       val notificationFactoryTypeMapper = MobileNotificationTypeToFactoryMapper(this,componentsActiveStatus)
        notificationFactoryTypeMapper.mapNotificationTypeToNotificationFactory(ReceivedNotification.data)

        //SendTheNotification(ReceivedNotification)

//        val notificatinId = Random.nextInt()
//        val notification = BuildTheNotification(ReceivedNotification)
//
//        if(notification != null){
//            notificationManager.notify(notificatinId, notification)
//        }

    }

//    @RequiresApi(Build.VERSION_CODES.O)
//    fun CreateNotificationChannel(notificationManager: NotificationManager,mobileNotificationType:String?) {
//
//
//       var notificationChannelId = NotificationChannelsIdssharedPreferences.getString(mobileNotificationType,null)
//
//        if(notificationChannelId == null){
//
//
//            when(mobileNotificationType){
//
//                NotificationTypes.UserMessage -> {
//
//                    val notificationChannel = NotificationChannel(
//                        mobileNotificationType, mobileNotificationType,
//                        NotificationManager.IMPORTANCE_HIGH
//                    ).apply {
//
//                        enableLights(true)
//                        lightColor = Color.rgb(255, 69, 0)
//
//                    }
//
//
//                    notificationManager.createNotificationChannel(notificationChannel)
//
//                    NotificationChannelsIdssharedPreferences.edit().putString(mobileNotificationType,mobileNotificationType)
//                        .apply()
//
//                }
//
//
//            }
//
//
//
//        }
//
//
//    }


//
//    fun SendTheNotification(ReceivedNotification:RemoteMessage){
//
//        return when(ReceivedNotification.data[CommonNotificationsKeys.NotificationType]){
//
//            NotificationTypes.UserMessage -> {
//                val gson = Gson()
//                val userMessageNotificationModel = gson.fromJson(gson.toJson(ReceivedNotification.data),UserMessageNotificationModel::class.java)
//                 val userMessageNotificationFactory =  UserMessageNotificationFactory(this,componentsActiveStatus)
//                 userMessageNotificationFactory.sendNotification(userMessageNotificationModel)
//            }
//            else -> {
//
//            }
//
//        }
//
//    }
//
//
//    fun BuildTheNotification(ReceivedNotification:RemoteMessage): Notification?{
//
//       return when(ReceivedNotification.data[CommonNotificationsKeys.NotificationType]){
//
//            NotificationTypes.UserMessage -> {
//
//                if(!(componentsActiveStatus.ActiveStatusList.
//                    filter { component -> component.ComponentName == AppComponentsNames.MessagesComponent }.first().IsActive)){
//                    val intent = Intent(this, MainActivity::class.java)
//
//                    val pendingIntent = PendingIntent.getActivity(this, 2, intent, PendingIntent.FLAG_ONE_SHOT)
//
//                    NotificationCompat.Builder(this,ReceivedNotification.data[CommonNotificationsKeys.NotificationType]?:"" )
//                        .setContentTitle(ReceivedNotification.data[UserMessageNotificationKeys.Title])
//                        .setContentText(ReceivedNotification.data[UserMessageNotificationKeys.Message])
//                        .setSmallIcon(R.drawable.ic_stat_name)
//                        .setContentIntent(pendingIntent)
//                        .setAutoCancel(true)
//                        .build()
//                }
//                else{
//                    null
//                }
//
//            }
//            else -> {
//                null
//            }
//
//        }
//
//    }


}

