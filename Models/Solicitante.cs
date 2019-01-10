using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;////////////ESTO LO HE COLOCADO DE PRUEBA/////////
using System.ComponentModel.DataAnnotations.Schema;//ESTO LO HABIA COLOCADO POR ALGUN MOTIVO///

namespace BBKBootCamp.Models
{
    public class Solicitante
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [Display(Name = "FechaNacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string SexoTipo { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Proceso { get; set; }

    }
}
