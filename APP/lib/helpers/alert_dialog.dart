import 'package:barberiapp/helpers/colores.dart';
import 'package:flutter/material.dart';

enum AlertDialogType { error, success, info, warning, question }

void showAlertDialog({
  required AlertDialogType type,
  required Widget content,
  required String title,
  required BuildContext context,
  required Function() callback,
  String? defaultActionText
  }) {
  FocusNode myFocusNode= FocusNode();
  showDialog(
    context: context,
    builder: (BuildContext context) {
      return AlertDialog(
        title: Text(title),
        scrollable: true,
        icon: _showIcon(type),
        content: Column(
            children: <Widget>[
              content,
              _closeButton(context, callback, defaultActionText, myFocusNode)
            ],
          ),
      );
    },
  );
  myFocusNode.requestFocus();
}
Icon _showIcon(AlertDialogType type){
  IconData icon;
  Color color;
  switch (type) {
    case AlertDialogType.error:
      icon = Icons.error;
      color = color_errores;
      break;
    case AlertDialogType.success:
      icon = Icons.check_circle;
      color = color_exitos;
      break;
    case AlertDialogType.info:
      icon = Icons.info;
      color = color_informaciones;
      break;
    case AlertDialogType.warning:
      icon = Icons.warning;
      color = color_advertencias;
      break;
    case AlertDialogType.question:
      icon = Icons.help;
      color = color_preguntas;
      break;
    default:
      icon = Icons.error;
      color = color_errores;
  }
  return Icon(icon, size: 50, color: color);
}
Widget _closeButton(BuildContext context, Function() callback, String? defaultActionText, FocusNode myFocusNode){
  return Padding(
    padding: EdgeInsetsDirectional.only(top: 25),
    child: OutlinedButton(
      focusNode: myFocusNode,
      style: ButtonStyle(
        backgroundColor: MaterialStateProperty.resolveWith<Color>(
          (Set<MaterialState>states){
            return color_boton_segundario;
          }
        ),
      ),
      onPressed: () {
        Navigator.of(context).pop();
        callback();
      },
      child: Text(defaultActionText??'Cerrar', style: TextStyle(fontSize: 15, color: color_textos_alternativo)),
    ),
  );
}