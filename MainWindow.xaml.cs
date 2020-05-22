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
        // Создадим хранилище всех данных по валопроводу!
        public VP_INPUT_DATA vp_input_data = new VP_INPUT_DATA();

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


            defOne_elem_VP newOneElem = new defOne_elem_VP
            {
                N_elem = vp_input_data.Elems_VP.Count + 1,
                Lx = dialog.Lx,
                D_vala = dialog.D_vala,
                D2 = dialog.D_Obl,
                D3 = dialog.D_vnut,
                N_Layers = dialog.N_Layers,
                Mat1 = dialog.Mat1,
                Mat2 = dialog.Mat2,
                Env = dialog.Env,
                Comment = dialog.Comm
            };

            dgrVP_elems.Items.Add(newOneElem);
            vp_input_data.Elems_VP.Add(newOneElem);

            txtN_Elem.Text = vp_input_data.Elems_VP.Count.ToString();
        }

        // Редактирование элемента валопровода
        private void btnEditElem_Click(object sender, RoutedEventArgs e)
        {

            if (dgrVP_elems.SelectedItem == null)
            {
                MessageBox.Show("Отметьте в таблице элемент валопровода !",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrVP_elems.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты параметра

            string[] AttribPar = new string[10];

            for (int iAttr = 0; iAttr < AttribPar.Length; iAttr++)
            {
                int Number_nameCOL = iAttr; // номер столбца DataGrid c именем параметра
                var _cell = new DataGridCellInfo(dgrVP_elems.SelectedItem, dgrVP_elems.Columns[Number_nameCOL]);
                var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
                AttribPar[iAttr] = cell_content.Text.Trim();
            }

            int N_Elem = Convert.ToInt32(AttribPar[0]);
            // Вызываем окно редактирования параметра элемента Словаря
            Elem_VP dialog = new Elem_VP();
            // Инициализируем значения атрибутов
            dialog.N_Elem = N_Elem;
            dialog.Lx = Convert.ToInt32(AttribPar[1]);
            dialog.D_vala = Convert.ToDouble(AttribPar[2]);
            dialog.D_Obl = Convert.ToDouble(AttribPar[3]);
            dialog.D_vnut = Convert.ToDouble(AttribPar[4]);
            dialog.N_Layers = Convert.ToInt32(AttribPar[5]);
            dialog.Mat1 = AttribPar[6].Trim();
            dialog.Mat2 = AttribPar[7].Trim();
            dialog.Env = AttribPar[8].Trim();
            dialog.Comm = AttribPar[9].Trim();

            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_elem_VP newOneElem = new defOne_elem_VP
            {
                N_elem = N_Elem,
                Lx = dialog.Lx,
                D_vala = dialog.D_vala,
                D2 = dialog.D_Obl,
                D3 = dialog.D_vnut,
                N_Layers = dialog.N_Layers,
                Mat1 = dialog.Mat1,
                Mat2 = dialog.Mat2,
                Env = dialog.Env,
                Comment = dialog.Comm
            };

            //   dgrVP_elems.Items.Clear();
            //    dgrVP_elems.ItemsSource = vp_input_data.Elems_VP;

            vp_input_data.Elems_VP.Insert(N_Elem - 1, newOneElem);
            vp_input_data.Elems_VP.RemoveAt(N_Elem);

            dgrVP_elems.Items.Insert(N_Elem - 1, newOneElem);
            dgrVP_elems.Items.RemoveAt(N_Elem);
            dgrVP_elems.SelectedIndex = N_Elem - 1;
          
        }
        // Удалить элемент ВП
        private void btnDelElem_Click(object sender, RoutedEventArgs e)
        {

            if (dgrVP_elems.SelectedItem == null)
            {
                MessageBox.Show("Отметьте в таблице элемент валопровода для удаления!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrVP_elems.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты параметра

            string N_Elem_str;
            var _cell = new DataGridCellInfo(dgrVP_elems.SelectedItem, dgrVP_elems.Columns[0]);
            var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
            N_Elem_str = cell_content.Text.Trim();

            int N_Elem = Convert.ToInt32(N_Elem_str);
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Удалим элемент валопровода № "+ N_Elem_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Удалим элемент из коллекции

            List<defOne_elem_VP> newElems_VP = new List<defOne_elem_VP>();
            dgrVP_elems.Items.Clear();
            defOne_elem_VP newOne_elem_VP;

            for (int ii = 0; ii < vp_input_data.Elems_VP.Count; ii++)
            {
                if (ii < N_Elem -1)
                {
                    newElems_VP.Add(vp_input_data.Elems_VP[ii]);
                    dgrVP_elems.Items.Add(vp_input_data.Elems_VP[ii]);
                }
                else if (ii == N_Elem -1) continue;
                else
                {
                    newOne_elem_VP = vp_input_data.Elems_VP[ii];
                    newOne_elem_VP.N_elem = ii ;
                    newElems_VP.Add(newOne_elem_VP);
                    dgrVP_elems.Items.Add(newOne_elem_VP);
                }
            } // for

            vp_input_data.Elems_VP = newElems_VP;
            txtN_Elem.Text = vp_input_data.Elems_VP.Count.ToString();

        }
/// <summary>
/// Сохранить данные шапки ВП
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnSaveGlabalData_Click(object sender, RoutedEventArgs e)
        {
            string _str = txtCalcName.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtCalcName.Focus();
                return;
            }
            vp_input_data.CalcName = _str;
            //----------Количество смещаемых опор----------------
            _str = txtN_SmOpor.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_SmOpor.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtN_SmOpor.Focus();
                return;
            }
            vp_input_data.N_SmOpor = Convert.ToInt32(_str);
            //----------Число дейд.подшипников--------------
            _str = txtN_TypeDP.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_TypeDP.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtN_TypeDP.Focus();
                return;
            }
            vp_input_data.N_TypeDP = Convert.ToInt32(_str);
            //----------Число типов материалов--------------
            _str = txtN_TypeMat.Text.Trim();
            if (_str == "")
            {
                MessageBox.Show("Значение параметра на введено!", "Внимание");
                txtN_TypeMat.Focus();
                return;
            }
            if (!kaa_convert.is_number(_str))
            {
                MessageBox.Show("Нечисловые символы в значении параметра!", "Внимание");
                txtN_TypeMat.Focus();
                return;
            }

            vp_input_data.N_TypeMat = Convert.ToInt32(_str);

            if (rbtSGS.IsChecked == true) vp_input_data.units_SYS = "СГС";
            else vp_input_data.units_SYS = "СИ";
           
            vp_input_data.print_Elems_VP = chbPrintElemVP.IsChecked.Value;
            vp_input_data.print_Sm_Elems = chbPrintSMElem.IsChecked.Value;

            if (radioButtonRus.IsChecked == true) vp_input_data.LangPrint = "Rus";
            else vp_input_data.LangPrint = "Eng";
        }
// Добавить силу
        private void btnAddForce_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем окно ввода имени данных по параметрам силы
            One_force dialog = new One_force();

            dialog.N_Force = vp_input_data.Forces.Count + 1;
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_force newOneForce = new defOne_force
            {
                N_force = vp_input_data.Forces.Count + 1,
                N_elem = dialog.N_Elem,
                Value = dialog.ValueForce,
                Env = dialog.Env,
                Comment = dialog.Comm
            };

            dgrForces.Items.Add(newOneForce);
            vp_input_data.Forces.Add(newOneForce);

            txtN_forces.Text = vp_input_data.Forces.Count.ToString();
        }
