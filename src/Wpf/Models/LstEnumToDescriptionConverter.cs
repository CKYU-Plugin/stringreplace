using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wpf.Data;

namespace Wpf.Models
{
    public class LstEnumToDescriptionConverter : IValueConverter
    {
  
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter.GetType() == typeof(string))
                {
                    if (parameter.ToString() == "TriggerID")
                    {
                        ObservableCollection<long> TriggerID = (ObservableCollection<long>)value;

                        if (TriggerID == null) { return string.Empty; }
                        if (TriggerID.Count == 0) { return string.Empty; }

                        return String.Join(",", TriggerID.Select(s =>
                        {
                            TID _srtype = (TID)Enum.ToObject(typeof(TID), s);
                            return _srtype.ToDescription();
                        }).ToList());
                    }
                }
            }
            catch { }

            return String.Empty;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter.GetType() == typeof(string))
                {
                    if (parameter.ToString() == "TriggerID")
                    {
                        ObservableCollection<long> TriggerID = new ObservableCollection<long>();
                        value.ToString().Split(',').ToList().ForEach(f =>
                        {
                            TriggerID.Add((long)f.DescriptionToEnum<SRID>());
                        });
                    }
                }
            }
            catch { }

            throw new InvalidOperationException("Converter cannot convert back.");
        }

    }
}
