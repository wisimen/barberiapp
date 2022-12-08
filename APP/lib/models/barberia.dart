
class Barberia {
  int codigoBarberia;
  String nombre;
  String direccion;
  String celular;
  String correo;
  String? urlUbicacion;
  String? urlInstagram;
  String? urlFacebook;
  String? urlYoutube;
  String logoFile;

  Barberia({
    required this.codigoBarberia,
    required this.nombre,
    required this.direccion,
    required this.celular,
    required this.correo,
    required this.urlUbicacion,
    required this.urlInstagram,
    required this.urlFacebook,
    required this.urlYoutube,
    required this.logoFile,
  });

  factory Barberia.fromJson(Map<String, dynamic> json) => Barberia(
        codigoBarberia: json["codigoBarberia"],
        nombre: json["nombre"],
        direccion: json["direccion"],
        celular: json["celular"],
        correo: json["correo"],
        urlUbicacion: json["urL_Ubicacion"]??'',
        urlInstagram: json["urL_Instagram"]??'',
        urlFacebook: json["urL_Facebook"]??'',
        urlYoutube: json["urL_Youtube"]??'',
        logoFile: json["logo"],
      );

  Map<String, dynamic> toJson() => {
        "codigoBarberia": codigoBarberia,
        "nombre": nombre,
        "direccion": direccion,
        "celular": celular,
        "correo": correo,
        "url_Ubicacion": urlUbicacion,
        "url_Instagram": urlInstagram,
        "url_Facebook": urlFacebook,
        "url_Youtube": urlYoutube,
        "logoFile": logoFile,
      };
}