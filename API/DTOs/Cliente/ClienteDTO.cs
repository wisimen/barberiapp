namespace Barberiapp.DTOs.Cliente
{
    public class ClienteDTO
    {
        public int CodigoCliente { get; set; }
        public string CodigoUsuario { get; set; }

        public string Email { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Documento { get; set; }

        public string Direccion { get; set; }

        public string Celular { get; set; }

        public int CodigoTipoDocumento { get; set; }

        public string Foto { get; set; }
    }
}

