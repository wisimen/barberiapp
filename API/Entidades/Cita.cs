﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoCita { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.DateTime, ErrorMessage = "El campo {0} no cumple con el formato")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Time, ErrorMessage = "El campo {0} no cumple con el formato")]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Currency, ErrorMessage = "El campo {0} no cumple con el formato")]
        public decimal Valoracion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarbero { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarberia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoMedioPago { get; set; }

        // Referencias

        [ForeignKey("CodigoBarbero")]
        public Barbero Barbero { get; set; }

        [ForeignKey("CodigoCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("CodigoBarberia")]
        public Barberia Barberia { get; set; }

        public ICollection<Servicio> Servicios { get; set; }

        public MediosPago MedioPago { get; set; }
    }
}
