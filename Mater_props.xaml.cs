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
    /// Логика взаимодействия для Mater_props.xaml
    /// </summary>
    public partial class Mater_props : Window
    {
        public Mater_props()
        {
            InitializeComponent();
        }
        // Свойство для номера материала
        public int N_mat
        {
            set
            {
                txtN_mat.Text = value.ToString();
            }
        }

        // Свойство для имени материала
        public string Mat_name
        {
            get
            {
                return cmbMat_name.Text;
            }
            set
            {
                cmbMat_name.Text = value.Trim();
            }
        }
        // Свойство для внутреннего диаметра вала элемента ВП
        public double Mod_upr
        {
            get
            {
                return Convert.ToDouble(txtMod_upr.Text);
            }
            set
            {
                txtMod_upr.Text = value.ToString();
            }
        }

        // Свойство для внутреннего диаметра вала элемента ВП
        public double Mod_sdv
        {
            get
            {
                return Convert.ToDouble(txtMod_sdv.Text);
            }
            set
            {
                txtMod_sdv.Text = value.ToString();
            }
        }
        // Свойство для внутреннего диаметра вала элемента ВП
        public double Densi
        {
            get
            {
                return Convert.ToDouble(txtDensi.Text);
            }
            set
            {
                txtDensi.Text = value.ToString();
            }
        }
/// <summary>
/// Обработка кнопки Ok
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string _str;
            //----------Номер материала---------------
            _str = txtN_mat.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_mat.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtN_mat.Focus();
                return;
            }
            //---------Модуль упругости--------------
            _str = txtMod_upr.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtMod_upr.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtMod_upr.Focus();
                return;
            }
            //---------Модуль сдвига --------------
            _str = txtMod_sdv.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtMod_sdv.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtMod_sdv.Focus();
                return;
            }

            //---------Плотность  --------------
            _str = txtDensi.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtDensi.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtDensi.Focus();
                return;
            }


            DialogResult = true;
            Close();
        }
    }
}
