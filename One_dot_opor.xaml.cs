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

namespace VP_CALC
{
    /// <summary>
    /// Логика взаимодействия для One_dot_opor.xaml
    /// </summary>
    public partial class One_dot_opor : Window
    {
        public One_dot_opor()
        {
            InitializeComponent();
        }
        // Свойство для номера точечной опоры 
        public int N_dot_opor
        {
            set
            {
                txtN_dot_opor.Text = value.ToString();
            }
        }
        // Свойство для номера элемента ВП
        public int N_Elem
        {
            get
            {
                return Convert.ToInt32(txtN_Elem.Text);
            }
            set
            {
                txtN_Elem.Text = value.ToString();
            }
        }
        // Свойство для смещения кормового
        public double Sm_korm
        {
            get
            {
                return Convert.ToDouble(txtSm_korm.Text);
            }
            set
            {
                txtSm_korm.Text = value.ToString();
            }
        }
        // Свойство для комментария по силе
        public string Comm
        {
            get
            {
                return txtComm.Text.Trim();
            }
            set
            {
                txtComm.Text = value.Trim();
            }
        }
// Обработка клика на Ok
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string _str;
            //----------Номер элемента----------------
            _str = txtN_Elem.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_Elem.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtN_Elem.Focus();
                return;
            }
           
            //----------Смещение кормовое----------------
            _str = txtSm_korm.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtSm_korm.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtSm_korm.Focus();
                return;
            }
            
            DialogResult = true;
            Close();
        }
    }
}
