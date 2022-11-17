import 'package:flutter/material.dart';
import 'package:email_validator/email_validator.dart';

class RegisterUserScreen extends StatefulWidget {
  const RegisterUserScreen({super.key});

  @override
  State<RegisterUserScreen> createState() => _RegisterUserScreenState();
}

class _RegisterUserScreenState extends State<RegisterUserScreen> {

  String _email='';
  String _password='';
  String _password2='';
  String _name='';
  String _last_name='';
  String _phone='';
  String _address='';
  String _dni='';


  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return  Scaffold(
      backgroundColor: Color.fromARGB(249, 230, 216, 178),
      appBar: show_app_bar(),
      body: Center(
        child: SingleChildScrollView(
          padding: EdgeInsets.all(32),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget> [
              Form(
                key: _formKey,
                child: ListView(
                  children: <Widget>[
                    _show_tittle(),
                    _show_profile_picture(),
                    _show_name(),
                    _show_lastname(),
                    _show_dni(),
                    _show_phone(),
                    _show_address(),
                    _show_email(),
                    _show_password(),
                    _show_password2(),
                    _show_buttons(),
                  ],
                ),
              ),
            ],
          )
        ),
      )
    );
  }

  AppBar show_app_bar() {
    return AppBar(
      leading: IconButton(
        icon: Icon(Icons.arrow_back, color: Colors.black),
        onPressed: () => Navigator.of(context).pop(),
        ),
      title: Text('Volver', style: TextStyle(color: Colors.black)),
      centerTitle: false,
      backgroundColor: Color.fromARGB(225, 207, 145, 11),
      elevation: 0,
    );
  }

  Widget _show_tittle() {
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: Text(
        'Registro de usuario',
        style: TextStyle(
          fontSize: 30,
          fontWeight: FontWeight.bold,
          color: Colors.black,
        ),
      ),
    );
  }

  Widget _show_email(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        keyboardType: TextInputType.emailAddress,
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Email',
          hintText: 'example@yourmail.com',
          prefixIcon: Icon(Icons.email),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _email = value.trim();
          });
        },
      )
    );
  }

  Widget _show_profile_picture() {
    return Container(
      padding: EdgeInsets.all(20),
      child: Center(
        child: Column(
          children: <Widget>[
            CircleAvatar(
              radius: 50,
              backgroundImage: AssetImage('images/Avatar.svg'),
            ),
            TextButton(
              onPressed: () {
                chooseImage();
              },
              child: Text(
                'Seleccionar imagen',
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 16,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  void chooseImage() {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Seleccionar imagen'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextButton(
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                  child: Text(
                    'Galería',
                    style: TextStyle(
                      color: Colors.black,
                      fontSize: 16,
                    ),
                  ),
                ),
                TextButton(
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                  child: Text(
                    'Cámara',
                    style: TextStyle(
                      color: Colors.black,
                      fontSize: 16,
                    ),
                  ),
                ),
              ],
            ),
          ),
        );
      },
    );
  }

  Widget _show_password(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        obscureText: true,
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Contraseña',
          hintText: '********',
          prefixIcon: Icon(Icons.lock),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _password = value.trim();
          });
        },
      )
    );
  }

  Widget _show_password2(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        obscureText: true,
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Confirmar contraseña',
          hintText: '********',
          prefixIcon: Icon(Icons.lock),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _password2 = value.trim();
          });
        },
      )
    );
  }

  Widget _show_name(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextFormField(
        validator: (value) {
           if (value == null || value.isEmpty) {
             return 'Es obligatorio introducir un nombre';
           }
          return null;
        },
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Nombres',
          hintText: 'Nombres',
          prefixIcon: Icon(Icons.account_circle_outlined),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _name = value.trim();
          });
        },
      )
    );
  }

  Widget _show_lastname(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Apellidos',
          hintText: 'Apellidos',
          prefixIcon: Icon(Icons.account_circle_outlined),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _last_name = value.trim();
          });
        },
      )
    );
  }

  Widget _show_dni(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        keyboardType: TextInputType.number,
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'N° Documento',
          hintText: 'N° Documento',
          prefixIcon: Icon(Icons.qr_code_2),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _dni = value.trim();
          });
        },
      )
    );
  }

  Widget _show_phone(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        keyboardType: TextInputType.phone,
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Teléfono',
          hintText: 'Teléfono',
          prefixIcon: Icon(Icons.phone),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _phone = value.trim();
          });
        },
      )
    );
  }

  Widget _show_address(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: TextField(
        style: TextStyle(color: Colors.black),
        decoration: InputDecoration(
          labelText: 'Dirección',
          hintText: 'Dirección',
          prefixIcon: Icon(Icons.location_on),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fillColor: Colors.white,
          filled: true,
        ),
        onChanged: (value) {
          setState(() {
            _address = value.trim();
          });
        },
      )
    );
  }

  void validate_password(String value) {
    if (value.length < 8) {
      print('La contraseña debe tener al menos 8 caracteres');
    }
  }
  Widget _show_buttons(){
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: <Widget>[
          ElevatedButton.icon(
            label: Text(
              'Registrarse',
              style: TextStyle(
                color: Colors.black,
                fontSize: 16,
              ),
            ),
            icon: Icon(Icons.save, color: Colors.black),
            onPressed: () {
              if (_formKey.currentState!.validate()) {
                _formKey.currentState!.save();
                print('Nombre: $_name');
                print('Apellido: $_last_name');
                print('DNI: $_dni');
                print('Teléfono: $_phone');
                print('Dirección: $_address');
                print('Email: $_email');
                print('Password: $_password');
                print('Password2: $_password2');
                if(_password == _password2){
                  print('Las contraseñas coinciden');
                  _register();
                }else{
                  print('Las contraseñas no coinciden');
                }
              }
            },
          )
        ],
      ),
    );
  }
  void _register(){}
}