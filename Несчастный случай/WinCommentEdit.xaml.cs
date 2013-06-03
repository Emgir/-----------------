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
    /// Логика взаимодействия для AddComment.xaml
    /// Обработчик формы добавления и редактирования комментариев
    /// </summary>
    public partial class WinCommentEdit : Window
    {

        /*Локальные переменные используемые в форме*/
        private CollectionViewSource ColComment;
        private CollectionViewSource ColCurrent;
        private Несчастный_случай.DataSet1 ds1;
        private Boolean Add;
        private Int32 AID;


        public WinCommentEdit(Несчастный_случай.DataSet1 ds,
            CollectionViewSource CommentViewSource,
            Int32 AccidentID)
        /*обработчик при добавлении нового комментария*/
        {
            InitializeComponent();
            ds1 = ds;
            ColComment = CommentViewSource;
            Add = true;
            AID = AccidentID;
        }


        public WinCommentEdit(Несчастный_случай.DataSet1 ds,
            CollectionViewSource CommentViewSource)
        /*Обработчик при редактировании комментария*/
        {
            InitializeComponent();
            ds1 = ds;
            ColComment = CommentViewSource;
            Add = false;
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        /*Загрузчик*/
        {
            if ((!Add)) //загрузка выбранного случая при редактировании
            {
                ColCurrent = ((CollectionViewSource)(this.FindResource("CommentViewSource")));
                ColCurrent.Source = ColComment.Source;
                ColCurrent.View.MoveCurrentToPosition(ColComment.View.CurrentPosition);
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        /*Обработчик кнопки добавить*/
        {
            if (Add)
            {
                DataSet1.AccidentRow row = ds1.Accident.Single(d => d.ID == AID);
                ds1.Comment.AddCommentRow(row, textCommentTextBox.Text, fIOTextBox.Text, groupTextBox.Text, _E_MailTextBox.Text, DateTime.Today);
            }
            ds1.SaveXml();
            this.Close();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        /*Обработчик кнопки отменить*/
        {
            ds1.LoadXml();
            this.Close();
        }
    }
}