// Исправить силу в таблице
        private void btnEditForce_Click(object sender, RoutedEventArgs e)
        {
            if (dgrForces.SelectedItem == null)
            {
                MessageBox.Show("Отметьте силу в таблице  !",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrForces.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты параметра

            string[] AttribPar = new string[5];

            for (int iAttr = 0; iAttr < AttribPar.Length; iAttr++)
            {
                int Number_nameCOL = iAttr; // номер столбца DataGrid c именем параметра
                var _cell = new DataGridCellInfo(dgrForces.SelectedItem, dgrForces.Columns[Number_nameCOL]);
                var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
                AttribPar[iAttr] = cell_content.Text.Trim();
            }

            int N_Force = Convert.ToInt32(AttribPar[0]);
            // Вызываем окно редактирования параметра элемента Словаря
            One_force dialog = new One_force();
            // Инициализируем значения атрибутов
            dialog.N_Force = N_Force;
            dialog.N_Elem = Convert.ToInt32(AttribPar[1]);
            dialog.ValueForce = Convert.ToInt32(AttribPar[2]);
            dialog.Env = AttribPar[3].Trim();
            dialog.Comm = AttribPar[4].Trim();

            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_force newOneForce = new defOne_force
            {
                N_force = vp_input_data.Forces.Count,
                N_elem = dialog.N_Elem,
                Value = dialog.ValueForce,
                Env = dialog.Env,
                Comment = dialog.Comm
            };

            vp_input_data.Forces.Insert(N_Force - 1, newOneForce);
            vp_input_data.Forces.RemoveAt(N_Force);

            dgrForces.Items.Insert(N_Force - 1, newOneForce);
            dgrForces.Items.RemoveAt(N_Force);
            dgrForces.SelectedIndex = N_Force - 1;
        }
// Удалить силу из таблицы
        private void btnDelForce_Click(object sender, RoutedEventArgs e)
        {

            if (dgrForces.SelectedItem == null)
            {
                MessageBox.Show("Отметьте силу в таблице!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }
            int SI = dgrForces.SelectedIndex;

            // Извлечём из отмеченной строки DataGrid  номер силы
            string N_Force_str;
            var _cell = new DataGridCellInfo(dgrForces.SelectedItem, dgrForces.Columns[0]);
            var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
            N_Force_str = cell_content.Text.Trim();

            int N_Force = Convert.ToInt32(N_Force_str);
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Удалим силу № " + N_Force_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Удалим элемент из коллекции

            List<defOne_force> newForces = new List<defOne_force>();
            dgrForces.Items.Clear();
            defOne_force newOne_Force;

            for (int ii = 0; ii < vp_input_data.Forces.Count; ii++)
            {
                if (ii < N_Force - 1)
                {
                    newForces.Add(vp_input_data.Forces[ii]);
                    dgrForces.Items.Add(vp_input_data.Forces[ii]);
                }
                else if (ii == N_Force - 1) continue;
                else
                {
                    newOne_Force = vp_input_data.Forces[ii];
                    newOne_Force.N_force = ii;
                    newForces.Add(newOne_Force);
                    dgrForces.Items.Add(newOne_Force);
                }
            } // for

            vp_input_data.Forces = newForces;
            txtN_forces.Text = vp_input_data.Forces.Count.ToString();
        }
// Добавить момент
        private void btnAddMom_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем окно ввода имени данных по параметрам момента
            One_moment dialog = new One_moment();

            dialog.N_Moment = vp_input_data.Moments.Count + 1;
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_moment newOneMoment = new defOne_moment
            {
                N_moment = vp_input_data.Moments.Count + 1,
                N_elem = dialog.N_Elem,
                Value = dialog.ValueMoment,
                Comment = dialog.Comm
            };

            dgrMoments.Items.Add(newOneMoment);
            vp_input_data.Moments.Add(newOneMoment);

            txtN_moments.Text = vp_input_data.Moments.Count.ToString();
        }
// Редактировать момент
        private void btnEditMom_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMoments.SelectedItem == null)
            {
                MessageBox.Show("Отметьте момент в таблице!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrMoments.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты параметра

            string[] AttribPar = new string[4];

            for (int iAttr = 0; iAttr < AttribPar.Length; iAttr++)
            {
                int Number_nameCOL = iAttr; // номер столбца DataGrid c именем параметра
                var _cell = new DataGridCellInfo(dgrMoments.SelectedItem, dgrMoments.Columns[Number_nameCOL]);
                var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
                AttribPar[iAttr] = cell_content.Text.Trim();
            }

            int N_Moment = Convert.ToInt32(AttribPar[0]);
            // Вызываем окно редактирования параметра элемента Словаря
            One_moment dialog = new One_moment();
            // Инициализируем значения атрибутов
            dialog.N_Moment = N_Moment;
            dialog.N_Elem = Convert.ToInt32(AttribPar[1]);
            dialog.ValueMoment = Convert.ToInt32(AttribPar[2]);
            dialog.Comm = AttribPar[3].Trim();

            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_moment newOneMoment = new defOne_moment
            {
                N_moment = vp_input_data.Moments.Count,
                N_elem = dialog.N_Elem,
                Value = dialog.ValueMoment,
                Comment = dialog.Comm
            };

            vp_input_data.Moments.Insert(N_Moment - 1, newOneMoment);
            vp_input_data.Moments.RemoveAt(N_Moment);

            dgrMoments.Items.Insert(N_Moment - 1, newOneMoment);
            dgrMoments.Items.RemoveAt(N_Moment);
            dgrMoments.SelectedIndex = N_Moment - 1;
        }
// Удаляем момент из таблицы
        private void btnDelMom_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMoments.SelectedItem == null)
            {
                MessageBox.Show("Отметьте момент в таблице!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }
            int SI = dgrMoments.SelectedIndex;

            // Извлечём из отмеченной строки DataGrid  номер момента
            string N_Moment_str;
            var _cell = new DataGridCellInfo(dgrMoments.SelectedItem, dgrMoments.Columns[0]);
            var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
            N_Moment_str = cell_content.Text.Trim();

            int N_Moment = Convert.ToInt32(N_Moment_str);
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Удалим момент № " + N_Moment_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Сформируем новую коллекцию моментов

            List<defOne_moment> newMoments = new List<defOne_moment>();
            dgrMoments.Items.Clear();
            defOne_moment newOne_Moment;

            for (int ii = 0; ii < vp_input_data.Moments.Count; ii++)
            {
                if (ii < N_Moment - 1)
                {
                    newMoments.Add(vp_input_data.Moments[ii]);
                    dgrMoments.Items.Add(vp_input_data.Moments[ii]);
                }
                else if (ii == N_Moment - 1) continue;
                else
                {
                    newOne_Moment = vp_input_data.Moments[ii];
                    newOne_Moment.N_moment = ii;
                    newMoments.Add(newOne_Moment);
                    dgrMoments.Items.Add(newOne_Moment);
                }
            } // for

            vp_input_data.Moments = newMoments;
            txtN_moments.Text = vp_input_data.Moments.Count.ToString();
        }
    }
}
