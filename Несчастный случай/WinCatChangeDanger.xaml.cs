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
    /// Логика взаимодействия для WinCatChangeDanger.xaml
    /// </summary>
    public partial class WinCatChangeDanger : Window
    {
        public WinCatChangeDanger
            (Несчастный_случай.DataSet1 ds,//база данных
            CollectionViewSource categoryViewSource,
            CollectionViewSource dangerViewSource)
        {
            InitializeComponent();
            ds1 = ds;
            ColCategory = categoryViewSource;
            ColDanger = dangerViewSource;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        /*Загрузчик*/
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("categoryViewSource")));
            ColCurrent.Source = ColCategory.Source;
            ColCurrent.View.MoveCurrentToPosition(ColCategory.View.CurrentPosition);
            ColCurrent1 = ((CollectionViewSource)(this.FindResource("dangerViewSource")));
            ColCurrent1.Source = ColDanger.Source;
            ColCurrent1.View.MoveCurrentToPosition(ColDanger.View.CurrentPosition);
        }


        private CollectionViewSource ColCategory;
        private CollectionViewSource ColCurrent;
        private CollectionViewSource ColDanger;
        private CollectionViewSource ColCurrent1; 
        private Несчастный_случай.DataSet1 ds1;


        private void Button_Click(object sender, RoutedEventArgs e)
        /*Обработчик кнопки сохранить*/
        {
            (((System.Data.DataRowView)((ColCategory.View).CurrentItem)).Row)["DangerID"] =
                (((System.Data.DataRowView)(dangerIDComboBox.SelectionBoxItem)).Row)["ID"];
            ds1.SaveXml();  //сохранение изменений в базе данных
            this.Close(); 
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        /*Обработчик кнопки отмена*/
        {
            ds1.LoadXml();  //загрузка базы данных до изменений         
            this.Close();   //закрытие окна редактирования опасности
        }
    }
}
