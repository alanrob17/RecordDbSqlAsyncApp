using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Components
{
    internal class DataConvert
    {
        public static T ConvertTo<T>(object value, object defaultValue) where T : struct
        {
            if (value == DBNull.Value)
            {
                return (T)defaultValue;
            }
            else
            {
                return (T)value;
            }
        }

        public static DateTime HtmlDateToDotNet(string dateValue)
        {
            var value = DateTime.Now;
            string temp = string.Empty;

            if (!DateTime.TryParse(dateValue, out value))
            {
                // Assume it is a javascript date
                temp = Encoding.Default.GetString(Encoding.ASCII.GetBytes(dateValue)).Replace("?", string.Empty);

                if (!DateTime.TryParse(temp, out value))
                {
                    value = DateTime.MinValue;
                }
            }

            return value;
        }
    }
}
