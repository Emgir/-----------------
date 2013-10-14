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
    /// Логика взаимодействия для WinZoomMainPic.xaml
    /// </summary>
    public partial class WinZoomMainPic : Window
    {
        public WinZoomMainPic(Несчастный_случай.DataSet1 ds, CollectionViewSource accidentViewSource)
        {
            InitializeComponent();
            ds1 = ds;
            ColAccident = accidentViewSource;
        }


        private CollectionViewSource ColAccident;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        /*Загрузчик*/
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("accidentViewSource")));
            ColCurrent.Source = ColAccident.Source;
            ColCurrent.View.MoveCurrentToPosition(ColAccident.View.CurrentPosition);
        }


        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        /*Обработчик клика на картинку*/
        {
            this.Close();
        }
    }
}
