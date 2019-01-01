using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wpf.Data;

namespace Wpf.Models
{
    public class TriggerConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            //if (parameter.GetType() == typeof(string))
            //{
            //    ObservableCollection<int> TriggerID = (ObservableCollection<int>)value;
            //    if (TriggerID.Contains((int) parameter.ToString().DescriptionToEnum<TID>()) )
            //    {

            //        return true;
            //    }
            //    else
            //    {
            //    //    TriggerID.Remove((int)parameter.ToString().DescriptionToEnum<TID>());
            //        return false;
            //    }
            //}
            return true;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {

            throw new InvalidOperationException("Converter cannot convert back.");
        }
    }

    public class TIDConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {

                ObservableCollection<int> TID = (ObservableCollection<int>)value;
                if (TID.Contains((int)parameter))
                {

                    return true;
                }
                else
                {
                    return false;
                }
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {

            throw new InvalidOperationException("Converter cannot convert back.");
        }
    }

}
