class TipoDocumento {
  int codigoTipoDocumento;
  String nombre;

  TipoDocumento({
    required this.codigoTipoDocumento,
    required this.nombre,
  });
  factory TipoDocumento.fromJson(Map<String, dynamic> json) => TipoDocumento(
        codigoTipoDocumento: json["codigoTipoDocumento"],
        nombre: json["nombre"],
      );
  Map<String, dynamic> toJson() => {
        "codigoTipoDocumento": codigoTipoDocumento,
        "nombre": nombre,
      };
}