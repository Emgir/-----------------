using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Несчастный_случай
{
    /// <summary>
    /// Логика взаимодействия для WinAccidentShow.xaml
    /// </summary>
    public partial class WinAccidentShow : Window
    {
        public WinAccidentShow(Несчастный_случай.DataSet1 ds, CollectionViewSource dangerCategoryAccidentViewSource)
        {
            InitializeComponent();
            ds1 = ds;
            ColAccident = dangerCategoryAccidentViewSource;
            string exepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if  ((string)(((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row).ItemArray[15] != "")
            {
                TextRange range = new TextRange(RichTextBox1.Document.ContentStart, RichTextBox1.Document.ContentEnd);
                FileStream stream = new FileStream(exepath + (string)(((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row).ItemArray[15],
                  FileMode.OpenOrCreate);
                range.Load(stream, DataFormats.Rtf);
                stream.Close();
            }
            
            
        }

        private CollectionViewSource ColAccident;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("dangerCategoryAccidentViewSource")));
            ColCurrent.Source = ColAccident.Source;
            ColCurrent.View.MoveCurrentToPosition(ColAccident.View.CurrentPosition);
            double a = img1.ActualHeight;
        }

        private void Addcoment_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}
