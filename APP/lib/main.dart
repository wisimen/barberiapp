import 'package:barberiapp/screens/home_screen.dart';
import 'package:barberiapp/screens/login_screen.dart';
import 'package:barberiapp/screens/register_user_screen.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Barberiapp',
      home: const LoginScreen(),
      routes: {
        //'/': (context) => const HomeScreen(),
        '/login': (context) => const LoginScreen(),
        '/register': (context) => const RegisterUserScreen(),
        '/home': (context) => const HomeScreen(),
      },
    );
  }
}

