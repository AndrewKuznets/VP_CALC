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
    /// Логика взаимодействия для Prot_opor.xaml
    /// </summary>
    public partial class Prot_opor : Window
    {
        public Prot_opor()
        {
            InitializeComponent();
        }

        // Свойство для номера протяжённой опоры 
        public int N_prot_opor
        {
            set
            {
                txtN_prot_opor.Text = value.ToString();
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
        // Свойство для количества элементов ВП над опорой
        public int Kol_Elems
        {
            get
            {
                return Convert.ToInt32(txtKol_Elem.Text);
            }
            set
            {
                txtKol_Elem.Text = value.ToString();
            }
        }
        // Свойство для тангенса угла наклона
        public double TG_UN
        {
            get
            {
                return Convert.ToDouble(txtTG_UN.Text);
            }
            set
            {
                txtTG_UN.Text = value.ToString();
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
        // Свойство для диам. зазора
        public double DZ
        {
            get
            {
                return Convert.ToDouble(txtDZ.Text);
            }
            set
            {
                txtDZ.Text = value.ToString();
            }
        }
        // Свойство для упругого основания
        public double Upr_osn
        {
            get
            {
                return Convert.ToDouble(txtUpr_osn.Text);
            }
            set
            {
                txtUpr_osn.Text = value.ToString();
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
/// <summary>
/// Обработка кромки Ok
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
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
           //----------Количество элементов----------------
            _str = txtKol_Elem.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtKol_Elem.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtKol_Elem.Focus();
                return;
            }

            //----------Тангенс угла наклона----------------
            _str = txtTG_UN.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtTG_UN.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtTG_UN.Focus();
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

            //----------Диаметральный зазор--------------
            _str = txtDZ.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtDZ.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtDZ.Focus();
                return;
            }

            //----------Упругое основание-----------
            _str = txtUpr_osn.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtUpr_osn.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtUpr_osn.Focus();
                return;
            }

            DialogResult = true;
            Close();

        }
    }
}
