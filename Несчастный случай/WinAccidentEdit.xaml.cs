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
    /// Логика взаимодействия для WinAccidentEdit.xaml
    /// Обработчик дейсвий на форму добавления случая
    /// </summary>
    public partial class WinAccidentEdit : Window
    {
        public WinAccidentEdit //добавление нового случая
            (Несчастный_случай.DataSet1 ds, //база данных
            CollectionViewSource dangerCategoryAccidentViewSource, 
            Int32 CategoryID)//id материнской категории
        {
            InitializeComponent();
            ds1 = ds;
            ColAccident = dangerCategoryAccidentViewSource;
            Add = true;
            CID = CategoryID;
        }


        public WinAccidentEdit //редактирование выбранного случая
            (Несчастный_случай.DataSet1 ds, //база данных
            CollectionViewSource dangerCategoryAccidentViewSource)
        {
            InitializeComponent();
            ds1 = ds;
            ColAccident = dangerCategoryAccidentViewSource;
            Add = false;
        }


        private CollectionViewSource ColAccident;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;
        private Boolean Add; //отметка о добавлении
        private Int32 CID; //id материнской категории
        private string photopath; //путь к главной фотографии
        private string docpath; //путь к описанию



        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        /* Загрузчик */
        {
            if ((!Add)) //загрузка выбранного случая при редактировании
            {
                ColCurrent = ((CollectionViewSource)(this.FindResource("AccidentViewSource")));
                ColCurrent.Source = ColAccident.Source;
                ColCurrent.View.MoveCurrentToPosition(ColAccident.View.CurrentPosition);
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        /* Обработчик кнопки сохранить */
        {
            if (Add) //при добавлении новой записи
            {
                if (docpath == null)
                    docpath = "";
                DataSet1.CategoryRow row = ds1.Category.Single(d => d.ID == CID); //получение материнской строки
                ds1.Accident.AddAccidentRow(nameTextBox.Text, descriptionTextBox.Text, DateTime.Today, incidentDataDatePicker.DisplayDate,
genderComboBox.Text, socialStatusTextBox.Text, educationComboBox.Text,resultTextBox.Text,isCrimeComboBox.Text,placeIncidentTextBox.Text,
conditionsComboBox.Text,sourceTextBox.Text,row,photopath,docpath,Int32.Parse(ageTextBox.Text)); //добавление нового случая в базу данных

            }
            if (!Add) //при редактировании
            {
                if (photopath!=null)
                {
                    
                }
            }
            ds1.SaveXml(); //созранение изменений базы данных
            this.Close(); // закрытие редактора
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        /* обработчик кнопки отмена */
        {
            ds1.LoadXml(); //загрузка первоначальной базы данных
            this.Close();  //закрытие редактора
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        /* Обработчик кнопки Дополнительные картинки  */
        {
            WinPicAccidentEdit w = new WinPicAccidentEdit(ds1, ColCurrent,
                (int)(((System.Data.DataRowView)((ColAccident.View).CurrentItem)).Row).ItemArray[0]);
            w.ShowDialog();
        }


        private void picturePathButton_Click_1(object sender, RoutedEventArgs e) 
        /*загрузка главной фотографии*/
        {
            OpenFileDialog dlg = new OpenFileDialog();  //создание экземпляра класса для отрытия файла
            dlg.InitialDirectory = "c:\\";      //определение начальной директории
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";  //ограничения для фильтра
            dlg.DefaultExt = "*.BMP;*.JPG;*.GIF";
            dlg.FilterIndex = 2 ;
            dlg.RestoreDirectory = true ;
            dlg.Multiselect = false;  //выбор одного файла
            Nullable<bool> result = dlg.ShowDialog(); //проверка результативности открытия файла

            if (result == true)
            {
                try
                {
                    string exepath= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location); //путь до программы
                    File.Copy(dlg.FileName, exepath + "\\MainPicture\\" + System.IO.Path.GetFileName(dlg.FileName),true); // копирование фото в специальную папку
                    photopath = "\\MainPicture\\" + System.IO.Path.GetFileName(dlg.FileName); //относительный путь к фотографии
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        private void docPathButton_Click_1(object sender, RoutedEventArgs e)
        /*загрузка файла с описанием случая*/
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Text Files(*.RTF)|*.RTF";
            dlg.DefaultExt = "*.RTF";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = false;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string exepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    File.Copy(dlg.FileName, exepath + "\\Doc\\" + System.IO.Path.GetFileName(dlg.FileName), true);
                    docpath = "\\Doc\\" + System.IO.Path.GetFileName(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

    }
}
