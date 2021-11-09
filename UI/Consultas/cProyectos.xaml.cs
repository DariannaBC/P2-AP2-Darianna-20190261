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
using P2_AP2_Darianna_20190261.BLL;
using P2_AP2_Darianna_20190261.Entidades;

namespace P2_AP2_Darianna_20190261.UI.Consultas
{
    /// <summary>
    /// Lógica de interacción para cProyectos.xaml
    /// </summary>
    public partial class cProyectos : Window
    {
        public cProyectos()
        {
            InitializeComponent();
        }

        private void BuscarCriterioButton_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Proyectos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //ProyectoId
                        int.TryParse(CriterioTextBox.Text, out int ProyectoId);
                        lista = ProyectosBLL.GetList(a => a.TipoId == ProyectoId);
                        break;

                    case 1: //DescripcionProyecto
                        lista = ProyectosBLL.GetList(a => a.DescripcionProyecto.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;
                }
            }
            else
            {
                lista = ProyectosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                lista = lista.Where(c => c.Fecha.Date >= DesdeDatePicker.SelectedDate).ToList();

            if (HastaDatePicker.SelectedDate != null)
                lista = lista.Where(c => c.Fecha.Date <= HastaDatePicker.SelectedDate).ToList();

            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = lista;
        }
    }
}