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
    /// Логика взаимодействия для One_force.xaml
    /// </summary>
    public partial class One_force : Window
    {
        public One_force()
        {
            InitializeComponent();
        }

        // Свойство для номера силы 
        public int N_Forse
        {
             set
            {
                txtN_Force.Text = value.ToString();
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

        // Свойство для значения силы
        public double ValueForce
        {
            get
            {
                return Convert.ToDouble(txtValueForce.Text);
            }
            set
            {
                txtValueForce.Text = value.ToString();
            }
        }
        // Свойство для среды приложения силы
        public string Env
        {
            get
            {
                return cmbEnv.Text;
            }
            set
            {
                cmbEnv.Text = value.Trim();
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
