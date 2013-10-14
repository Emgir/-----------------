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
    /// Логика взаимодействия для WinZoomPic.xaml
    /// </summary>
    public partial class WinZoomPic : Window
    {
        public WinZoomPic(Несчастный_случай.DataSet1 ds, CollectionViewSource photoViewSource)
        /*Окно для показа увеличенной фотографии*/
        {
            InitializeComponent();
            ds1 = ds;
            ColPhoto = photoViewSource;
        }


        /*Локальные переменные*/
        private CollectionViewSource ColPhoto;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        /*Загрузчик*/
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("photoViewSource")));
            ColCurrent.Source = ColPhoto.Source;
            ColCurrent.View.MoveCurrentToPosition(ColPhoto.View.CurrentPosition);
        }


        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        /*Обработчик клика по картинке(картинка закроется)*/
        {
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        /*Переключение на предыдущую картинку*/
        {
            ColCurrent.View.MoveCurrentToPrevious();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        /*Переключение на следующую картинку*/
        {
            ColCurrent.View.MoveCurrentToNext();
        }
    }
}
