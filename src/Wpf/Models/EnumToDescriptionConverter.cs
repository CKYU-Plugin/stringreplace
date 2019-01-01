using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wpf.Data;

namespace Wpf.Models
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter.GetType() == typeof(string))
            {
                if (parameter.ToString() == "TypeId")
                {
                    SRID _srtype = (SRID)Enum.ToObject(typeof(SRID), value);
                    return _srtype.ToDescription();
                }
            }
            return String.Empty;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter.GetType() == typeof(string))
            {
                if (parameter.ToString() == "TypeId")
                {
                    return value.ToString().DescriptionToEnum<SRID>();
                }
            }
            throw new InvalidOperationException("Converter cannot convert back.");
        }

    }


}

