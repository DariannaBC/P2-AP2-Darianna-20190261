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
using System.Windows.Navigation;
using System.Windows.Shapes;
using P2_AP2_Darianna_20190261.UI.Consultas;
using P2_AP2_Darianna_20190261.UI.Registros;

namespace P2_AP2_Darianna_20190261
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            rProyectos pro = new rProyectos();
            pro.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            cProyectos cpro = new cProyectos();
            cpro.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            cTareas cta = new cTareas();
            cta.Show();
        }
    }
}
