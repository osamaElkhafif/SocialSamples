import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:test_flutter/appCoreFunctionalities/AppCustomWidgets/localizeEdgeInsets.dart';
import 'package:test_flutter/appCoreFunctionalities/appStaticData/appInitializedData.dart';
import 'package:test_flutter/pages/profilePage/profilePageOrchestrator.dart';
import 'package:test_flutter/reusableWidgets/userProfilePicture.dart';

var optionsMenuDataProvider =
    ChangeNotifierProvider.family<OpitonsMenuWidgetChanfeNotifier, String>(
        (ref, id) {
  return OpitonsMenuWidgetChanfeNotifier();
});

class OpitonsMenuWidgetChanfeNotifier with ChangeNotifier {
  bool showMenu = false;

  OptionsItemData? optionsItemDataChosen;

  showMenuNow() {
    showMenu = true;
    notifyListeners();
  }

  hideMenu() {
    showMenu = false;
    notifyListeners();
  }

  changeOptionsItemDataChosen(OptionsItemData optionsItemData) {
    this.optionsItemDataChosen = optionsItemData;
    notifyListeners();
  }
}

class OptionsItemData {
  IconData iconData;
  String optionText;
  String? optionValue;
  Function()? functionToCallOnClick;
  OptionsItemData(this.iconData, this.optionText,
      {this.functionToCallOnClick, this.optionValue});
}

class OptionsMenu extends StatefulWidget {
  final List<OptionsItemData> optionsItemsData;
  String optionsMenuId;
  OptionsMenu(this.optionsItemsData, this.optionsMenuId);

  @override
  _OptionsMenuState createState() => _OptionsMenuState();
}

class _OptionsMenuState extends State<OptionsMenu> {
  late StreamSubscription subscription;

  @override
  void initState() {
    super.initState();
  }

  @override
  void dispose() {
    super.dispose();

    subscription.cancel();
  }

  @override
  Widget build(BuildContext context) {
    var screenWidth = MediaQuery.of(context).size.width;
    var screenHeight = MediaQuery.of(context).size.height;
    return Consumer(
      builder: (context, wat, child) {
        wat(optionsMenuDataProvider(widget.optionsMenuId));

        var optionsMenuData =
            context.read(optionsMenuDataProvider(widget.optionsMenuId));

        return Visibility(
          visible: optionsMenuData.showMenu,
          child: ConstrainedBox(
            constraints:
                BoxConstraints.tight(Size(screenWidth, screenHeight * .4)),
            child: Container(
              decoration: BoxDecoration(
                  color: AppInitializedData
                      .appColors.color4SecondaryAppColor1.backgroundColor,
                  borderRadius: BorderRadius.only(
                      topLeft: Radius.circular(10),
                      topRight: Radius.circular(10))),
              child: ListView.builder(
                itemBuilder: (context, index) {
                  return GestureDetector(
                    onTap: () {
                      var funTocall =
                          widget.optionsItemsData[index].functionToCallOnClick;
                      if (funTocall != null) {
                        funTocall();
                      }
                      optionsMenuData.hideMenu();

                      optionsMenuData.changeOptionsItemDataChosen(
                          widget.optionsItemsData[index]);
                    },
                    child: Padding(
                      padding: const EdgeInsets.all(4.0),
                      child: Row(
                        children: [
                          ClipOval(
                            clipper: CircularClipper(radius: 15),
                            clipBehavior: Clip.antiAlias,
                            child: Container(
                              color: AppInitializedData.appColors
                                  .color3PrimaryAppColor3.backgroundColor,
                              child: Padding(
                                padding: const EdgeInsets.all(10.0),
                                child: FaIcon(
                                  widget.optionsItemsData[index].iconData,
                                  size: 15,
                                  color: AppInitializedData.appColors
                                      .color3PrimaryAppColor3.textColor,
                                ),
                              ),
                            ),
                          ),
                          Padding(
                            padding: AppLocalizations.localizeEdgeInsets(
                                EdgeInsets.only(left: 5),
                                AppInitializedData.appStrings),
                            child: Text(
                              widget.optionsItemsData[index].optionText,
                              style: TextStyle(
                                  color: AppInitializedData.appColors
                                      .color4SecondaryAppColor1.textColor),
                            ),
                          )
                        ],
                      ),
                    ),
                  );
                },
                itemCount: widget.optionsItemsData.length,
              ),
            ),
          ),
        );
      },
    );
  }
}
