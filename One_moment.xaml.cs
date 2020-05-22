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
    /// Логика взаимодействия для One_moment.xaml
    /// </summary>
    public partial class One_moment : Window
    {
        public One_moment()
        {
            InitializeComponent();
        }

        // Свойство для номера момента 
        public int N_Moment
        {
            set
            {
                txtN_Moment.Text = value.ToString();
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
        // Свойство для значения момента
        public double ValueMoment
        {
            get
            {
                return Convert.ToDouble(txtValueMoment.Text);
            }
            set
            {
                txtValueMoment.Text = value.ToString();
            }
        }

        // Свойство для комментария по моменту
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
// Обработка кнопки Ок для момента
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
            //----------Величина силы----------------
            _str = txtValueMoment.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtValueMoment.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtValueMoment.Focus();
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
