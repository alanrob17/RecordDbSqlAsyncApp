using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Components
{
    internal static class DbDataReaderExtensions
    {
        public static T? Field<T>(this DbDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? default(T) : reader.GetFieldValue<T>(ordinal);
        }
    }
}
