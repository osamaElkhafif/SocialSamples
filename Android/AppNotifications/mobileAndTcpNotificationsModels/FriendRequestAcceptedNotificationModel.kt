package com.app.androidsocialclient.AppNotifications.mobileAndTcpNotificationsModels

import com.app.androidsocialclient.HttpApiModels.ReceivedModels.UserDataModel

class FriendRequestAcceptedNotificationModel {
    var requestAcceptorUserData :UserDataModel?=null
    var dateAccepted :String?= null
}