using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class RelacionLLaveSeccion
    {
        public int Id { get; set; }
        public int IdLlave { get; set; }
        public int IdSeccion { get; set; }
        public bool Activo { get; set; }
    }
}
