using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class RelacionLlaveHorario
    {
        public int Id { get; set; }
        public int IdLlave { get; set; }
        public int IdHorario { get; set; }
        public bool Activo { get; set; }
    }
}
