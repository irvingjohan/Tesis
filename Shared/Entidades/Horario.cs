using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class Horario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Dia { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int Agrupador { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

    }
}
