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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Несчастный_случай
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private Несчастный_случай.DataSet1 ds; //Объявление переменной для базы данных


        private void Window_Loaded_1(object sender, RoutedEventArgs e) 
        /* Загрузка быза данных */
        {
            ds = ((Несчастный_случай.DataSet1)(this.FindResource("dataSet1"))); 
            ds.LoadXml(); //загрузка базы данных
        }
       

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "добавить" для listbox 1 
         добавляет новую запись в таблицу danger */
        {
            WinDangerEdit w = new WinDangerEdit(ds, ((CollectionViewSource)(this.FindResource("dangerViewSource"))), true); //инициализация окна редактора
            w.ShowDialog(); //вызов окна редактора
        }        


        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        /* Обработчик нажатия кнопки "удалить" для listbox 1
         удаляет выбраную запись из таблицы danger */
        {
            if (lb1.SelectedItem!=null)
            {
                MessageBoxResult rez = MessageBox.Show("Уверены что хотите удалить категорию - "
                    +((System.Data.DataRowView)lb1.SelectedItem).Row["Name"],
                    "Внимание", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)   //защита от случайного удаления
                {
                    Int32 dangerID = (int)((System.Data.DataRowView)lb1.SelectedItem).Row["ID"]; //получение id выбраной опасности
                    String filter = "DangerID = " + dangerID.ToString();  //переменная для фильтрации категой опасности

                    if (dangerID != 9999) //нельзя удалять "пустую" опасность, тк в нее записывуются категории удаленных опасностей
                    {
                        foreach (DataRow item in ds.Category.Select(filter)) //перевод категорий удаляемой опасности в "пустую" опасность
                            item["DangerID"] = 9999;
                        ((System.Data.DataRowView)lb1.SelectedItem).Delete(); //удаление выброной опасности
                    }
                    else            //обработка исключения
                        MessageBox.Show("Ошибка", "Нельзя удалить пустую категорию");
                    ds.SaveXml();   //сохранение изменений в базе данных
                }                
            }
            else
                MessageBox.Show("Выберите опасность", "Ошибка");
        }


        private void lb1_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        /* Обработчик двойного клика по элементу listbox 1
         редактирует выбраный элемент listbox*/
        {
            WinDangerEdit w = new WinDangerEdit(ds, ((CollectionViewSource)(this.FindResource("dangerViewSource"))), false); //инициализация окна редактора
            w.ShowDialog();        //вызов окна редактора
        }


        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "добавить" для listbox 2 
         добавляет новую запись в таблицу category */
        {
            if (this.FindResource("dangerCategoryViewSource") != null && lb1.SelectedItem!=null)  //Обработчик корректности выбора категории
            {
                    WinCatEdit w = new WinCatEdit(ds, ((CollectionViewSource)(this.FindResource("dangerCategoryViewSource"))),
                    (int)((System.Data.DataRowView)lb1.SelectedItem).Row["ID"]); //инициализация окна редактора
                    w.ShowDialog(); //вызов окна редактора               
            }
            else
                MessageBox.Show("Выберите опасность", "Ошибка");                                  
        }


        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "Корректировать" для listbox 2 
        изменяет опасность категории */
        {
            if (this.FindResource("dangerCategoryViewSource") != null && lb1.SelectedItem != null)  //Обработчик корректности выбора категории
            {
                WinCatChangeDanger w = new WinCatChangeDanger(ds,
                ((CollectionViewSource)(this.FindResource("dangerCategoryViewSource"))),
                ((CollectionViewSource)(this.FindResource("dangerViewSource"))));
                w.ShowDialog(); //вызов окна редактора               
            }
            else
                MessageBox.Show("Выберите опасность", "Ошибка"); 
        }


        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        /* Обработчик нажатия кнопки "удалить" для listbox 2
         удаляет выбраную запись из таблицы category */
        {
            if (lb2.SelectedItem!=null) //проверка выбора категории
            {
                 MessageBoxResult rez = MessageBox.Show("Уверены что хотите удалить категорию - "
                    +((System.Data.DataRowView)lb2.SelectedItem).Row["Name"],
                    "Внимание", MessageBoxButton.YesNo);
                 if (rez == MessageBoxResult.Yes)   //защита от случайного удаления
                 {
                     Int32 categoryID = (int)((System.Data.DataRowView)lb2.SelectedItem).Row["ID"]; //получение id выбраной категории
                     String filter = "CategoryID = " + categoryID.ToString(); //переменная для фильтрации категой опасности

                     if (categoryID != 9999) //нельзя удалять "пустую" категорию, тк в нее записывуются категории удаленных опасностей
                     {
                         foreach (DataRow item in ds.Accident.Select(filter)) //перевод случаев удаляемой опасности в "пустую" категорию
                             item["CategoryID"] = 9999;
                         ((System.Data.DataRowView)lb2.SelectedItem).Delete();//удаление выброной опасности
                     }
                     else                //обработка исключения
                         MessageBox.Show("Нельзя удалить пустую категорию", "Ошибка");
                     ds.SaveXml(); //сохранение изменений в базе данных
                 }               
            }
            else
                MessageBox.Show("Выберите категорию", "Ошибка");   
        }


        private void lb2_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        /* Обработчик двойного клика по элементу listbox 2
         редактирует выбраный элемент listbox*/
        {
            WinCatEdit w = new WinCatEdit(ds, 
                ((CollectionViewSource)(this.FindResource("dangerCategoryViewSource")))); //инициализация окна редактора
            w.ShowDialog(); //вызов окна редактора
        }


        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "добавить" для listbox 3 
         добавляет новую запись в таблицу accident */
        {
            if (this.FindResource("dangerCategoryAccidentViewSource")!=null &&
                lb2.SelectedItem!=null) //Обработчик корректности выбора категории
            {
                WinAccidentEdit w = new WinAccidentEdit(ds, ((CollectionViewSource)(this.FindResource("dangerCategoryAccidentViewSource"))),
                  (int)((System.Data.DataRowView)lb2.SelectedItem).Row["ID"]); //инициализация окна редактора
                w.ShowDialog();  //вызов окна редактора
            }
            else
                MessageBox.Show("Выберите категорию", "Ошибка");             
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        /* Обработчик нажатия  кнопки "Корректировать" для listbox 3 
         изменяет категорию "несчастного случая" */
        {
            if (this.FindResource("dangerCategoryAccidentViewSource") != null && 
                lb2.SelectedItem != null)  //Обработчик корректности выбора категории
            {
                WinAccidentChangeDander w = new WinAccidentChangeDander(ds,
                ((CollectionViewSource)(this.FindResource("dangerCategoryAccidentViewSource"))),
                ((CollectionViewSource)(this.FindResource("categoryViewSource"))));
                w.ShowDialog(); //вызов окна редактора               
            }
            else
                MessageBox.Show("Выберите опасность", "Ошибка"); 
        }


        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        /* Обработчик нажатия кнопки "удалить" для listbox 3
         удаляет выбраную запись из таблицы accident */
        {
            if (lb3.SelectedItem!=null) //Обработчик корректности выбора случая
            {
                MessageBoxResult rez = MessageBox.Show("Уверены что хотите удалить категорию - "
                    + ((System.Data.DataRowView)lb3.SelectedItem).Row["Name"],
                    "Внимание", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)   //защита от случайного удаления
                {
                    ((System.Data.DataRowView)lb3.SelectedItem).Delete(); //удаление случая
                    ds.SaveXml(); //сохранение изменений в базе данных
                }                
            }
            else
                MessageBox.Show("Выберите случай", "Ошибка"); 
        }


        private void lb3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        /* Обработчик двойного клика по элементу listbox 3
         редактирует выбраный элемент listbox*/
        {
            WinAccidentEdit w = new WinAccidentEdit(ds,
                ((CollectionViewSource)(this.FindResource("dangerCategoryAccidentViewSource")))); //инициализация окна редактора
            w.ShowDialog(); //вызов окна редактора
        }


        private void ShowButton_Click_1(object sender, RoutedEventArgs e)
        /* Обработчик кнопки показать подробности
         показывает подробно случай*/
        {
            if (lb3.SelectedItem != null) //Обработчик корректности выбора случая
            {
                WinAccidentShow w = new WinAccidentShow(ds, 
                    ((CollectionViewSource)(this.FindResource("dangerCategoryAccidentViewSource"))));//инициализация окна прсмотра
                w.ShowDialog();//открытие окна просмотра
            }
            else
                MessageBox.Show("Выберите случай!", "Ошибка");
        }

        private void lb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        } 
    }
}
