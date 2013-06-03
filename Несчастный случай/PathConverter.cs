using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NSConverter
{
    class PathConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value.ToString();
            if (path=="")
                path="C:\\Users\\Emgir\\Documents\\Visual Studio 2012\\Projects\\Несчастный случай\\Несчастный случай\\bin\\Debug\\MainPicture\\notmianfoto.png";
            else
            {
                string exepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                path = exepath + path;
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
