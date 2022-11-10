namespace Barberiapp.DTOs.Cita
{
    public class CitaDTO
    {
        public int CodigoCita { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public int Valoracion { get; set; }

        public string Comentario { get; set; }

        public bool Estado { get; set; }

        public int CodigoBarbero { get; set; }

        public int CodigoCliente { get; set; }
    }
}
