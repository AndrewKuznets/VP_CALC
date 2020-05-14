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
        public int Kol_elem
        {
            get
            {
                return Convert.ToInt32(txtKol_elem.Text);
            }
            set
            {
                txtKol_elem.Text = value.ToString();
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


    }
}
