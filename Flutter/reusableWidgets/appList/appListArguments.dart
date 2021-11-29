import 'dart:async';

import 'package:flutter/cupertino.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:test_flutter/appCoreFunctionalities/apiHttpConnections/AppHttpResponse.dart';
import 'package:test_flutter/appCoreFunctionalities/appStaticData/appInitializedData.dart';
import 'package:test_flutter/pages/homePage/homePage.dart';
import 'package:test_flutter/reusableWidgets/appList/appList.dart';

abstract class AppListArguments<T extends AppListDataItem> {
  abstract Future<AppHttpResponse<T>> Function(int)
      functionToCallForGettingElements;
  abstract Widget Function(T) functionToCallForEachListElement;
  abstract T Function() functiontoGetElementUsedForHint;
  abstract Widget? header;
  Function(List<T> itemsToAddTo)? functionToControlAddingNewElements;
  StreamController<T> elementAddedController = StreamController<T>.broadcast();
  late Stream<T> elementAdded;
  AppListArguments() {
    elementAdded = elementAddedController.stream;
  }
  Widget elmentToShowWhenNoElementFound = Center(
    child: Container(
      margin: EdgeInsets.only(top: 30),
      decoration: BoxDecoration(
          color: AppInitializedData
              .appColors.color3PrimaryAppColor3.backgroundColor,
          borderRadius: BorderRadius.all(Radius.circular(10))),
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: [
            FaIcon(
              FontAwesomeIcons.exclamationTriangle,
              color: AppInitializedData
                  .appColors.color5SecondaryAppColor2.backgroundColor,
            ),
            Text(
              AppInitializedData.appStrings.nothingToShow,
              style: TextStyle(
                  color: AppInitializedData
                      .appColors.color3PrimaryAppColor3.textColor),
            ),
          ],
        ),
      ),
    ),
  );
  Widget loadingFirstDataSetHintWidget =
      LayoutBuilder(builder: (context, constraints) {
    return Container(
      width: constraints.maxWidth,
      height: constraints.maxHeight == double.infinity
          ? MediaQuery.of(context).size.height * .6
          : constraints.maxHeight,
      child: Image.asset(
        "assets/loading2.gif",
        width: 100,
        height: 100,
      ),
    );
  });

  Widget loadingNewElementsHintWidget = Image.asset(
    "assets/loading2.gif",
    width: 50,
    height: 50,
  );
}

class MyAppListArguments extends AppListArguments<MyelementTest> {
  @override
  Widget Function(MyelementTest p1) functionToCallForEachListElement = (p1) {
    return Container(
        height: 150, child: Text("this is element number ${p1.value}"));
  };

  @override
  Future<AppHttpResponse<MyelementTest>> Function(int p1)
      functionToCallForGettingElements = (p1) async {
    var data = <MyelementTest>[
      MyelementTest(0),
      MyelementTest(1),
      MyelementTest(2),
      MyelementTest(3),
      MyelementTest(4),
      MyelementTest(5),
      MyelementTest(6),
      MyelementTest(7),
      MyelementTest(8),
      MyelementTest(9),
      MyelementTest(10),
    ];

    return AppHttpResponse(true, AppResponseDataTypeEnum.List,
        hasMoreData: false, responseDataMultiple: data);
  };

  @override
  MyelementTest Function() functiontoGetElementUsedForHint = () {
    return MyelementTest(1000);
  };

  @override
  Widget? header;

  @override
  Widget loadingFirstDataSetHintWidget = Text("StillLoadingFirstData");

  @override
  Widget loadingNewElementsHintWidget = Text("loading new elements");
}
