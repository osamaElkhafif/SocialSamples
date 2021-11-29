package com.app.androidsocialclient.AppNotifications.MobileNotifications.MobileSpecificModels

import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.FileType

class FileUploadingProgressNotificationDataBuilder {

    private var fileUploadingProgressData:FileUploadingProgressData = FileUploadingProgressData()

    fun setForVideoUploadingData(videoTitle:String,
                                 progressPercentage:Int,
                                 workerId:String):FileUploadingProgressData{

        fileUploadingProgressData.videoTitle = videoTitle
        fileUploadingProgressData.fileProgressPercentage = progressPercentage
        fileUploadingProgressData.fileType = FileType.videoFile
        fileUploadingProgressData.workerId = workerId

        return fileUploadingProgressData
    }

}