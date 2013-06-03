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

namespace Несчастный_случай
{
    /// <summary>
    /// Логика взаимодействия для WinCatEdit.xaml
    /// </summary>
    public partial class WinCatEdit : Window
    {
        public WinCatEdit       //при добавлении
            (Несчастный_случай.DataSet1 ds, //база данных
            CollectionViewSource dangerCategoryViewSource, 
            Int32 DangerID /* id матернской опасности */)
        {
            InitializeComponent();
            ds1 = ds;
            ColCategory = dangerCategoryViewSource;
            Add = true;
            DID = DangerID;

        }
        public WinCatEdit //при редактировании
            (Несчастный_случай.DataSet1 ds,//база данных
            CollectionViewSource dangerCategoryViewSource)
        {
            InitializeComponent();
            ds1 = ds;
            ColCategory = dangerCategoryViewSource;
            Add = false;

        }


        private CollectionViewSource ColCategory;
        private CollectionViewSource ColCurrent; //локальная копия содержимого listbox
        private Несчастный_случай.DataSet1 ds1; //локальная переменная для базы данных
        private Boolean Add; //локальная отметка о добавлении или редактировании
        private Int32 DID; //id матернской опасности



        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        /* Загрузчик */
        {
            if ((!Add))//при редактировании получаем выбранный элемент базы
            {
                ColCurrent = ((CollectionViewSource)(this.FindResource("dangerCategoryViewSource")));
                ColCurrent.Source = ColCategory.Source;
                ColCurrent.View.MoveCurrentToPosition(ColCategory.View.CurrentPosition);
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        /* Обработчик кнопки сохранить */
        {
            if (Add)//При добавлении новой записи
            {
                DataSet1.DangerRow row = ds1.Danger.Single(d => d.ID == DID); //получение строки опасности
                ds1.Category.AddCategoryRow(row, nameTextBox.Text); //запись в базу данных новой 
            }
            ds1.SaveXml();  //сохранение изменений в базе данных
            this.Close();   //закрытие окна редактирования опасности
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        /* Обработчик кнопки отменить */
        {
            ds1.LoadXml();  //загрузка базы данных до изменений         
            this.Close();   //закрытие окна редактирования опасности
        }
    }
}
