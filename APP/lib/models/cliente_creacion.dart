class ClienteCreacion {
  String email;
  String password;
  String nombre;
  String apellido;
  String documento;
  String direccion;
  String celular;
  int codigoTipoDocumento;

  ClienteCreacion({
    required this.email,
    required this.password,
    required this.nombre,
    required this.apellido,
    required this.documento,
    required this.direccion,
    required this.celular,
    required this.codigoTipoDocumento,
  });

  factory ClienteCreacion.fromJson(Map<String, dynamic> json) => ClienteCreacion(
        email: json["email"],
        password: json["password"],
        nombre: json["nombre"],
        apellido: json["apellido"],
        documento: json["documento"],
        direccion: json["direccion"],
        celular: json["celular"],
        codigoTipoDocumento: json["codigoTipoDocumento"],
  );

  Map<String, dynamic> toJson() => {
        "email": email,
        "password": password,
        "nombre": nombre,
        "apellido": apellido,
        "documento": documento,
        "direccion": direccion,
        "celular": celular,
        "codigoTipoDocumento": codigoTipoDocumento,
  };
}