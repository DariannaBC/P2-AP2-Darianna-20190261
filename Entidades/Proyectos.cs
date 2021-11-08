using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP2_Darianna_20190261.Entidades
{
    public class Proyectos
    {
        [Key]
        public int TipoId { get; set; }
        public DateTime Fecha { get; set; }
        public string DescripcionProyecto { get; set; }
        public int TiempoTotal { get; set; }

        [ForeignKey("TipoId")]
        public List<ProyectosDetalle> Detalle { get; set; }

        public Proyectos()
        {
            TipoId = 0;
            Fecha = DateTime.Now;
            DescripcionProyecto = "";
            TiempoTotal = 0;
            Detalle = new List<ProyectosDetalle>();
        }
    }
}
