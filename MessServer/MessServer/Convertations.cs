using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessServer
{
    static class Convertations
    {
        public static string DataReaderToString(SqlDataReader data)
        {
            string result = "";
            while(data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                    result = result + data[i].ToString() + ":";
                result += ";";
            }
            return result;
        }
    }
}
