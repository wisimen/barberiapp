
import 'package:flutter/material.dart';
import "package:barberiapp/helpers/colores.dart";


Widget crearCampo(
  String label,
  {
    String? hint,
    IconData? icon,
    void Function(String)? onChanged,
    String? Function(String?)? validator,
    bool? obscureText,
    void Function()? obscureTextCallback,
    TextInputType? keyboardType,
    String? initialValue
  }) {
  return Container(
    padding: EdgeInsets.symmetric(horizontal: 0, vertical: 10),
    child: TextFormField(
      keyboardType: keyboardType,
      initialValue: initialValue,
      style: TextStyle(
        color: color_labels
      ),
      obscureText: obscureText ?? false,
      decoration: InputDecoration(
        labelText: label,
        hintText: hint,
        prefixIcon: (
          icon != null
            ? Icon(icon, color: color_iconos,)
            : null
          ),
        fillColor: color_fondo_campos,
        filled: true,
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10.0),
          borderSide: BorderSide(
            color: color_borde_campos,
            width: 2.0,
          ),
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10.0),
          borderSide: BorderSide(
            color: color_borde_campos,
            width: 1.0,
          ),
        ),
        errorBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10.0),
          borderSide: BorderSide(
            color: color_errores,
            width: 2.0,
          ),
        ),
        prefixIconColor: color_iconos,
        labelStyle: TextStyle(
          color: color_labels,
        ),
        errorStyle: TextStyle(
          color: color_errores,
        ),
        suffixIcon: (
          (obscureText!=null)
          ? IconButton(
            icon: Icon(
              (obscureText
                ? Icons.visibility
                : Icons.visibility_off
              ),
              color: color_iconos
            ),
            onPressed: obscureTextCallback
            )
          : null
        ),
      ),
      onChanged: onChanged,
      validator: validator,
    ),
  );
}