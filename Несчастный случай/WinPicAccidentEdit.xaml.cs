using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для WinPicAccidentEdit.xaml
    /// Обрабатывает взаимодействие с формой для добавления новых фотографий
    /// </summary>
    public partial class WinPicAccidentEdit : Window
    {
        public WinPicAccidentEdit(Несчастный_случай.DataSet1 ds, CollectionViewSource accidentViewSource, Int32 CategoryID)
        {
            InitializeComponent();
            ds1 = ds;
            ColPhoto = accidentViewSource;
            AID = CategoryID;
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        /*Загрузчик*/
        {
            ColCurrent = ((CollectionViewSource)(this.FindResource("accidentViewSource")));
            ColCurrent.Source = ColPhoto.Source;
            ColCurrent.View.MoveCurrentToPosition(ColPhoto.View.CurrentPosition);
        }


        /*Локальные переменные используемые внутри формы*/
        private Несчастный_случай.DataSet1 ds1;
        private CollectionViewSource ColPhoto;
        private CollectionViewSource ColCurrent;
        private Int32 AID;


        private void Button_Click_1(object sender, RoutedEventArgs e)
        /*Обработчик кнопки добавить новые фотографии*/
        {
            string exepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = exepath + "\\MorePicture\\";
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            dlg.DefaultExt = "*.BMP;*.JPG;*.GIF";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = true;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    for (int i = 0; i < dlg.FileNames.Count(); i++)
                    {
                        string phootopath = "\\MorePicture\\" + System.IO.Path.GetFileName(dlg.FileNames[i]);
                        DataSet1.AccidentRow row = ds1.Accident.Single(d => d.ID == AID);
                        ds1.Photo.AddPhotoRow(row, phootopath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                ds1.SaveXml();
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        /*Обработчик кнопки удалить*/
        {
            if (ListBoxPhoto.SelectedItem != null)
            {
                MessageBoxResult rez = MessageBox.Show("Уверены что хотите удалить дополнительную картинку ",
                    "Внимание", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)   //защита от случайного удаления
                {
                    ((System.Data.DataRowView)ListBoxPhoto.SelectedItem).Delete(); //удаление выбранной фотографии
                    ds1.SaveXml();   //сохранение изменений в базе данных
                }
            }
            else
                MessageBox.Show("Выберите картинку", "Ошибка");
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        /*Обработчик кнопки закрыть*/
        {
            this.Close();
        }
    }
}
