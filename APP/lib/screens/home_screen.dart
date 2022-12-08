import 'dart:ffi';

import 'package:barberiapp/helpers/colores.dart';
import 'package:flutter/material.dart';
import "package:barberiapp/screens/sub_screens/barbershops_selector.dart";
import "package:barberiapp/helpers/particles_background.dart";
import "package:barberiapp/helpers/loader_dialog.dart";
import "package:barberiapp/helpers/security_manager.dart";
import "package:barberiapp/helpers/constants.dart";
import 'package:flutter/scheduler.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:flutter_svg/flutter_svg.dart';

import '../helpers/default_loading_widget.dart';

// build class HomeScreen with basic bottom navigation bar
class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});
  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> with TickerProviderStateMixin {
  int _selectedIndex = 0;
  String? _token;
  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
      obtenerFuentes(indices: [index]);
    });
  }

  Widget _listaBarberia = defaultLoadingWidget();
  // constructor
  _HomeScreenState() {
    obtenerFuentes(indices: [0,1,2]);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: generar_fondo_animado(
          Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              generarContenido(
                context,
                _selectedIndex,
              )
            ],
          ),
          this),
      bottomNavigationBar: BottomNavigationBar(
        iconSize: defaultIconSize,
        backgroundColor: color_bottom_navigation_bar,
        // label color change when selected
        unselectedItemColor: color_item_normal,
        selectedItemColor: color_item_activo,
        items: <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            // draw icon: SvgPicture.asset('images/pole.svg', fit: BoxFit.cover,), but with size 15
            icon: SvgPicture.asset('images/tijeras.svg',
                width: defaultIconSize,
                height: defaultIconSize,
                fit: BoxFit.cover),
            label: 'Barberías',
          ),
          BottomNavigationBarItem(
            icon: SvgPicture.asset('images/cal1.svg',
                width: defaultIconSize,
                height: defaultIconSize,
                fit: BoxFit.cover),
            label: 'Citas',
          ),
          BottomNavigationBarItem(
            icon: SvgPicture.asset('images/profile.svg',
                width: defaultIconSize,
                height: defaultIconSize,
                fit: BoxFit.cover),
            label: 'Perfil',
          ),
        ],
        currentIndex: _selectedIndex,
        onTap: _onItemTapped,
      ),
    );
  }

  Widget generarContenido(BuildContext context, int index) {
    if (_token == null) {
      return defaultLoadingWidget();
    }
    switch (index) {
      case 0:
        return _listaBarberia;
      case 1:
        return const Text('Index 1: Business');
      case 2:
        return const Text(
          'Index 2: School',
        );
      default:
        return const Text(
          'empty',
        );
    }
  }

  void obtenerFuentes({List<int>? indices}) {
    var tokenValidation =
        Future.wait([getToken(), isExpired()]).then((listaRespuestasToken) {
      setState(() {
        _token = (listaRespuestasToken[0] as String?);
        var isExpired = (listaRespuestasToken[1] as bool);

        if (isExpired) {
          deleteToken();
          SchedulerBinding.instance.addPostFrameCallback((_) {
            Navigator.of(context).pushNamedAndRemoveUntil(
                '/login', (Route<dynamic> route) => false);
          });
        }
        // search in indices if 0 is present
        if (indices == null || indices.contains(0)) {
          getBarbershopsList(_token!, (Widget value) {
            setState(() {
              _listaBarberia = value;
            });
          });
        }
      });
    });
  }
}
