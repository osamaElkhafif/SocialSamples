import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:test_flutter/appCoreFunctionalities/ChangeHttpsToHttp.dart';
import 'package:test_flutter/appCoreFunctionalities/appStaticData/appInitializedData.dart';
import 'package:test_flutter/appCoreFunctionalities/httpApiModels/sendingModels/imageTypeEnum.dart';

class UserProfilePictureData {
  num? imageWidth;
  num? imageHeight;
  int iamgeSizeToShow;
  ImageTypeEnum? imageType;
  num? topOrLeftPositionPercent;
  String? imageUrl;
  UserProfilePictureData(this.iamgeSizeToShow,
      {this.imageUrl,
      this.imageType,
      this.topOrLeftPositionPercent,
      this.imageHeight,
      this.imageWidth});
}

Widget getUserPicture(UserProfilePictureData userProfilePictureData) {
  double valueToMovePictureLeft = 0;
  double valueToMovePictureTop = 0;
  double heightToUse = userProfilePictureData.iamgeSizeToShow.toDouble();
  double widthToUse = userProfilePictureData.iamgeSizeToShow.toDouble();

  if (userProfilePictureData.imageUrl != null) {
    if (userProfilePictureData.imageUrl != null &&
        (userProfilePictureData.imageWidth == null ||
            userProfilePictureData.imageHeight == null)) {
      throw Exception("both width and height should not be null");
    }

    if (userProfilePictureData.imageUrl != null) {
      if (userProfilePictureData.imageType == ImageTypeEnum.Landscape) {
        heightToUse = userProfilePictureData.iamgeSizeToShow.toDouble();
        widthToUse = (userProfilePictureData.imageWidth! /
                userProfilePictureData.imageHeight!) *
            heightToUse;
      } else {
        widthToUse = userProfilePictureData.iamgeSizeToShow.toDouble();
        heightToUse = (userProfilePictureData.imageHeight! /
                userProfilePictureData.imageWidth!) *
            widthToUse;
      }
    }

    if (userProfilePictureData.imageType == ImageTypeEnum.Portrait) {
      if (userProfilePictureData.topOrLeftPositionPercent != null) {
        valueToMovePictureTop = 1 *
            (userProfilePictureData.topOrLeftPositionPercent!.toDouble() /
                100 *
                heightToUse);
      }
    }

    if (userProfilePictureData.imageType == ImageTypeEnum.Landscape) {
      if (userProfilePictureData.topOrLeftPositionPercent != null) {
        valueToMovePictureLeft = 1 *
            (userProfilePictureData.topOrLeftPositionPercent!.toDouble() /
                100 *
                widthToUse);
      }
    }
  }

  Widget imageToUse = userProfilePictureData.imageUrl == null
      ? Transform.translate(
          offset: Offset(widthToUse * .05, widthToUse * .1),
          child: FaIcon(
            FontAwesomeIcons.solidUser,
            color: AppInitializedData
                .appColors.color1PrimaryAppColor1.backgroundColor,
            size: heightToUse,
          ),
        )
      : CachedNetworkImage(
          imageUrl: ChangeHttpsToHttp.change(userProfilePictureData.imageUrl!),
          alignment: Alignment.topLeft,
          fit: BoxFit.fill,
          placeholder: (cont, string) => CircularProgressIndicator(),
          width: widthToUse,
          height: heightToUse,
        );
  print(valueToMovePictureLeft);
  return ClipOval(
    clipper:
        CircularClipper(radius: userProfilePictureData.iamgeSizeToShow / 2),
    child: Container(
      height: userProfilePictureData.iamgeSizeToShow.toDouble(),
      width: userProfilePictureData.iamgeSizeToShow.toDouble(),
      child: Stack(
        clipBehavior: Clip.hardEdge,
        children: [
          Positioned(
            left: 0,
            top: 0,
            child: Container(
              height: userProfilePictureData.iamgeSizeToShow.toDouble(),
              width: userProfilePictureData.iamgeSizeToShow.toDouble(),
              color: AppInitializedData
                  .appColors.color3PrimaryAppColor3.backgroundColor,
            ),
          ),
          Positioned(
            left: valueToMovePictureLeft,
            top: valueToMovePictureTop,
            child: imageToUse,
          ),
        ],
      ),
    ),
  );
}

class CircularClipper extends CustomClipper<Rect> {
  double radius;

  CircularClipper({this.radius = 50});

  @override
  Rect getClip(Size size) {
    return Rect.fromCircle(
        center: Offset(size.width / 2, size.height / 2), radius: radius);
  }

  @override
  bool shouldReclip(covariant CustomClipper<Rect> oldClipper) {
    return false;
  }
}
