import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';
import 'dart:ui';
import 'package:barberiapp/helpers/constants.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:email_validator/email_validator.dart';
import 'package:flutter_svg_provider/flutter_svg_provider.dart';
import "package:barberiapp/helpers/colores.dart";
import "package:barberiapp/helpers/generador_campos.dart";
import "package:barberiapp/helpers/particles_background.dart";
import 'package:image_picker/image_picker.dart';
import "package:barberiapp/models/tipo_documento.dart";
import "package:barberiapp/models/cliente_creacion.dart";
import "package:barberiapp/helpers/list_dialog.dart";
import "package:barberiapp/helpers/alert_dialog.dart";
import "package:barberiapp/helpers/loader_dialog.dart";
import 'package:http/http.dart' as http;

class RegisterUserScreen extends StatefulWidget {
  const RegisterUserScreen({
    super.key
  });


  @override
  State<RegisterUserScreen> createState() => _RegisterUserScreenState();
}

class _RegisterUserScreenState extends State<RegisterUserScreen>with TickerProviderStateMixin {

  String _email='';
  String _password='';
  String _password2='';
  String _name='';
  String _lastName='';
  String _phone='';
  String _address='';
  String _dni='';
  TipoDocumento? _dniType;
  bool _passwordShow=false;
  bool _password2Show=false;
  PlatformFile? _avatarFile;
  String _avatarPath = "images/Avatar.svg";
  Widget? dniTipeWidget;

