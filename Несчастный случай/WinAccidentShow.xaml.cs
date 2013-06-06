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
    /// 
    /// </summary>
    public partial class WinAccidentShow : Window
    {


        public WinAccidentShow(Несчастный_случай.DataSet1 ds, CollectionViewSource dangerCategoryAccidentViewSource)
        /*Загрузка выбранного случая для его детального просмотра*/
        {
            InitializeComponent();
            ds1 = ds;
            ColAccident = dangerCategoryAccidentViewSource;
            string exepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            /*проверка наличая адреса документа с описанием и его загрузка*/
            if  ((string)(((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row).ItemArray[15] != "")
            {
                TextRange range = new TextRange(RichTextBox1.Document.ContentStart, RichTextBox1.Document.ContentEnd);
                FileStream stream = new FileStream(exepath + (string)(((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row).ItemArray[15],
                  FileMode.OpenOrCreate);
                range.Load(stream, DataFormats.Rtf);
                stream.Close();
            }  
        }


        /*Локальные переменные*/
        private CollectionViewSource ColAccident;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        /*Загрузчик*/
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("accidentViewSource")));
            ColCurrent.Source = ColAccident.Source;
            ColCurrent.View.MoveCurrentToPosition(ColAccident.View.CurrentPosition);
        }


        private void Addcoment_Click_1(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "добавить" для listbox  
         добавляет новую запись в таблицу Comment */
        {
            WinCommentEdit w = new WinCommentEdit(ds1, ((CollectionViewSource)(this.FindResource("accidentCommentViewSource"))),
              (int)(((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row).ItemArray[0]); //инициализация окна редактора
            w.ShowDialog();  //вызов окна редактора
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "редактировать" для listbox  
         добавляет новую запись в таблицу Comment */
        {
            WinCommentEdit w = new WinCommentEdit(ds1, ((CollectionViewSource)(this.FindResource("accidentCommentViewSource"))));
            w.ShowDialog();
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        /* Обработчик нажатия кнопки "удалить" для listbox 
     удаляет выбраную запись из таблицы Comment */
        {
            if (lb.SelectedItem != null) //Обработчик корректности выбора случая
            {
                MessageBoxResult rez = MessageBox.Show("Уверены что хотите удалить комментарий - "
                    + ((System.Data.DataRowView)lb.SelectedItem).Row["ID"],
                    "Внимание", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)   //защита от случайного удаления
                {
                    ((System.Data.DataRowView)lb.SelectedItem).Delete(); //удаление случая
                    ds1.SaveXml(); //сохранение изменений в базе данных
                }
            }
            else
                MessageBox.Show("Выберите случай", "Ошибка"); 
        }


        private void img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WinZoomMainPic w = new WinZoomMainPic(ds1, ColAccident);
            w.ShowDialog();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WinZoomPic w = new WinZoomPic(ds1, ((CollectionViewSource)(this.FindResource("accidentPhotoViewSource"))));
            w.ShowDialog();
        }

    }
}
