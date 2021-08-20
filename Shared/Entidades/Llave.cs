using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class Llave
    {
        public int Id { get; set; }
        public int IdTablilla { get; set; }

        public string Descripcion { get; set; }

        public bool Estatus { get; set; }

    //    [Required(ErrorMessage = "El campo {0} es requerido")]
    //    public string Mac { get; set; }
    //    [Required(ErrorMessage = "El campo {0} es requerido")]
    //    public string Ip { get; set; }

    //    public bool Activo { get; set; }
    }
}
