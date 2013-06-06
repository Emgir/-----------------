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
        {
            InitializeComponent();
            ds1 = ds;
            ColPhoto = photoViewSource;
        }


        private CollectionViewSource ColPhoto;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("photoViewSource")));
            ColCurrent.Source = ColPhoto.Source;
            ColCurrent.View.MoveCurrentToPosition(ColPhoto.View.CurrentPosition);
        }


        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
