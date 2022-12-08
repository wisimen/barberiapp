import 'dart:convert';

import 'package:barberiapp/helpers/particles_background.dart';
import "package:barberiapp/helpers/colores.dart";
import 'package:flutter/material.dart';
import 'package:email_validator/email_validator.dart';
import 'package:barberiapp/screens/register_user_screen.dart';
import 'package:barberiapp/helpers/generador_campos.dart';
import 'package:barberiapp/helpers/alert_dialog.dart';
import 'package:barberiapp/helpers/constants.dart';
import 'package:barberiapp/helpers/loader_dialog.dart';
import 'package:barberiapp/helpers/security_manager.dart';
import 'package:flutter/scheduler.dart';
import 'package:http/http.dart' as http;

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen>
    with TickerProviderStateMixin {
  String _email = '';
  bool _sessionDataChecked = false;
  String _password = '';
  bool _passwordShow = false;
  bool _remerberme = true;

  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  // on constructor call getToken() and isExpired() to validate token
  // then call super constructor
  _LoginScreenState() {
    var tokenValidation =
        Future.wait([getToken(), isExpired(), getSesionData()]);
    tokenValidation.then((List<Object?> listaRespuestasToken) {
      var token = listaRespuestasToken[0] as String?;
      var isValid = !(listaRespuestasToken[1] as bool);
      if (token != null) {
        if (isValid) {
          Navigator.of(context).pushNamedAndRemoveUntil(
              '/home', (Route<dynamic> route) => false);
        } else {
          deleteToken();
        }
      }
      var sesionData = listaRespuestasToken[2] as SesionData?;
      setState(() {
        if (sesionData?.email != null) {
          _email = sesionData!.email!;
        }

        if (sesionData?.password != null) {
          _password = sesionData!.password!;
        }
        _sessionDataChecked = true;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: color_fondo,
        body: generar_fondo_animado(_showBody(), this));
  }

  Widget _showBody() {
    // Return a future widget to show a loading dialog
    if (_sessionDataChecked) {
      return Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            _showLogo(),
            Form(
              key: _formKey,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  _showEmail(),
                  _showPassword(),
                ],
              ),
            ),
            _showRememberMe(),
            _showButtons(),
          ]);
    }
    return Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: const <Widget>[CircularProgressIndicator()]);
  }

  Widget _showLogo() {
    return const Image(
      image: AssetImage('images/logo_barberiapp_blanco.png'),
      width: 500,
      fit: BoxFit.fill,
    );
  }

  Widget _showEmail() {
    onChanged(value) {
      setState(() {
        _email = value.toString().trim();
      });
    }

    validator(value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado un email';
      }
      if (!EmailValidator.validate(value.toString())) {
        return 'El email ingresado no tiene un formato válido';
      }
      return null;
    }

    return crearCampo(
      "Email",
      hint: "example@mail.com",
      keyboardType: TextInputType.emailAddress,
      icon: Icons.email,
      onChanged: onChanged,
      validator: validator,
      initialValue: _email,
    );
  }

  Widget _showPassword() {
    onChanged(value) {
      setState(() {
        _password = value.toString().trim();
      });
    }

    validator(value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado una contraseña';
      }
      return null;
    }

    return crearCampo(
      "Contraseña",
      keyboardType: TextInputType.visiblePassword,
      icon: Icons.lock,
      onChanged: onChanged,
      validator: validator,
      initialValue: _password,
      obscureText: !_passwordShow,
      obscureTextCallback: (() {
        setState(() {
          _passwordShow = !_passwordShow;
        });
      }),
    );
  }

  Widget _showRememberMe() {
    return Theme(
        data: ThemeData(unselectedWidgetColor: color_labels),
        child: CheckboxListTile(
            checkColor: color_labels,
            activeColor: color_boton_principal,
            title:
                const Text('Recordarme', style: TextStyle(color: color_labels)),
            value: _remerberme,
            onChanged: ((value) {
              setState(() {
                _remerberme = value!;
                deleteSesionData();
              });
            })));
  }

  Widget _showButtons() {
    return Container(
      margin: const EdgeInsets.only(left: 10, right: 10),
      child: Column(
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: <Widget>[
              _showLoginButton(),
              const SizedBox(
                width: 20,
              ),
              _showRegisterButton(),
            ],
          )
        ],
      ),
    );
  }

  Widget _showLoginButton() {
    return Expanded(
      child: ElevatedButton(
        style: ButtonStyle(
          backgroundColor: MaterialStateProperty.resolveWith<Color>(
              (Set<MaterialState> states) {
            return color_boton_principal;
          }),
        ),
        child: const Text('Ingresar'),
        onPressed: () => _login(),
      ),
    );
  }

  Widget _showRegisterButton() {
    return Expanded(
      child: ElevatedButton(
        style: ButtonStyle(
          backgroundColor: MaterialStateProperty.resolveWith<Color>(
              (Set<MaterialState> states) {
            return color_boton_segundario;
          }),
        ),
        child: const Text(
          'Registrarse',
          style: TextStyle(
            color: color_labels_segundario,
          ),
        ),
        onPressed: () => _register(),
      ),
    );
  }

  void _login() async {
    if (_formKey.currentState!.validate()) {
      _formKey.currentState!.save();
      showLoadingDialog(context);
      var request = http.post(loginControllerUri,
          body: json.encode({
            'email': _email,
            'password': _password,
          }),
          headers: <String, String>{
            'Content-Type': 'application/json; charset=UTF-8',
            'accept': 'application/json',
          });
      request.then((http.Response response) {
            if (response.statusCode == 200) {
              var jsonResponse = jsonDecode(response.body);
              saveToken(jsonResponse['token']);
              if (_remerberme) {
                storeSesionData(_email, _password);
              }
              SchedulerBinding.instance.addPostFrameCallback((_) {
                Navigator.of(context).pushNamedAndRemoveUntil(
                    '/home', (Route<dynamic> route) => false);
              });
            } else {
              SchedulerBinding.instance.addPostFrameCallback((_) {
                Navigator.of(context).pop();
                showAlertDialog(
                    type: AlertDialogType.error,
                    content: const Text(
                      'Error al iniciar sesión. Verifique los datos ingresados',
                      textAlign: TextAlign.center,
                    ),
                    title: "No se pudo iniciar sesión",
                    context: context,
                    callback: () {});
              });
            }
          });
    } else {
      showAlertDialog(
          type: AlertDialogType.info,
          context: context,
          title: 'Hay campos incompletos',
          content: const Text(
            'Debe ingresar correctamente la información solicitada',
            textAlign: TextAlign.center,
          ),
          callback: () {},
          defaultActionText: 'OK');
    }
  }

  void _register() {
    Navigator.push(
        context, MaterialPageRoute(builder: (context) => RegisterUserScreen()));
  }
}
