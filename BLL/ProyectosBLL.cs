using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P2_AP2_Darianna_20190261.DAL;
using P2_AP2_Darianna_20190261.Entidades;

namespace P2_AP2_Darianna_20190261.BLL
{
    public class ProyectosBLL
    {
        public static bool Guardar(Proyectos proyecto)
        {
            if (!Existe(proyecto.TipoId))
            {
                return Insertar(proyecto);
            }
            else
            {
                return Modificar(proyecto);
            }
        }

        public static bool Insertar(Proyectos proyecto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Proyectos.Add(proyecto);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Proyectos proyecto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM ProyectosDetalle Where TipoId ={proyecto.TipoId}");
                foreach (var item in proyecto.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                contexto.Entry(proyecto).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var proyecto = contexto.Proyectos.Find(id);
                if (proyecto != null)
                {
                    contexto.Entry(proyecto).State = EntityState.Deleted;
                    paso = contexto.SaveChanges() > 0;

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Proyectos.Any(e => e.TipoId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }

        public static Proyectos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Proyectos proyecto;
            try
            {
                proyecto = contexto.Proyectos.Include(e => e.Detalle).Where(p => p.TipoId == id).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return proyecto;
        }

        public static List<Proyectos> GetList(Expression<Func<Proyectos, bool>> criterio)
        {
            List<Proyectos> lista = new List<Proyectos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Proyectos.Where(criterio).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static List<Proyectos> GetProyectos()
        {
            List<Proyectos> lista = new List<Proyectos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Proyectos.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}