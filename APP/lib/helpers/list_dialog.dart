import 'package:barberiapp/helpers/colores.dart';
import 'package:flutter/material.dart';

void showListDialog({required List<DialogListItem> listItems, required String title, required BuildContext context, required Function(dynamic) callback}) {
  showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(title),
          content: SingleChildScrollView(
            child: ListBody(
              children: createItems(listItems, callback, context),
            ),
          ),
        );
      },
    );
}

List<Widget> createItems(List<DialogListItem> listItems, Function(dynamic) callback, BuildContext context) {
  return <Widget>[
    for (var item in listItems)
      TextButton(
        onPressed: () {
          Navigator.of(context).pop();
          callback(item.value);
        },
        child: Text(item.label, style: TextStyle(fontSize: 15, color: color_textos_alternativo)),
      )
  ];
}
class DialogListItem {
  final String label;
  final dynamic value;
  DialogListItem({required this.label, required this.value});
}