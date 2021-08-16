using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdEmpresa { get; set; }
        //public Empresa Empresa { get; set; }
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdSucursal { get; set; }
        //public Sucursal Sucursal { get; set; }
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdRol { get; set; }
        public Roles Roles { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public bool EsAdmin { get; set; }
        public bool Activo { get; set; }
    }
}
