import 'package:barberiapp/helpers/colores.dart';
import 'package:flutter/material.dart';

Widget defaultLoadingWidget() {
  return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.center,
      children: const <Widget>[CircularProgressIndicator(
        // color property has ARGB cake orange color
        color: Color.fromARGB(255, 255, 153, 0),
        )]);
}
