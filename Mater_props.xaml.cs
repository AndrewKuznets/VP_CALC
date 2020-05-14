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
        public string Mat1
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


    }
}
