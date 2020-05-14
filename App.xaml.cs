using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;


namespace VP_CALC
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
    // Преобразования типов и контроль значений
    static class kaa_convert
    {
        // Контроль строки на конвертируемость в положительное число
        public static bool is_number(string _string)
        {
            Regex re = new Regex("^[0-9]+(.[0-9]+)?$");
            return re.IsMatch(_string);
        }

        // Контроль строки на форму числового диапазона
        public static bool is_diap(string _string)
        {
            Regex re = new Regex("^\\[[0-9]+(.[0-9]+)? *- *[0-9]+(.[0-9]+)?\\]$");

            try
            {
                if (!re.IsMatch(_string)) return false;

                //MatchCollection mc = Regex.Matches(_string, " *[0-9]+(.[0-9]+)? *");
                //if (mc.Count != 2)  return false;

                string mc1 = Regex.Match(_string, "[0-9]+(.[0-9]+)? *-").Value;
                string mc2 = Regex.Match(_string, "- *[0-9]+(.[0-9]+)?").Value;
                mc1 = mc1.Substring(0, mc1.Length - 1);
                mc2 = mc2.Substring(1);

                // MessageBox.Show(mc1, "mc1");
                // MessageBox.Show(mc2, "mc2");

                double MinD = Convert.ToDouble(mc1);
                double MaxD = Convert.ToDouble(mc2);

                if (MinD < MaxD) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
