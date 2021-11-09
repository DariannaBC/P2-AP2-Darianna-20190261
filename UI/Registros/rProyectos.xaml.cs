﻿using P2_AP2_Darianna_20190261.BLL;
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
                this.proyectos = Proyectos;
            else
                this.proyectos = new Proyectos();
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
                Tiempo = (int)Convert.ToInt32(tiempoMinutosTextBox.Text)
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
                return;//Con estas 2 lineas ya no vuelve a pasar
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
                return;

            var paso = ProyectosBLL.Guardar(this.proyectos);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Su proyecto se ha guardado  o modificado con exito!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Proyectos existe = ProyectosBLL.Buscar(this.proyectos.TipoId);

            if (ProyectosBLL.Eliminar(this.proyectos.TipoId))
            {
                Limpiar();
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("No fue posible eliminarlo", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
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
        }

        private bool Validar()
        {
            bool esValido = true;

            if (proyectoIdTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo proyecto Id no puede quedar vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (descripcionProyectoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo Descripcio del proyecto no puede quedar vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (string.IsNullOrWhiteSpace(tipoTareaComboBox.Text))
            {
                esValido = false;
                MessageBox.Show("Debe seleccionar un tipo de tarea", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (requerimientosDeTareaTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo de requerimientos de tarea no puede quedar vacio");
            }
            if (tiempoMinutosTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("El campo de tiempo en minutos no puede quedar vacio");
            }

            return esValido;
        }
    }
}