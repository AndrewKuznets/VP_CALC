using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Класс всех входных данных по валопроводу
/// </summary>
namespace VP_CALC
{
    /// <summary>
    /// Структура данных по одному элементу валопровода
    /// </summary>
   public class defOne_elem_VP
    {
        public int    N_elem { get; set; } // номер элемента
        public double Lx { get; set; } // длина элемента по оси Х
        public double D_vala { get; set;} // длина элемента по оси Х
        public double D2 { get; set; } //  диаметр облицовки
        public double D3 { get; set; } // внутренний диаметр вала на элементе
        public int N_Layers { get; set;} // число слоёв
        public string Mat1 { get; set; } // имя первого материала
        public string Mat2 { get; set; } // имя второго материала
        public string Env { get; set; } // окружающая среда
        public string Comment { get; set;}// комментарий по элементу
    }
    /// <summary>
    /// Структура данных по одной сосредоточенной силе
    /// </summary>
    public class defOne_force
    {
        public int N_force { get; set;} //номер силы
        public int N_elem { get; set; } //номер элемента приложения силы
        public double Value { get; set; } //  значение силы
        public string Env { get; set; } //  окружающая среда приложения силы
        public string Comment { get; set; }// комментарий по силе 
    }

    /// <summary>
    /// Структура данных по одному материалу
    /// </summary>
    public class defOne_mat
    {
        public int N_mat { get; set;} //номер материала
        public string Mat_name { get; set; } //имя материала
        public double Mod_upr { get; set; } //  модуль упругости
        public double Mod_sdv { get; set; } //  модуль сдвига
        public double Densi { get; set; }// плотность
    }

    /// <summary>
    /// Структура данных по одому изгибающему моменту
    /// </summary>
    public class defOne_moment
    {
        public int N_moment { get; set; } //номер момента
        public int N_elem { get; set; } //номер элемента приложения момента
        public double Value { get; set; } //  значение момента
        public string Comment { get; set; }// комментарий по моменту
    }

    /// <summary>
    /// Структура данных по одной протяжённой опоре
    /// </summary>
    public class defOne_prot_opor
    {
        public int N_prot_opor { get; set; } //номер протяжённой опоры
        public int N_elem { get; set; } //номер первого элемента над опорой
        public int Kol_elems { get; set; } // количество элементов над опорой
        public double Tg { get; set; } //  тангенс угла наклона
        public double Sm_korm { get; set; } // кормовое смещение
        public double DZ { get; set; } //  Диаметральный зазор
        public double T_upr_osn { get; set; }// Толщина упругого основания
        public string Comment { get; set; }// комментарий по опоре
    }

    /// <summary>
    /// Структура данных по одной точечной опоре
    /// </summary>
    public class defOne_dot_opor
    {
        public int N_dot_opor { get; set; } //номер точечной опоры
        public int N_elem { get; set; } //номер элемента приложения над опорой
        public double Sm_korm { get; set; } // кормовое смещение
        public string Comment { get; set; }// комментарий по опоре
    }
/// <summary>
/// Все данные по одному валопроводу!
/// </summary>
    public class VP_INPUT_DATA
    {
        public string CalcName=""; // имя расчёта
        public int N_SmOpor=0; // количество смещаемых опор
        public int N_TypeDP=0; // число типов дейдвудных подшипников
        public int N_TypeMat=0; // число типов материалов
        public string units_SYS="СГС"; // система единиц
        public bool print_Elems_VP=true; // печать элементовт валопровода
        public bool print_Sm_Elems = true; // печать смещений элементов
        public string LangPrint="Русский"; // язык печати
        public List<defOne_elem_VP> Elems_VP = new List<defOne_elem_VP>(); //элементы валопровода
        public List<defOne_force> Forces = new List<defOne_force>(); // сосредоточенные силы
        public List<defOne_moment> Moments = new List<defOne_moment>() ; // изгибающие моменты
        public List<defOne_prot_opor> Prot_opors = new List<defOne_prot_opor>(); // пртяжённые опоры
        public List<defOne_dot_opor> Dot_opors = new List<defOne_dot_opor>(); // точечные опоры
    }

/// <summary>
/// Все материалы по валопроводам
/// </summary>
    public class MATERIALS
    {
       public List<defOne_mat> All_mats = new List<defOne_mat>(); // все материалы по валопроводам
    }
}
