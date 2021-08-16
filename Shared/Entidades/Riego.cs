﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIEGO.Shared.Entidades
{
    public class Riego
    {
        public int Id { get; set; }
        public int IdRelacionLlaveSeccion { get; set; }
        public decimal Litros { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFIN { get; set; }
        public bool Activo { get; set; }
    }
}