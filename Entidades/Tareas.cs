using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP2_Darianna_20190261.Entidades
{
    public class Tareas
    {
        [Key]
        public int TareaId { get; set; }
        public string TipoTarea { get; set; }
        public DateTime FechaTarea { get; set; }
        public int Tiempo { get; set; }

        [ForeignKey("TareaId")]
        public List<TareasDetalle> Detalle { get; set; }

        public Tareas()
        {
            Tiempo = 0;
            FechaTarea = DateTime.Now;
        }
    }
}