  final ImagePicker _picker = ImagePicker();
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return  Scaffold(
      backgroundColor: color_fondo,
      appBar: _showAppBar(),
      body: generar_fondo_animado(_showFormulario(), this),
    );
  }
  Widget _showFormulario(){
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget> [
        _showTittle(),
        _showProfilePicture(),
        Form(
          key: _formKey,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              _showName(),
              _showLastname(),
              _showDNI(),
              _showEmail(),
              _showPassword(),
              _showPassword2(),
              _showPhone(),
              _showAddress(),
              _showDNIType(),
            ]
          ),
        ),
        _showButtons(),
      ],
    );
  }

  AppBar _showAppBar() {
    return AppBar(
      leading: IconButton(
        icon: const Icon(Icons.arrow_back, color: color_labels),
        onPressed: () => Navigator.of(context).pop(),
        ),
      title: const Text('Volver', style: TextStyle(color: color_labels)),
      centerTitle: false,
      backgroundColor: Colors.transparent,
      elevation: 2,
    );
  }

  Widget _showTittle() {
    return const Padding(
      padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child:  Text(
        'Registro de usuario',
        style:  TextStyle(
          fontSize: 30,
          fontWeight: FontWeight.bold,
          color: color_textos,
        ),
      ),
    );
  }

  Widget _showName(){
    onChanged (value) {
      setState(() {
        _name =  value.toString().toUpperCase().trim();
        }
      );
    }

    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado un nombre';
      }
      return null;
    }

    return crearCampo(
      'Nombres',
      icon: Icons.keyboard_arrow_right,
      onChanged: onChanged,
      validator: validator
    );
  }

  Widget _showLastname(){
    onChanged (value) {
      setState(() {
        _lastName =  value.toString().toUpperCase().trim();
        });
    }

    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado un apellido';
      }
      return null;
    }

    return crearCampo(
      'Apellidos',
      icon: Icons.keyboard_double_arrow_right,
      onChanged: onChanged,
      validator: validator
    );
  }

  Widget _showDNI(){
    onChanged (value) {
      setState(() {
        _dni =  value.toString().trim();
        });
    }

    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado un número de identificación';
      }
      return null;
    }

    return crearCampo(
      'Documento de identidad',
      icon: Icons.qr_code_2,
      keyboardType: TextInputType.number,
      onChanged: onChanged,
      validator: validator
    );
  }

  Widget _showEmail(){
    onChanged (value) {
      setState(() {
        _email =  value.toString().toLowerCase().trim();
        }
      );
    }

    validator (value) {
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
      validator: validator
    );
  }

  Widget _showPassword() {
       onChanged (value) {
      setState(() {
        _password =  value.toString().trim();
        }
      );
    }

    validator (value) {
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
      obscureText: !_passwordShow,
      obscureTextCallback: (() {
        setState(() {
          _passwordShow = !_passwordShow;
        });
      }),
    );
  }

  Widget _showPassword2(){
       onChanged (value) {
      setState(() {
        _password2 =  value.toString().trim();
        }
      );
    }

    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado una contraseña';
      }
      if (value != _password) {
        return 'Las contraseñas no coinciden';
      }
      return null;
    }

    return crearCampo(
      "Confirmar contraseña",
      keyboardType: TextInputType.visiblePassword,
      icon: Icons.lock,
      onChanged: onChanged,
      validator: validator,
      obscureText: !_password2Show,
      obscureTextCallback: (() {
        setState(() {
          _password2Show = !_password2Show;
        });
      }),
    );
  }

  Widget _showProfilePicture() {
    return Container(
      decoration: const BoxDecoration(color: color_fondo_campos),
      child: InkWell(
        onTap: () {
          chooseImage();
        },
        child: Center(
          child: Column(
            children: <Widget>
            [
              Center(
              child: ClipRect(  // <-- clips to the 200x200 [Container] below
                child: BackdropFilter(
                  filter: ImageFilter.blur(
                    sigmaX: 4.0,
                    sigmaY: 4.0,
                  ),
                  child: AnimatedContainer(
                    alignment: Alignment.center,
                    duration: const Duration(seconds: 1),
                    curve: Curves.fastOutSlowIn,
                    child: Padding(
                        padding: EdgeInsets.symmetric(horizontal: 0, vertical: 20),
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: <Widget>[
                            CircleAvatar(
                              radius: 50,
                              backgroundImage: (_avatarFile == null
                                ? Image(image:Svg(_avatarPath)).image
                                : Image.file(File(_avatarFile?.path??_avatarPath)).image),
                              ),
                            const Padding(
                              padding: EdgeInsets.only(top: 20),
                              child: Text(
                                'Seleccionar imagen',
                                style: TextStyle(
                                color: color_labels,
                                fontSize: 16,
                                fontWeight: FontWeight.w400,
                                ),
                              ),
                            ),
                          ],
                        )
                      ),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  void chooseImage() {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Seleccionar imagen'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextButton(
                  onPressed: () async {
                    Navigator.of(context).pop();
                    FilePickerResult? result = await FilePicker.platform.pickFiles();
                    if (result != null) {
                      PlatformFile file = result.files.first;
                      _avatarFile = file;
                      setState(() {
                        _avatarPath = file.path ?? _avatarPath;
                      });
                    }
                    },
                  child: const Text(
                    'Galería',
                    style: TextStyle(
                      color: color_textos_alternativo,
                      fontSize: 16,
                    ),
                  )
                ),
                TextButton(
                  onPressed: () async  {
                    final XFile? photo = await _picker.pickImage(source: ImageSource.camera);
                    _avatarFile = PlatformFile(
                      path: photo!.path,
                      name: photo.name,
                      bytes: await photo.readAsBytes(),
                      size: await photo.length()
                    );
                    setState(() {
                      Navigator.of(context).pop();
                      _avatarPath = photo.path;
                    });
                  },
                  child: const Text(
                    'Cámara',
                    style: TextStyle(
                      color: color_textos_alternativo,
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

  Widget _showPhone(){
    onChanged (value) {
      setState(() {
        _phone =  value.toString().trim();
        }
      );
    }

    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado un número de teléfono';
      }
      return null;
    }

    return crearCampo(
      'Teléfono',
      icon: Icons.phone,
      keyboardType: TextInputType.phone,
      onChanged: onChanged,
      validator: validator
    );
  }

  Widget _showAddress(){
    onChanged (value) {
      setState(() {
        _address =  value.toString().trim();
        }
      );
    }

    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha ingresado una dirección';
      }
      return null;
    }

    return crearCampo(
      'Dirección',
      icon: Icons.location_on,
      keyboardType: TextInputType.streetAddress,
      onChanged: onChanged,
      validator: validator,
    );
  }

  Widget _showDNIType(){
    validator (value) {
      if (value == null || value.isEmpty) {
        return 'No se ha seleccionado un tipo de documento';
      }
      return null;
    }
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 0, vertical: 10),
      child: TextFormField(
        readOnly: true,
        controller: TextEditingController()..text = _dniType?.nombre??'',
        onTap:  () {
          http.get(
            tipoDocumentoControllerUri,
            headers: <String, String>{
              'Content-Type': 'application/json; charset=UTF-8',
              'accept': 'application/json',
            },
          ).then((response){
            if (response.statusCode == 200) {
              List<TipoDocumento> tipoDocumentoList=[];
              var jsonResponse = json.decode(response.body);
              for (var item in jsonResponse) {
                tipoDocumentoList.add(TipoDocumento.fromJson(item));
              }
              var items = tipoDocumentoList.map((item) {
                return DialogListItem(
                  label: item.nombre,
                  value: item);
              }).toList();

              showListDialog(
                listItems: items,
                title: 'Tipo de documento',
                context: context,
                callback: (value) {
                  setState(() {
                    _dniType = value;
                  });
                }
              );
            }
          });
        },
        style: const TextStyle(
          color: color_labels
          ),
        decoration: InputDecoration(
          labelText: 'Tipo de documento',
          prefixIcon: const Icon(Icons.account_box_outlined, color: color_iconos),
          fillColor: color_fondo_campos,
          filled: true,
          focusedBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
            borderSide: const BorderSide(
              color: color_borde_campos,
              width: 2.0,
            ),
          ),
          enabledBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
            borderSide: const BorderSide(
              color: color_borde_campos,
              width: 1.0,
            ),
          ),
          errorBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
            borderSide: const BorderSide(
              color: color_errores,
              width: 2.0,
            ),
          ),
          prefixIconColor: color_iconos,
          labelStyle: const TextStyle(
            color: color_labels,
          ),
          errorStyle: const TextStyle(
            color: color_errores,
          )
        ),
        validator: validator,
      ),
    );
  }

  Widget _showButtons(){
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 0, vertical: 20), //apply padding to some sides only
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: <Widget>[
          ElevatedButton(
          style: ButtonStyle(
            backgroundColor: MaterialStateProperty.resolveWith<Color>(
              (Set<MaterialState>states){
                return color_boton_principal;
              }
            ),
          ),
          onPressed: tryRegister,
          child: const Text('Registrarse'),
          )
        ],
      ),
    );
  }

  void tryRegister() {
    if (_formKey.currentState!.validate() && _avatarFile != null) {
      _formKey.currentState!.save();
      showLoadingDialog(context);
      _register();
    }else{
      showAlertDialog(
        type: AlertDialogType.info,
        context: context,
        title: 'Hay campos vacíos',
        content: const Text('Debe completar todos los campos y seleccionar una foto de perfil'),
        callback: (){},
        defaultActionText: 'OK'
      );
    }
  }

  void _register() async {
    var cliente = ClienteCreacion(
      email: _email,
      password: _password,
      nombre: _name,
      apellido: _lastName,
      documento: _dni,
      direccion: _address,
      celular: _phone,
      codigoTipoDocumento: _dniType?.codigoTipoDocumento ?? 0,
    );
    // build multipart request
    var request = http.MultipartRequest('POST', clienteControllerUri);
    //foreach property of the object, add it to the request
    cliente.toJson().forEach((key, value) {
      request.fields[key] = value.toString();
    });
    request.files.add(await http.MultipartFile.fromPath('FotoFile', _avatarFile!.path??''));
    http.StreamedResponse? response;
    try {
      response = await request.send();
    } catch (e) {
      showAlertDialog(
        type: AlertDialogType.error,
        context: context,
        title: 'Error al registrar',
        content: const Text('Ha ocurrido un error al registrar'),
        callback: (){},
        defaultActionText: 'OK'
      );
    }
    finally {
      setState(() {
        Navigator.pop(context);
      });
    }
    if (response == null){
      return;
    }

    if (response.statusCode >= 200 && response.statusCode < 300) {
      showAlertDialog(
        type: AlertDialogType.success,
        content: const Text('Inicia sesión para continuar'),
        title: 'Se ha registrado correctamente',
        context: context,
        callback: () {
          Navigator.pushNamed(context, '/login');
        }
      );
      return;
    }
    if(response.statusCode>=400 && response.statusCode<500) {
      showAlertDialog(
        type: AlertDialogType.warning,
        content: Text(response.reasonPhrase??''),
        title: 'El servidor ha devuelto un error',
        context: context,
        callback: () {
        }
      );
    } else {
      showAlertDialog(
        type: AlertDialogType.error,
        content: const Text('No se ha podido establecer conexión con el servidor'),
        title: 'Error al registrar usuario',
        context: context,
        callback: () {
        }
      );
    }
  }
}