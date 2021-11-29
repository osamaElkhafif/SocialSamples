package com.app.androidsocialclient.AppNotifications.MobileNotifications.MobileSpecificModels

import com.app.androidsocialclient.AppNotifications.MobileNotifications.NotificationsFactory.FileType

class FileUploadingResultDataBuilder {

    private var fileUploadingResultData = FileUploadingResultData()

    fun setForVideoUploadingResult(videoTitle:String,succeeded:Boolean):FileUploadingResultData{
        fileUploadingResultData.VideoTitle = videoTitle
        fileUploadingResultData.succeeded = succeeded
        fileUploadingResultData.fileType = FileType.videoFile
        return fileUploadingResultData
    }

}