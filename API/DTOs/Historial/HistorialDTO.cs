namespace Barberiapp.DTOs.Historial
{
    public class HistorialDTO
    {
        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public int CodigoVehiculo { get; set; }

        public int CodigoUsuario { get; set; }

        public string ResumenDeMantenimiento { get; set; }
    }
}
