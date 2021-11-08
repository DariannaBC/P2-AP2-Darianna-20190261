using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP2_Darianna_20190261.Entidades
{
    public class TareasDetalle
    {
        [Key]
        public int TareaId { get; set; }
        public string TipoTarea { get; set; }
    }
}