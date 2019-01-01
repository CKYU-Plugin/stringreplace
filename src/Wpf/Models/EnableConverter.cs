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
    public class EnableConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter.GetType() == typeof(string))
                {
                    //if (parameter.ToString() == "FW")
                    //{
                    //    if ((SRID)value == SRID.FW)
                    //    {
                    //        return true;
                    //    }
                    //}
                    if (parameter.ToString() == "BL")
                    {
                        if ((SRID)value != SRID.BL)
                        {
                            return true;
                        }
                    }
                    if (parameter.ToString() == "BR")
                    {
                        if ((SRID)value != SRID.BR)
                        {
                            return true;
                        }
                    }
                    if (parameter.ToString() == "BLBR")
                    {
                        if ((SRID)value != SRID.BL && (SRID)value != SRID.BR)
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Converter cannot convert back.");
        }

    }
}
