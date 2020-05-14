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


namespace VP_CALC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VP_INPUT_DATA vp_input_data = new VP_INPUT_DATA();

        public MainWindow()
        {
            InitializeComponent();
        }
   // Добавить данные по элементу ВП
        private void btnAddElem_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем окно ввода имени данных по  элементу ВП
            Elem_VP dialog = new Elem_VP();

            dialog.N_Elem = vp_input_data.Elems_VP.Count + 1;
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;
        }
    }
}
