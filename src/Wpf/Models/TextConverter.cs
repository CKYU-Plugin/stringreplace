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
    public class TextConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter.GetType() == typeof(string))
                {
                    if (parameter.ToString() == "Value")
                    {
                        if ((SRID)value == SRID.FS)
                        {
                            return "在信息前置入:";
                        }
                        if ((SRID)value == SRID.LS)
                        {
                            return "在信息后置入:";
                        }
                        if ((SRID)value == SRID.IP)
                        {
                            return "在词条前插入:";
                        }
                        if ((SRID)value == SRID.IC)
                        {
                            return "在词条后加入:";
                        }
                        if ((SRID)value == SRID.RP)
                        {
                            return "取代成:";
                        }
                        if ((SRID)value == SRID.BR)
                        {
                            return "跳出";
                        }
                        if ((SRID)value == SRID.BL)
                        {
                            return "拦截";
                        }
                        //if ((SRID)value == SRID.FW)
                        //{
                        //    return "转发內容:";
                        //}

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
