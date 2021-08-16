using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class Horario
    {
        public int Id { get; set; }
        public string Dia { get; set; }
        public int IdSeccion { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public bool Activo { get; set; }
    }
}
