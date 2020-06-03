using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml;



namespace VP_CALC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Создадим хранилище всех данных по валопроводу!
        public VP_INPUT_DATA vp_input_data = new VP_INPUT_DATA();
        // Создадим хранилище всех данных по материалам валопроводов!
        public MATERIALS materials = new MATERIALS();

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

            txtKol_Elems.Text = vp_input_data.Elems_VP.Count.ToString();
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
            MessageBoxResult result = MessageBox.Show("Удалим элемент валопровода № " + N_Elem_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Удалим элемент из коллекции

            List<defOne_elem_VP> newElems_VP = new List<defOne_elem_VP>();
            dgrVP_elems.Items.Clear();
            defOne_elem_VP newOne_elem_VP;

            for (int ii = 0; ii < vp_input_data.Elems_VP.Count; ii++)
            {
                if (ii < N_Elem - 1)
                {
                    newElems_VP.Add(vp_input_data.Elems_VP[ii]);
                    dgrVP_elems.Items.Add(vp_input_data.Elems_VP[ii]);
                }
                else if (ii == N_Elem - 1) continue;
                else
                {
                    newOne_elem_VP = vp_input_data.Elems_VP[ii];
                    newOne_elem_VP.N_elem = ii;
                    newElems_VP.Add(newOne_elem_VP);
                    dgrVP_elems.Items.Add(newOne_elem_VP);
                }
            } // for

            vp_input_data.Elems_VP = newElems_VP;
            txtKol_Elems.Text = vp_input_data.Elems_VP.Count.ToString();

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
                N_force = N_Force,
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
                N_moment = N_Moment,
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
        /// <summary>
        /// Добавить протяжённую опору
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProtOpor_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем окно ввода  данных по протяжённой опоре
            Prot_opor dialog = new Prot_opor();

            dialog.N_prot_opor = vp_input_data.Prot_opors.Count + 1;
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_prot_opor newOneProtOpor = new defOne_prot_opor
            {
                N_prot_opor = vp_input_data.Prot_opors.Count + 1,
                N_elem = dialog.N_Elem,
                Kol_elems = dialog.Kol_Elems,
                Tg = dialog.TG_UN,
                Sm_korm = dialog.Sm_korm,
                DZ = dialog.DZ,
                T_upr_osn = dialog.Upr_osn,
                Comment = dialog.Comm
            };

            dgrProtOpors.Items.Add(newOneProtOpor);
            vp_input_data.Prot_opors.Add(newOneProtOpor);

            txtProtOpors.Text = vp_input_data.Prot_opors.Count.ToString();
        }
        /// <summary>
        /// Исправить данные о протяжённой опоре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProtOpor_Click(object sender, RoutedEventArgs e)
        {
            if (dgrProtOpors.SelectedItem == null)
            {
                MessageBox.Show("Отметьте протяжённую опору в таблице  !",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrProtOpors.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты прот. опоры

            string[] AttribPar = new string[8];

            for (int iAttr = 0; iAttr < AttribPar.Length; iAttr++)
            {
                int Number_nameCOL = iAttr; // номер столбца DataGrid c именем параметра
                var _cell = new DataGridCellInfo(dgrProtOpors.SelectedItem, dgrProtOpors.Columns[Number_nameCOL]);
                var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
                AttribPar[iAttr] = cell_content.Text.Trim();
            }

            int N_prot_opor = Convert.ToInt32(AttribPar[0]);
            // Вызываем окно редактирования параметра элемента Словаря
            Prot_opor dialog = new Prot_opor();
            // Инициализируем значения атрибутов
            dialog.N_prot_opor = N_prot_opor;
            dialog.N_Elem = Convert.ToInt32(AttribPar[1]);
            dialog.Kol_Elems = Convert.ToInt32(AttribPar[2]);
            dialog.TG_UN = Convert.ToDouble(AttribPar[3]);
            dialog.Sm_korm = Convert.ToDouble(AttribPar[4]);
            dialog.DZ = Convert.ToDouble(AttribPar[5]);
            dialog.Upr_osn = Convert.ToDouble(AttribPar[6]);
            dialog.Comm = AttribPar[7].Trim();
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;
            // Контекст новых данных по опоре
            defOne_prot_opor newOneProtOpor = new defOne_prot_opor
            {
                N_prot_opor = N_prot_opor,
                N_elem = dialog.N_Elem,
                Kol_elems = dialog.Kol_Elems,
                Tg = dialog.TG_UN,
                Sm_korm = dialog.Sm_korm,
                DZ = dialog.DZ,
                T_upr_osn = dialog.Upr_osn,
                Comment = dialog.Comm
            };

            vp_input_data.Prot_opors.Insert(N_prot_opor - 1, newOneProtOpor);
            vp_input_data.Prot_opors.RemoveAt(N_prot_opor);

            dgrProtOpors.Items.Insert(N_prot_opor - 1, newOneProtOpor);
            dgrProtOpors.Items.RemoveAt(N_prot_opor);
            dgrProtOpors.SelectedIndex = N_prot_opor - 1;
        }
        /// <summary>
        /// Удалим прот. опору в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelProtOpor_Click(object sender, RoutedEventArgs e)
        {

            if (dgrProtOpors.SelectedItem == null)
            {
                MessageBox.Show("Отметьте силу в таблице!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }
            int SI = dgrProtOpors.SelectedIndex;

            // Извлечём из отмеченной строки DataGrid  номер силы
            string N_Prot_opor_str;
            var _cell = new DataGridCellInfo(dgrProtOpors.SelectedItem, dgrProtOpors.Columns[0]);
            var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
            N_Prot_opor_str = cell_content.Text.Trim();

            int N_Prot_opor = Convert.ToInt32(N_Prot_opor_str);
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Удалим протяжённую опору № " + N_Prot_opor_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Удалим элемент из коллекции

            List<defOne_prot_opor> newProt_opors = new List<defOne_prot_opor>();
            dgrProtOpors.Items.Clear();
            defOne_prot_opor newOne_Prot_opor;

            for (int ii = 0; ii < vp_input_data.Prot_opors.Count; ii++)
            {
                if (ii < N_Prot_opor - 1)
                {
                    newProt_opors.Add(vp_input_data.Prot_opors[ii]);
                    dgrProtOpors.Items.Add(vp_input_data.Prot_opors[ii]);
                }
                else if (ii == N_Prot_opor - 1) continue;
                else
                {
                    newOne_Prot_opor = vp_input_data.Prot_opors[ii];
                    newOne_Prot_opor.N_prot_opor = ii;
                    newProt_opors.Add(newOne_Prot_opor);
                    dgrProtOpors.Items.Add(newOne_Prot_opor);
                }
            } // for

            vp_input_data.Prot_opors = newProt_opors;
            txtProtOpors.Text = vp_input_data.Prot_opors.Count.ToString();
        }
        /// <summary>
        /// Добавление точечной опоры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDotOpor_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем окно ввода  данных по протяжённой опоре
            One_dot_opor dialog = new One_dot_opor();

            dialog.N_dot_opor = vp_input_data.Dot_opors.Count + 1;
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_dot_opor newOneDotOpor = new defOne_dot_opor
            {
                N_dot_opor = vp_input_data.Dot_opors.Count + 1,
                N_elem = dialog.N_Elem,
                Sm_korm = dialog.Sm_korm,
                Comment = dialog.Comm
            };

            dgrDotOpors.Items.Add(newOneDotOpor);
            vp_input_data.Dot_opors.Add(newOneDotOpor);

            txtN_DotOpors.Text = vp_input_data.Dot_opors.Count.ToString();
        }
        /// <summary>
        /// Исправить данные точечной опоры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDotOpor_Click(object sender, RoutedEventArgs e)
        {
            if (dgrDotOpors.SelectedItem == null)
            {
                MessageBox.Show("Отметьте точечную опору в таблице  !",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrDotOpors.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты прот. опоры

            string[] AttribPar = new string[4];

            for (int iAttr = 0; iAttr < AttribPar.Length; iAttr++)
            {
                int Number_nameCOL = iAttr; // номер столбца DataGrid c именем параметра
                var _cell = new DataGridCellInfo(dgrDotOpors.SelectedItem, dgrDotOpors.Columns[Number_nameCOL]);
                var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
                AttribPar[iAttr] = cell_content.Text.Trim();
            }

            int N_dot_opor = Convert.ToInt32(AttribPar[0]);
            // Вызываем окно редактирования параметра элемента Словаря
            One_dot_opor dialog = new One_dot_opor();
            // Инициализируем значения атрибутов
            dialog.N_dot_opor = N_dot_opor;
            dialog.N_Elem = Convert.ToInt32(AttribPar[1]);
            dialog.Sm_korm = Convert.ToDouble(AttribPar[2]);
            dialog.Comm = AttribPar[3].Trim();
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;
            // Контекст новых данных по опоре
            defOne_dot_opor newOneDotOpor = new defOne_dot_opor
            {
                N_dot_opor = N_dot_opor,
                N_elem = dialog.N_Elem,
                Sm_korm = dialog.Sm_korm,
                Comment = dialog.Comm
            };

            vp_input_data.Dot_opors.Insert(N_dot_opor - 1, newOneDotOpor);
            vp_input_data.Dot_opors.RemoveAt(N_dot_opor);

            dgrDotOpors.Items.Insert(N_dot_opor - 1, newOneDotOpor);
            dgrDotOpors.Items.RemoveAt(N_dot_opor);
            dgrDotOpors.SelectedIndex = N_dot_opor - 1;
        }
        /// <summary>
        /// Удалить точечную опору
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelDotOpor_Click(object sender, RoutedEventArgs e)
        {
            if (dgrDotOpors.SelectedItem == null)
            {
                MessageBox.Show("Отметьте точечную опору в таблице!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }
            int SI = dgrDotOpors.SelectedIndex;

            // Извлечём из отмеченной строки DataGrid  номер силы
            string N_Dot_opor_str;
            var _cell = new DataGridCellInfo(dgrDotOpors.SelectedItem, dgrDotOpors.Columns[0]);
            var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
            N_Dot_opor_str = cell_content.Text.Trim();

            int N_Dot_opor = Convert.ToInt32(N_Dot_opor_str);
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Удалим точечную опору № " + N_Dot_opor_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Удалим элемент из коллекции

            List<defOne_dot_opor> newDot_opors = new List<defOne_dot_opor>();
            dgrDotOpors.Items.Clear();
            defOne_dot_opor newOneDotOpor = new defOne_dot_opor();

            for (int ii = 0; ii < vp_input_data.Dot_opors.Count; ii++)
            {
                if (ii < N_Dot_opor - 1)
                {
                    newDot_opors.Add(vp_input_data.Dot_opors[ii]);
                    dgrDotOpors.Items.Add(vp_input_data.Dot_opors[ii]);
                }
                else if (ii == N_Dot_opor - 1) continue;
                else
                {
                    newOneDotOpor = vp_input_data.Dot_opors[ii];
                    newOneDotOpor.N_dot_opor = ii;
                    newDot_opors.Add(newOneDotOpor);
                    dgrDotOpors.Items.Add(newOneDotOpor);
                }
            } // for

            vp_input_data.Dot_opors = newDot_opors;
            txtN_DotOpors.Text = vp_input_data.Dot_opors.Count.ToString();
        }
        /// <summary>
        /// Добавить материал
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOneMat_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем окно ввода  данных по материалу
            Mater_props dialog = new Mater_props();

            dialog.N_mat = materials.All_mats.Count + 1;
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            defOne_mat newOneMat = new defOne_mat
            {
                N_mat = materials.All_mats.Count + 1,
                Mat_name = dialog.Mat_name,
                Mod_sdv = dialog.Mod_sdv,
                Mod_upr = dialog.Mod_upr,
                Densi = dialog.Densi
            };

            dgrMatsProps.Items.Add(newOneMat);
            materials.All_mats.Add(newOneMat);

            txtN_mats.Text = materials.All_mats.Count.ToString();
        }
        /// <summary>
        /// Исправить данные по материалу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditOneMat_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMatsProps.SelectedItem == null)
            {
                MessageBox.Show("Отметьте материал в таблице  !",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }

            int SI = dgrMatsProps.SelectedIndex;
            // Извлечём из отмеченной строки DataGrid  атрибуты прот. опоры

            string[] AttribPar = new string[5];

            for (int iAttr = 0; iAttr < AttribPar.Length; iAttr++)
            {
                int Number_nameCOL = iAttr; // номер столбца DataGrid c именем параметра
                var _cell = new DataGridCellInfo(dgrMatsProps.SelectedItem, dgrMatsProps.Columns[Number_nameCOL]);
                var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
                AttribPar[iAttr] = cell_content.Text.Trim();
            }

            int N_mat = Convert.ToInt32(AttribPar[0]);
            // Вызываем окно редактирования параметра элемента Словаря
            Mater_props dialog = new Mater_props();
            // Инициализируем значения атрибутов
            dialog.N_mat = N_mat;
            dialog.Mat_name = AttribPar[1].Trim();
            dialog.Mod_upr = Convert.ToDouble(AttribPar[2]);
            dialog.Mod_sdv = Convert.ToDouble(AttribPar[3]);
            dialog.Densi = Convert.ToDouble(AttribPar[4]);
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;
            // Контекст новых данных по материалу
            defOne_mat newOneMat = new defOne_mat
            {
                N_mat = N_mat,
                Mat_name = dialog.Mat_name,
                Mod_sdv = dialog.Mod_sdv,
                Mod_upr = dialog.Mod_upr,
                Densi = dialog.Densi
            };

            materials.All_mats.Insert(N_mat - 1, newOneMat);
            materials.All_mats.RemoveAt(N_mat);

            dgrMatsProps.Items.Insert(N_mat - 1, newOneMat);
            dgrMatsProps.Items.RemoveAt(N_mat);
            dgrMatsProps.SelectedIndex = N_mat - 1;
        }
        /// <summary>
        /// Удалить материала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelOneMat_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMatsProps.SelectedItem == null)
            {
                MessageBox.Show("Отметьте материал в таблице!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation); return;
            }
            int SI = dgrMatsProps.SelectedIndex;

            // Извлечём из отмеченной строки DataGrid  номер силы
            string N_Mat_str;
            var _cell = new DataGridCellInfo(dgrMatsProps.SelectedItem, dgrMatsProps.Columns[0]);
            var cell_content = _cell.Column.GetCellContent(_cell.Item) as TextBlock;
            N_Mat_str = cell_content.Text.Trim();

            int N_mat = Convert.ToInt32(N_Mat_str);
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Удалим материал № " + N_Mat_str + " ? ", "Внимание!", buttons);
            if (result == MessageBoxResult.No) return;
            // Удалим элемент из коллекции

            List<defOne_mat> newMats = new List<defOne_mat>();
            dgrMatsProps.Items.Clear();
            defOne_mat newOneMat = new defOne_mat();

            for (int ii = 0; ii < materials.All_mats.Count; ii++)
            {
                if (ii < N_mat - 1)
                {
                    newMats.Add(materials.All_mats[ii]);
                    dgrMatsProps.Items.Add(materials.All_mats[ii]);
                }
                else if (ii == N_mat - 1) continue;
                else
                {
                    newOneMat = materials.All_mats[ii];
                    newOneMat.N_mat = ii;
                    newMats.Add(newOneMat);
                    dgrMatsProps.Items.Add(newOneMat);
                }
            } // for

            materials.All_mats = newMats;
            txtN_mats.Text = materials.All_mats.Count.ToString();
        }
        // Сохранить все данные по валопроволу в XML-файле
        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            XDocument xdoc = new XDocument();
            XElement vp_calc_info = new XElement("VP_CALC_info");

            // Параметры валопровода
            XElement vp_params = new XElement("VP_PARAMS");
            XElement param = new XElement("PARAM",
                new XAttribute("code", "calc_name"),
                new XAttribute("name", "Имя расчёта"),
                new XAttribute("value", txtCalcName.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
                new XAttribute("code", "NE"),
                new XAttribute("name", "Количество элементов"),
                new XAttribute("value", txtKol_Elems.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
               new XAttribute("code", "NSO"),
               new XAttribute("name", "Количество смещаемых опор"),
               new XAttribute("value", txtN_SmOpor.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
              new XAttribute("code", "NV"),
              new XAttribute("name", "Число точечных опор валопровода"),
              new XAttribute("value", txtN_DotOpors.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
              new XAttribute("code", "NT"),
              new XAttribute("name", "Количество типов дейдвудных подшипников"),
              new XAttribute("value", txtN_TypeDP.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
             new XAttribute("code", "NMY"),
             new XAttribute("name", "Количество изгибающих моментов"),
             new XAttribute("value", txtN_moments.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
             new XAttribute("code", "NQ"),
             new XAttribute("name", "Количество сосредоточенных сил"),
             new XAttribute("value", txtN_forces.Text.Trim()));
            vp_params.Add(param);

            param = new XElement("PARAM",
            new XAttribute("code", "NTM"),
            new XAttribute("name", "Количество типов материала"),
            new XAttribute("value", txtN_TypeMat.Text.Trim()));
            vp_params.Add(param);
            vp_calc_info.Add(vp_params);

            // Добавим элементы валопровода
            if (vp_input_data.Elems_VP.Count > 0)
            {
                XElement vp_elements = new XElement("VP_ELEMENTS");
                XElement elem;
                foreach (defOne_elem_VP One_elem_VP in vp_input_data.Elems_VP)
                {
                    elem = new XElement("ELEM",
                         new XAttribute("number", One_elem_VP.N_elem.ToString()),
                         new XAttribute("Lx", One_elem_VP.Lx.ToString()),
                         new XAttribute("D_vala", One_elem_VP.D_vala.ToString()),
                         new XAttribute("D2", One_elem_VP.D2.ToString()),
                         new XAttribute("D3", One_elem_VP.D3.ToString()),
                         new XAttribute("N_Layers", One_elem_VP.N_Layers.ToString()),
                         new XAttribute("Mat1", One_elem_VP.Mat1),
                         new XAttribute("Mat2", One_elem_VP.Mat2),
                         new XAttribute("Env", One_elem_VP.Env),
                         new XAttribute("Comment", One_elem_VP.Comment));
                    vp_elements.Add(elem);
                }
                vp_calc_info.Add(vp_elements);
            }
            //<!--Сосредоточенные силы,т-->
            if (vp_input_data.Forces.Count > 0)
            {
                XElement Xpoint_forces = new XElement("point_forces");
                XElement Xforce;
                foreach (defOne_force One_force in vp_input_data.Forces)
                {
                    Xforce = new XElement("force",
                         new XAttribute("number", One_force.N_force.ToString()),
                         new XAttribute("N_elem", One_force.N_elem.ToString()),
                         new XAttribute("Value", One_force.Value.ToString()),
                         new XAttribute("Env", One_force.Env.ToString()),
                         new XAttribute("Comment", One_force.Comment));
                    Xpoint_forces.Add(Xforce);
                }
                vp_calc_info.AddAnnotation("Силы");
                vp_calc_info.Add(Xpoint_forces);
            }

            //<!--Изгибающие моменты-->
            if (vp_input_data.Forces.Count > 0)
            {
                XElement Xbending_moments = new XElement("bending_moments");
                XElement Xbend_moment;
                foreach (defOne_moment One_moment in vp_input_data.Moments)
                {
                    Xbend_moment = new XElement("bend_moment",
                         new XAttribute("number", One_moment.N_moment.ToString()),
                         new XAttribute("N_elem", One_moment.N_elem.ToString()),
                         new XAttribute("Value", One_moment.Value.ToString()),
                         new XAttribute("Comment", One_moment.Comment));
                    Xbending_moments.Add(Xbend_moment);
                }
                vp_calc_info.Add(Xbending_moments);
            }
            // < !--Протяжённые опоры-- >
            if (vp_input_data.Prot_opors.Count > 0)
            {
                XElement XProt_opors = new XElement("extent_bearings");
                XElement XOne_prot_opor;
                foreach (defOne_prot_opor One_prot_opor in vp_input_data.Prot_opors)
                {
                    XOne_prot_opor = new XElement("extent_bearing",
                         new XAttribute("number", One_prot_opor.N_prot_opor.ToString()),
                         new XAttribute("N_elem", One_prot_opor.N_elem.ToString()),
                         new XAttribute("Kol_elems", One_prot_opor.Kol_elems.ToString()),
                         new XAttribute("Tg", One_prot_opor.Tg.ToString()),
                         new XAttribute("Sm_korm", One_prot_opor.Sm_korm.ToString()),
                         new XAttribute("DZ", One_prot_opor.DZ.ToString()),
                         new XAttribute("Ss_upr", One_prot_opor.T_upr_osn.ToString()),
                         new XAttribute("Comment", One_prot_opor.Comment));
                    XProt_opors.Add(XOne_prot_opor);
                }
                vp_calc_info.Add(XProt_opors);
            }

            // < !--Точечные опоры-- >
            if (vp_input_data.Dot_opors.Count > 0)
            {
                XElement XDot_opors = new XElement("dot_bearings");
                XElement XOne_dot_opor;
                foreach (defOne_dot_opor One_dot_opor in vp_input_data.Dot_opors)
                {
                    XOne_dot_opor = new XElement("dot_bearing",
                         new XAttribute("number", One_dot_opor.N_dot_opor.ToString()),
                         new XAttribute("N_elem", One_dot_opor.N_elem.ToString()),
                         new XAttribute("Sm_korm", One_dot_opor.Sm_korm.ToString()),
                         new XAttribute("Comment", One_dot_opor.Comment));
                    XDot_opors.Add(XOne_dot_opor);
                }
                vp_calc_info.Add(XDot_opors);
            }

            // Настройки (ИИ и печать)
            string sys_units;
            if (rbtSGS.IsChecked == true) sys_units = "СГС";
            else sys_units = "СИ";

            string PrintElemVP;
            if (chbPrintElemVP.IsChecked == true) PrintElemVP = "есть";
            else PrintElemVP = "нет";

            string PrintSMElem;
            if (chbPrintSMElem.IsChecked == true) PrintSMElem = "есть";
            else PrintSMElem = "есть";

            string PrintLang;
            if (radioButtonEng.IsChecked == true) PrintLang = "Английский";
            else PrintLang = "Русский";

            XElement Xsettings = new XElement("settings",
                  new XAttribute("units", sys_units),
                  new XAttribute("VP_ELEMENTS_print", PrintElemVP),
                  new XAttribute("VP_ELEMENTS_disp_print", PrintSMElem),
                  new XAttribute("language_print", PrintLang));
            vp_calc_info.Add(Xsettings);

            xdoc.Add(vp_calc_info);
            // Сохранение файла
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
            saveFileDialog1.FileName = "VP_CALC_INPUT";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            var XMLfile = saveFileDialog1.FileName;
            xdoc.Save(XMLfile);
        }
        // Сохранить материалы во внешнем XML-файле
        private void btnMatSave_Click(object sender, RoutedEventArgs e)
        {
            if (materials.All_mats.Count > 0)
            {
                XDocument xdoc = new XDocument();
                XElement XMaterials = new XElement("Materials");
                XElement XOne_mat;
                foreach (defOne_mat One_mat in materials.All_mats)
                {
                    XOne_mat = new XElement("dot_bearing",
                         new XAttribute("number", One_mat.N_mat.ToString()),
                         new XAttribute("name", One_mat.Mat_name.Trim()),
                         new XAttribute("elastic_modulus", One_mat.Mod_upr.ToString()),
                         new XAttribute("shear_modulus", One_mat.Mod_sdv.ToString()),
                         new XAttribute("density", One_mat.Densi.ToString()));
                    XMaterials.Add(XOne_mat);
                }
                xdoc.Add(XMaterials);
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
                saveFileDialog1.FileName = "Materials";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                var XMLfile = saveFileDialog1.FileName;
                xdoc.Save(XMLfile);
            }
        }

        public static string OpenXMLFile()
        {
            string pathXMLFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.InitialDirectory = pathXMLFolder + @"\Экспертиза-МРТ";
            System.IO.Directory.CreateDirectory(pathXMLFolder);
            openFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return null;
            }

            var FileName = openFileDialog1.FileName;
            return FileName;
        }
        /// <summary>
        /// Загрузить данные по ВП из XML-файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            var XMLFileName = openFileDialog1.FileName;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(XMLFileName);

            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode VP_CALC_info = xRoot;

            foreach (XmlNode division in VP_CALC_info.ChildNodes)
            {
                // если узел - VP_PARAMS
                if (division.Name == "VP_PARAMS")
                {
                    foreach (XmlNode param in division.ChildNodes)
                    {
                        XmlNode attr = param.Attributes.GetNamedItem("code");
                        if (attr.Value == "calc_name")
                        {
                            txtCalcName.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NE")
                        {
                            txtKol_Elems.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NSO")
                        {
                            txtN_SmOpor.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NV")
                        {
                            txtN_DotOpors.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NT")
                        {
                            txtN_TypeDP.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NMY")
                        {
                            txtN_moments.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NQ")
                        {
                            txtN_forces.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                        if (attr.Value == "NTM")
                        {
                            txtN_TypeMat.Text = param.Attributes.GetNamedItem("value").Value;
                        }
                    }
                }
                // если узел - settings
                if (division.Name == "settings")
                {
                    XmlNode attr = division.Attributes.GetNamedItem("units");
                    if (attr.Value == "СГС") rbtSGS.IsChecked = true;
                    else rbtSI.IsChecked = true;

                    attr = division.Attributes.GetNamedItem("VP_ELEMENTS_print");
                    if (attr.Value == "есть") chbPrintElemVP.IsChecked = true;
                    else chbPrintElemVP.IsChecked = false;

                    attr = division.Attributes.GetNamedItem("VP_ELEMENTS_disp_print");
                    if (attr.Value == "есть") chbPrintSMElem.IsChecked = true;
                    else chbPrintSMElem.IsChecked = false;

                    attr = division.Attributes.GetNamedItem("language_print");
                    if (attr.Value == "Русский") radioButtonRus.IsChecked = true;
                    else radioButtonEng.IsChecked = true;

                }
            }
        }
/// <summary>
/// Конец работы?
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Действительно хотите закончить работу?", "",
                buttons, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            Close();
        }
    }
}
