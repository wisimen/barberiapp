
import 'package:barberiapp/helpers/colores.dart';
import 'package:flutter/material.dart';
import 'package:barberiapp/helpers/default_loading_widget.dart';

showLoadingDialog(BuildContext context,{String? message}) {
  showGeneralDialog(
    context: context,
    barrierColor: color_fondo_campos.withAlpha(200),
    pageBuilder:(BuildContext buildContext, Animation<double> animation, Animation<double> secondaryAnimation) {
      return Center(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            defaultLoadingWidget(),
            Padding(
              padding: const EdgeInsets.only(top: 20.0),
              child: Text(message ?? 'Espere por favor',
                style: const TextStyle(
                  color: color_fondo,
                  fontSize: 20,
                  decoration: TextDecoration.none,
                ),
              ),
            )
          ],
        ),
      );
    }
  );
}