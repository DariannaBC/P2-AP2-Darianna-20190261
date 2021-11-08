using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP2_Darianna_20190261.Entidades
{
    public class ProyectosDetalle
    {

        [Key]
        public int DetalleId { get; set; }
        public int TipoId { get; set; }
        public string TipoTarea { get; set; }
        public string Requerimentos { get; set; }
        public int Tiempo { get; set; }

        public ProyectosDetalle()
        {
            DetalleId = 0;
            TipoId = 0;
            TipoTarea = "";
            Requerimentos = "";
            Tiempo = 0;
        }

        public ProyectosDetalle(int TipoId, string TipoTarea, string Requerimentos, int Tiempo)
        {
            this.TipoId = TipoId;
            this.TipoTarea = TipoTarea;
            this.Requerimentos = Requerimentos;
            this.Tiempo = Tiempo;
        }
    }
}
