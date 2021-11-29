import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:test_flutter/appCoreFunctionalities/apiHttpConnections/deviceTokensApiHttp.dart';
import 'package:test_flutter/appCoreFunctionalities/appStaticData/appInitializedData.dart';
import 'package:test_flutter/appCoreFunctionalities/appStaticData/appPreferencesKeys.dart';
import 'package:test_flutter/appCoreFunctionalities/authenticationOAuth2/authentication.dart';
import 'package:test_flutter/pages/profilePage/profilePage.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class appDrawer extends StatelessWidget {
  bool isSigningOut = false;

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Container(
            color: AppInitializedData
                .appColors.color1PrimaryAppColor1.backgroundColor,
            child: DrawerHeader(
              child: Image.asset('assets/SocialSlogan.png'),
            ),
          ),
          Expanded(
            child: Container(
              color: AppInitializedData
                  .appColors.color3PrimaryAppColor3.backgroundColor,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  GestureDetector(
                    onTap: () {
                      Navigator.of(context)
                          .popUntil(ModalRoute.withName('/home'));
                    },
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        children: [
                          Container(
                            margin: EdgeInsets.symmetric(horizontal: 5),
                            child: FaIcon(FontAwesomeIcons.home,
                                color: AppInitializedData.appColors
                                    .color3PrimaryAppColor3.textColor),
                          ),
                          Text(
                            AppInitializedData.appStrings.homePageTitle,
                            style: TextStyle(
                                color: AppInitializedData.appColors
                                    .color3PrimaryAppColor3.textColor),
                          ),
                        ],
                      ),
                    ),
                  ),
                  GestureDetector(
                    onTap: () {
                      Navigator.of(context).pushNamed("/Profile",
                          arguments: ProfilePageArguments());
                    },
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        children: [
                          Container(
                            margin: EdgeInsets.symmetric(horizontal: 5),
                            child: FaIcon(FontAwesomeIcons.solidUser,
                                color: AppInitializedData.appColors
                                    .color3PrimaryAppColor3.textColor),
                          ),
                          Text(
                            AppInitializedData.appStrings.profile,
                            style: TextStyle(
                                color: AppInitializedData.appColors
                                    .color3PrimaryAppColor3.textColor),
                          ),
                        ],
                      ),
                    ),
                  ),
                  GestureDetector(
                    onTap: () async {
                      if (isSigningOut) {
                        return;
                      }
                      isSigningOut = true;
                      var sharedPreferences =
                          await SharedPreferences.getInstance();
                      var deviceToken = sharedPreferences
                          .getString(AppPreferencesKeys.deviceToken);
                      if (deviceToken != null) {
                        context
                            .read(deviceTokenApiHttpProvider)
                            .removeToken(deviceToken);
                      }

                      await sharedPreferences.setString(
                          AppPreferencesKeys.deviceToken, '');

                      AuthenticationOAuth2.signOut(context);
                    },
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        children: [
                          Container(
                            margin: EdgeInsets.symmetric(horizontal: 5),
                            child: FaIcon(FontAwesomeIcons.signOutAlt,
                                color: AppInitializedData.appColors
                                    .color3PrimaryAppColor3.textColor),
                          ),
                          Text(AppInitializedData.appStrings.signOut,
                              style: TextStyle(
                                  color: AppInitializedData.appColors
                                      .color3PrimaryAppColor3.textColor)),
                        ],
                      ),
                    ),
                  ),
                  GestureDetector(
                    onTap: () {
                      Navigator.of(context).pushNamed(
                        "/Settings",
                      );
                    },
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        children: [
                          Container(
                            margin: EdgeInsets.symmetric(horizontal: 5),
                            child: FaIcon(FontAwesomeIcons.cog,
                                color: AppInitializedData.appColors
                                    .color3PrimaryAppColor3.textColor),
                          ),
                          Text(AppInitializedData.appStrings.settings,
                              style: TextStyle(
                                  color: AppInitializedData.appColors
                                      .color3PrimaryAppColor3.textColor)),
                        ],
                      ),
                    ),
                  )
                ],
              ),
            ),
          )
        ],
      ),
    );
  }
}


// GestureDetector(
//   onTap: () {
//     Navigator.of(context).pushNamed("/Search",
//         arguments: ProfilePageArguments());
//   },
//   child: Padding(
//     padding: const EdgeInsets.all(8.0),
//     child: Row(
//       children: [
//         Container(
//           margin: EdgeInsets.symmetric(horizontal: 5),
//           child: FaIcon(FontAwesomeIcons.search,
//               color: AppInitializedData.appColors
//                   .color3PrimaryAppColor3.textColor),
//         ),
//         Text(AppInitializedData.appStrings.search,
//             style: TextStyle(
//                 color: AppInitializedData.appColors
//                     .color3PrimaryAppColor3.textColor)),
//       ],
//     ),
//   ),
// ),
// GestureDetector(
//   onTap: () {
//     Navigator.of(context).pushNamed(
//       "/Notifications",
//     );
//   },
//   child: Padding(
//     padding: const EdgeInsets.all(8.0),
//     child: Row(
//       children: [
//         Container(
//           margin: EdgeInsets.symmetric(horizontal: 5),
//           child: FaIcon(FontAwesomeIcons.solidBell,
//               color: AppInitializedData.appColors
//                   .color3PrimaryAppColor3.textColor),
//         ),
//         Text(AppInitializedData.appStrings.notifications,
//             style: TextStyle(
//                 color: AppInitializedData.appColors
//                     .color3PrimaryAppColor3.textColor)),
//       ],
//     ),
//   ),
// )