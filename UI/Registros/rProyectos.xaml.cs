using P2_AP2_Darianna_20190261.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using P2_AP2_Darianna_20190261.Entidades;

namespace P2_AP2_Darianna_20190261.UI.Registros
{
    /// <summary>
    /// Lógica de interacción para rProyectos.xaml
    /// </summary>
    public partial class rProyectos : Window
    {
        private Proyectos proyectos = new Proyectos();
        private Tareas tareas = new Tareas();
        public rProyectos()
        {
            InitializeComponent();
            this.DataContext = this.proyectos;
            LlenarComboTipoDeTareas();
            tiempoTotalTextBox.Text = 0.ToString();
        }

        private void BuscarIDButton_Click(object sender, RoutedEventArgs e)
        {
            var Proyectos = ProyectosBLL.Buscar(Utilidades.ToInt(proyectoIdTextBox.Text));

            if (Proyectos != null)
            {
                this.proyectos = Proyectos;
            }
            else
            {
                this.proyectos = new Proyectos();
            }
            Cargar();
        }

        private void AgregarDetalleButton_Click(object sender, RoutedEventArgs e)
        {
            tareas = TareasBLL.Buscar(Convert.ToInt32(tipoTareaComboBox.SelectedValue));

            proyectos.Detalle.Add(new ProyectosDetalle
            {
                TipoId = tareas.TareaId,
                TipoTarea = tareas.TipoTarea,
                Requerimentos = requerimientosDeTareaTextBox.Text,
                Tiempo = Convert.ToInt32(tiempoMinutosTextBox.Text)
            });

            int total = Convert.ToInt32(tiempoTotalTextBox.Text);
            int tiempo = Convert.ToInt32(tiempoMinutosTextBox.Text);
            total += tiempo;
            tiempoTotalTextBox.Text = Convert.ToString(total);

            Cargar();
        }

        private void RemoverFila_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.SelectedIndex < 0)
            {
                return;
            }
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                ProyectosDetalle proyec = (ProyectosDetalle)DetalleDataGrid.SelectedItem;

                int total = Convert.ToInt32(tiempoTotalTextBox.Text);
                string tiempo = proyec.Tiempo.ToString();
                total -= Convert.ToInt32(tiempo);
                tiempoTotalTextBox.Text = Convert.ToString(total);

                proyectos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
            {
                return;
            }

            llenarProyec();

            var paso = ProyectosBLL.Guardar(this.proyectos);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Su proyecto ha sido guardado o modificado correctamente");
            }
            else
            {
                MessageBox.Show("Su proyecto no ha podido ser almacenado...");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Proyectos existe = ProyectosBLL.Buscar(this.proyectos.TipoId);

            if (ProyectosBLL.Eliminar(this.proyectos.TipoId))
            {
                Limpiar();
                MessageBox.Show("Su proyecto ha sido eliminado con exito");
            }
            else
            {
                MessageBox.Show("No fue posible eliminarlo");
            }
        }

        private void LlenarComboTipoDeTareas()
        {
            this.tipoTareaComboBox.ItemsSource = TareasBLL.GetTareas();
            this.tipoTareaComboBox.SelectedValuePath = "TareaId";
            this.tipoTareaComboBox.DisplayMemberPath = "TipoTarea";

            if (tipoTareaComboBox.Items.Count > 0)
            {
                tipoTareaComboBox.SelectedIndex = 0;
            }
        }

        private void Cargar()
        {
            this.DetalleDataGrid.ItemsSource = null;
            this.DetalleDataGrid.ItemsSource = this.proyectos.Detalle;
            this.DataContext = null;
            this.DataContext = this.proyectos;
        }

        private void Limpiar()
        {
            proyectos = new Proyectos();
            Cargar();
            LlenarComboTipoDeTareas();
            descripcionProyectoTextBox.Text = "";
            proyectoIdTextBox.Text = "0";
        }

        private bool Validar()
        {
            bool esValido = true;

            if (proyectoIdTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo Proyecto Id no puede quedar vacio");
            }
            if (descripcionProyectoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo Descripcion del proyecto no puede quedar vacio");
            }
            if (string.IsNullOrWhiteSpace(tipoTareaComboBox.Text))
            {
                esValido = false;
                MessageBox.Show("Debe seleccionar un tipo de tarea");
            }
            if (requerimientosDeTareaTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo de requerimientos de tarea no puede quedar vacio");
            }
            if (tiempoMinutosTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo tiempo en minutos no puede quedar vacio");
            }

            return esValido;
        }

        private void llenarProyec()
        {
            proyectos.TiempoTotal = Convert.ToInt32(tiempoTotalTextBox.Text);
            proyectos.DescripcionProyecto = descripcionProyectoTextBox.Text;
            proyectos.Fecha = (DateTime)fechaDatePicker.SelectedDate;
        }
    }
}