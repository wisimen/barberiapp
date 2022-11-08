namespace Barberiapp.DTOs.Horario
{
    public class HorarioDTO
    {
        public int CodigoHorario { get; set; }

        public int Dia { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public int CodigoBarberia { get; set; }
    }
}

