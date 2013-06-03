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
    /// Логика взаимодействия для WinDangerEdit.xaml
    /// </summary>
    public partial class WinDangerEdit : Window
    {
        public WinDangerEdit(Несчастный_случай.DataSet1 ds, //получание базы данных
            CollectionViewSource dangerViewSource,  
            Boolean IsAdd /* отметка о добавление или редактирование */)
        {
            InitializeComponent();
            ds1 = ds;
            ColDanger = dangerViewSource;
            Add = IsAdd;
        }


        private CollectionViewSource ColDanger;
        private CollectionViewSource ColCurrent; 
        private Несчастный_случай.DataSet1 ds1; //локальная переменная для базы данных
        private Boolean Add;    //локальная отметка о добавлении или редактировании



        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if ((!Add)) //при редактировании получаем выбранный элемент базы
            {
                ColCurrent = ((CollectionViewSource)(this.FindResource("dangerViewSource")));
                ColCurrent.Source = ColDanger.Source;
                ColCurrent.View.MoveCurrentToPosition(ColDanger.View.CurrentPosition); //переход на выбраную опасность 
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e) 
        /* Обработчик кнопки сохранить */
        {
            if (Add)    //При добавлении новой записи
            {
                ds1.Danger.AddDangerRow(nameTextBox.Text); //запись в базу данных новой 
            }
            ds1.SaveXml(); //сохранение изменений в базе данных
            this.Close(); //закрытие окна редактирования опасности
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        /* Обработчик кнопки отменить */
        {
            ds1.LoadXml(); //загрузка базы данных до изменений
            this.Close();   //закрытие окна редактирования опасности
        }   
    }
}
