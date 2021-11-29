import 'dart:async';

import 'package:flutter/cupertino.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:path/path.dart';
import 'package:test_flutter/appCoreFunctionalities/apiHttpConnections/AppHttpResponse.dart';
import 'package:test_flutter/appCoreFunctionalities/appStaticData/appInitializedData.dart';
import 'package:test_flutter/pages/homePage/homePage.dart';
import 'package:test_flutter/reusableWidgets/appList/appListArguments.dart';

class AppList<T extends AppListDataItem, Args extends AppListArguments<T>>
    extends StatefulWidget {
  Args appListArguments;
  bool isFirstPageAlreadyGotten = false;
  List<T>? firstPagePassedElements;
  bool reverseList = false;
  AppList(this.appListArguments,
      {this.isFirstPageAlreadyGotten = false,
      this.firstPagePassedElements,
      this.reverseList = false,
      Key? key})
      : super(key: key);

  StreamController<int> streamController = StreamController<int>.broadcast();
  late Stream<int> stream = streamController.stream;

  @override
  AppListState createState() =>
      AppListState<T>(appListArguments.functionToCallForEachListElement,
          firstPassedElements: firstPagePassedElements,
          funToControlAddingElements:
              appListArguments.functionToControlAddingNewElements);
}

class AppListDataItem {
  @JsonKey(ignore: true)
  bool isLoadingElement;
  @JsonKey(ignore: true)
  bool isHeader;
  AppListDataItem({this.isLoadingElement = false, this.isHeader = false});
}

class AppListState<T extends AppListDataItem> extends State<AppList> {
  bool hasMore = true;
  int elementShownNowIndex = 0;
  int numbers = 0;
  Widget Function(T) funToCallforEachElement;
  Function(List<T>)? funToControlAddingElements;
  int lastPageNumberGot = 0;
  bool isLoadingFirstDataSet = true;
  List<T> elements = <T>[];
  ScrollController scrollController =
      ScrollController(initialScrollOffset: 0, keepScrollOffset: true);
  bool loadingElements = false;
  List<T>? firstPassedElements;

  AppListState(this.funToCallforEachElement,
      {this.firstPassedElements, this.funToControlAddingElements});

  @override
  void initState() {
    super.initState();
    if (!widget.isFirstPageAlreadyGotten) {
      loadNewElements(1);
    } else {
      if (widget.firstPagePassedElements == null) {
        throw Exception("first page data should be passed because " +
            "isFirstPageAlreadyGotten is set to true");
      }
      elements.clear();
      _addHeaderToElments();
      elements.addAll(firstPassedElements!);
      _addLoadingToElements();
      lastPageNumberGot = 1;
      isLoadingFirstDataSet = false;
      setState(() {});
    }

    if (this.funToControlAddingElements != null) {
      this.funToControlAddingElements!(this.elements);
    }

    widget.appListArguments.elementAdded.listen((item) {
      addElementAtStart(item as T);
      setState(() {});
    });

    widget.stream.listen((pageNumber) {
      loadNewElements(pageNumber);
    });

    scrollController.addListener(() {
      if (elements.length - elementShownNowIndex < 4 &&
          !loadingElements &&
          hasMore) {
        loadingElements = true;
        loadNewElements(lastPageNumberGot + 1);
      }
    });
  }

  addElement(T element) {
    elements.insert(elements.length - 1, element);
    setState(() {});
  }

  addElementAtStart(T? element) {
    if (element == null) {
      return;
    }
    if (widget.appListArguments.header != null) {
      elements.insert(1, element);
    } else {
      elements.insert(0, element);
    }
    scrollController.jumpTo(0);
    setState(() {});
  }

  addElementsAtStart(List<T> newelements) {
    if (widget.appListArguments.header != null) {
      elements.insertAll(1, newelements);
    } else {
      elements.insertAll(0, newelements);
    }
    scrollController.jumpTo(0);
    setState(() {});
  }

  addElements(List<T> newElements) {
    elements.insertAll(elements.length - 1, newElements);
    setState(() {});
  }

  startGettingFirstPageForNewDifferentData() {
    loadNewElements(1);
  }

  loadNewElements(int pageNumber) {
    try {
      widget.appListArguments
          .functionToCallForGettingElements(pageNumber)
          .then((value) {
        lastPageNumberGot = pageNumber;

        if (value.hasMoreData != null) {
          hasMore = value.hasMoreData!;
        } else {
          hasMore = false;
        }
        if (value.responseDataMultiple != null) {
          if (pageNumber == 1) {
            elements.clear();
            _addHeaderToElments();
            elements.addAll(value.responseDataMultiple as List<T>);
            _addLoadingToElements();
          } else {
            elements.insertAll(
                elements.length - 1, value.responseDataMultiple as List<T>);
          }
          loadingElements = false;
          setState(() {
            isLoadingFirstDataSet = false;
          });
        }
      });
    } catch (ex) {}
  }

  _addHeaderToElments() {
    if (widget.appListArguments.header != null) {
      elements.add((widget.appListArguments.functiontoGetElementUsedForHint()
        ..isHeader = true) as T);
    }
  }

  _addLoadingToElements() {
    elements.add((widget.appListArguments.functiontoGetElementUsedForHint()
      ..isLoadingElement = true) as T);
  }

  @override
  Widget build(BuildContext context) {
    return isLoadingFirstDataSet
        ? widget.appListArguments.loadingFirstDataSetHintWidget
        : Container(
            child: ListView.builder(
                reverse: widget.reverseList,
                itemCount: elements.length,
                controller: scrollController,
                itemBuilder: (context, index) {
                  elementShownNowIndex = index;
                  if ((elements[index].isHeader) == true &&
                      widget.appListArguments.header != null) {
                    return widget.appListArguments.header!;
                  } else if ((elements[0].isHeader && elements.length == 2) ||
                      elements[0].isLoadingElement) {
                    return widget
                        .appListArguments.elmentToShowWhenNoElementFound;
                  }
                  if ((elements[index]).isLoadingElement == true) {
                    return Visibility(
                        visible: loadingElements,
                        child: widget
                            .appListArguments.loadingNewElementsHintWidget);
                  }
                  return funToCallforEachElement(elements[index]);
                }),
          );
  }
}
