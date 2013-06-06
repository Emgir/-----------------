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
    /// Логика взаимодействия для WinAccidentChangeDander.xaml
    /// </summary>
    public partial class WinAccidentChangeDander : Window
    {
        public WinAccidentChangeDander(Несчастный_случай.DataSet1 ds,//база данных
            CollectionViewSource accidentViewSource,
            CollectionViewSource categoryViewSource)
        {
            InitializeComponent();
            ds1 = ds;
            ColAccident = accidentViewSource;
            ColCategory = categoryViewSource;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("accidentViewSource")));
            ColCurrent.Source = ColAccident.Source;
            ColCurrent.View.MoveCurrentToPosition(ColAccident.View.CurrentPosition);
            ColCurrent1 = ((CollectionViewSource)(this.FindResource("categoryViewSource")));
            ColCurrent1.Source = ColCategory.Source;
            ColCurrent1.View.MoveCurrentToPosition(ColCategory.View.CurrentPosition);
        }


        private CollectionViewSource ColAccident;
        private CollectionViewSource ColCurrent;
        private CollectionViewSource ColCategory;
        private CollectionViewSource ColCurrent1;
        private Несчастный_случай.DataSet1 ds1;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row)["CategoryID"] =
                (((System.Data.DataRowView)(categoryIDComboBox.SelectionBoxItem)).Row)["ID"];
            ds1.SaveXml();  //сохранение изменений в базе данных
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ds1.LoadXml();  //загрузка базы данных до изменений         
            this.Close();   //закрытие окна редактирования опасности
        }
    }
}
