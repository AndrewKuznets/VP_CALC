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
    struct defOne_elem_VP
    {
        double Lx; // длина элемента по оси Х
        double D2; //  диаметр облицовки
        double D3; // внутренний диаметр вала на элементе
        string Mat1; // имя первого материала
        string Mat2; // имя второго материала
        string Env; // окружающая среда
        string Comment;// комментарий по элементу
    }
    /// <summary>
    /// Структура данных по одной сосредоточенной силе
    /// </summary>
    struct defOne_force
    {
        int N_elem; //номер элемента приложения силы
        double Value; //  значение силы
        string Env; //  окружающая среда приложения силы
        string Comment;// комментарий по силе 
    }

    /// <summary>
    /// Структура данных по одному материалу
    /// </summary>
    struct defOne_mat
    {
        string Mat_name; //имя материала
        double Mod_upr; //  модуль упругости
        double Mod_sdv; //  модуль сдвига
        double Densi;// плотность
    }

    /// <summary>
    /// Структура данных по одому изгибающему моменту
    /// </summary>
    struct defOne_moment
    {
        int N_elem; //номер элемента приложения момента
        double Value; //  значение момента
        string Comment;// комментарий по моменту
    }

    /// <summary>
    /// Структура данных по одной протяжённой опоре
    /// </summary>
    struct defOne_prot_opor
    {
        int N_elem; //номер первого элемента над опорой
        int Kol_elems; // количество элементов над опорой
        double Tg; //  тангенс угла наклона
        double Sm_korm; // кормовое смещение
        double DZ; //  Диаметральный зазор
        double T_upr_osn;// Толщина упругого основания
        string Comment;// комментарий по опоре
    }

    /// <summary>
    /// Структура данных по одной точечной опоре
    /// </summary>
    struct defOne_dot_opor
    {
        int N_elem; //номер элемента приложения над опорой
        double Sm_korm; // кормовое смещение
        string Comment;// комментарий по опоре
    }

    partial class VP_INPUT_DATA
    {
        public int N_SmOpor; // количество смещаемых опор
        public int N_TypeDP; // число типов дейдвудных подшипников
        public int N_TypeMat; // число типов материалов
        public string units_SYS; // система единиц
        public bool print_Elems_VP; // печать элементовт валопровода
        public bool print_Sm_Elems; // печать смещений элементов
        public string LangPrint; // язык печати
        public List<defOne_elem_VP> Elems_VP = new List<defOne_elem_VP>(); //элементы валопровода
        public List<defOne_force> Forces; // сосредоточенные силы
        public List<defOne_moment> Moments; // изгибающие моменты
        public List<defOne_prot_opor> Prot_opors; // пртяжённые опорыо
        public List<defOne_dot_opor> Dot_opors; // точечные опоры
    }


}
