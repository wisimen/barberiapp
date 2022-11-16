// ignore_for_file: unused_field, prefer_final_fields, prefer_const_constructors, sort_child_properties_last

import 'package:flutter/material.dart';
import 'package:email_validator/email_validator.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {

  String _email='';
  String _emailError='';
  bool _emailShowError=false;

  String _password='';
  String _passwordError='';
  bool _passwordShowError=false;

  bool _passwordShow=false;
  bool _remerberme=true;
  


  @override
  Widget build(BuildContext context) {
    return Scaffold(

    backgroundColor: Color.fromARGB(249, 230, 216, 178),
    body: Center(child: Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget> [
        _showlogo(),
        _showemail(),
        _showpassword(),
        _showRememberme(),
        _showButtons(),
      ],
    )),

  );
    
  }
  
  Widget _showlogo() {
    return Image(
      image: AssetImage('images/barber.jpg'),

    width: 500,
    fit: BoxFit.fill,
    );
  }
  
  Widget _showemail() {
    return Container(
      padding: EdgeInsets.all(20),
      child: TextField(
        keyboardType: TextInputType.emailAddress,
          decoration: InputDecoration(
            fillColor: Colors.white,
            filled: true,
            hintText: 'Digite su email',
            labelText: 'Email',
            errorText: _emailShowError ? _emailError: null,
            prefixIcon: Icon(Icons.alternate_email),
            suffixIcon: Icon(Icons.email),
            border: OutlineInputBorder(
              borderRadius: BorderRadius.circular(10)))),
         
    );
  
  }
  
  Widget _showpassword() {
    return Container(
      padding: EdgeInsets.all(20),
      child: TextField(
        obscureText: _passwordShow,
          decoration: InputDecoration(
            fillColor: Colors.white,
            filled: true,
            hintText: 'Digite su password',
            labelText: 'Password',
            errorText: _passwordShowError ? _passwordError: null,
            prefixIcon: Icon(Icons.lock),
            suffixIcon: IconButton(
              icon: _passwordShow 
              ? Icon(Icons.visibility)
              : Icon(Icons.visibility_off),
              onPressed: () {
                setState(() {
                  _passwordShow = !_passwordShow;
                });
              },
            ),
            border: OutlineInputBorder(
              borderRadius: BorderRadius.circular(10)
              ),
              ),

              onChanged: (value) {
                _password = value;                
              },
      ),
    );
    
  }
  
   Widget _showRememberme() {
    return CheckboxListTile(
      
      title: Text('Remember me'),
      value: _remerberme, 
      onChanged: ((value) {
        setState(() {
          _remerberme=value!;
        });
      }));
   }
   
     Widget _showButtons() {
      return Container(
        margin: EdgeInsets.only(left: 10, right: 10),
        child: Column(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: <Widget>[
                _showLoginButton(),
                SizedBox(width: 20,),
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
          child: Text('Ingresar'),
          style: ButtonStyle(
            backgroundColor: MaterialStateProperty.resolveWith<Color>(
              (Set<MaterialState>states){
                return Color.fromARGB(238, 228, 189, 84);
              }
            ),
          ),
        onPressed: () => _login(),
        ),
      );
     }
     
      Widget _showRegisterButton() {

         return Expanded(
        child: ElevatedButton(
          child: Text('Registrarse'),
          style: ButtonStyle(
            backgroundColor: MaterialStateProperty.resolveWith<Color>(
              (Set<MaterialState>states){
                return Color.fromARGB(225, 207, 145, 11);
              }
            ),
          ),
        onPressed: () => _login(),
        ),
      );
     }
     
       void _login() async {
        setState(() {

          _passwordShow = false;
           if(!_validateFields()) {
          return;
        }
        });

       
       }
       
         bool _validateFields() {
          bool isValid=true;

          if(_email.isEmpty){
            isValid=false;
            _emailShowError=true;
            _emailError='El email es obligatorio';
          
          } 
          else if(!EmailValidator.validate(_email)) {
            isValid=false;

            _emailShowError=true;
            _emailError='email invalido';

          }
          else {
            _emailShowError=false;
          }


          //

          if(_password.isEmpty){
            isValid=false;
            _passwordShowError=true;
            _passwordError='Debes ingresar un password';
          
          } 
          else if(_password.length <6) {
            isValid=false;

            _passwordShowError=true;
            _passwordError='Su password debe contener al menos 6 caracteres';

          }
          else {
            _passwordShowError=false;
          }


          setState(() {
          });

          return isValid;

         }
       
        /* void _register() {
          Navigator.push(
            context, MaterialPageRoute(builder: (context) => RegisterUserScreen()));
          
        } */

      
}