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
    /// Логика взаимодействия для Elem_VP.xaml
    /// </summary>
    public partial class Elem_VP : Window
    {
        public Elem_VP()
        {
            InitializeComponent();
        }
        // Свойство для номера элемента ВП
        public int N_Elem
        {
            set
            {
                txtN_Elem.Text = value.ToString();
            }
        }
        // Свойство для длины элемента ВП
        public double Lx
        {
            get
            {
                return Convert.ToDouble(txtLx.Text);
            }
            set
            {
                txtLx.Text = value.ToString();
            }
        }
        // Свойство для диаметра вала элемента ВП
        public double D_vala
        {
            get
            {
                return Convert.ToDouble(txtD_vala.Text);
            }
            set
            {
                txtD_vala.Text = value.ToString();
            }
        }
        // Свойство для диаметра облицовки элемента ВП
        public double D_Obl
        {
            get
            {
                if (txtD_Obl.Text.Length == 0) return 0;
                else
                return Convert.ToDouble(txtD_Obl.Text);
            }
            set
            {
                txtD_Obl.Text = value.ToString();
            }
        }

        // Свойство для внутреннего диаметра вала элемента ВП
        public double D_vnut
        {
            get
            {
                return Convert.ToDouble(txtD_vnut.Text);
            }
            set
            {
                txtD_vnut.Text = value.ToString();
            }
        }
        // Свойство для количества слоёв
        public int N_Layers
        {
            get
            {
                return Convert.ToInt32(txtN_Layers.Text);
            }
            set
            {
                txtN_Layers.Text = value.ToString();
            }
        }
        // Свойство для первого материала элемента ВП
        public string Mat1
        {
            get
            {
                return cmbMat1.Text;
            }
            set
            {
                cmbMat1.Text = value.Trim();
            }
        }
        // Свойство для второго материала элемента ВП
        public string Mat2
        {
            get
            {
                return cmbMat2.Text.Trim();
            }
            set
            {
                cmbMat2.Text = value.Trim();
            }
        }
        // Свойство для среды элемента ВП
        public string Env
        {
            get
            {
                return cmbEnv.Text.Trim();
            }
            set
            {
                cmbEnv.Text = value.Trim();
            }
        }
        // Свойство для комментария по  элементу ВП
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
// Обработка клика кнопки ОК
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string _str =  txtN_Layers.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_Layers.Focus();
                return;
            }

            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtN_Layers.Focus();
                return;
            }
            //----------Длина вала----------------
             _str = txtLx.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtLx.Focus();
                return;
            }

            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtLx.Focus();
                return;
            }
            //----------Диаметр вала----------------
            _str = txtD_vala.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_Layers.Focus();
                return;
            }

            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtD_vala.Focus();
                return;
            }
            //----------Диаметр облицовки----------------
            _str = txtD_Obl.Text.Trim();
            //if (_str == "")
            //{
            //    MessageBox.Show("Значение параметра на введено!", "Внимание");
            //    txtN_Layers.Focus();
            //    return;
            //}
            if (_str.Length > 0 && !kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtD_Obl.Focus();
                return;
            }
            //----------Внутренний диаметр ----------------
            _str = txtD_vnut.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_Layers.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtD_vnut.Focus();
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
