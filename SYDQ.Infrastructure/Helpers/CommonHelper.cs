using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Helpers
{
    public class CommonHelper
    {
        public static void LogWriter(LogWriterType logWriterType, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Content can not be empty.");
            }

            string typeFlagName = logWriterType.ToString();
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\SYDQ_" + typeFlagName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileFullName = Path.Combine(folderPath, fileName);
            try
            {
                using (StreamWriter writer = new StreamWriter(fileFullName, true))
                {
                    StringBuilder fullContent = new StringBuilder();
                    fullContent.AppendLine("---------- start ----------");
                    fullContent.Append("--").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).AppendLine();
                    fullContent.AppendLine(content);
                    fullContent.AppendLine("----------  end  ----------");

                    writer.WriteLine(fullContent.ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        public static List<T> ToList<T>(DataTable data)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            for (int i = 1; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];
                T t = Activator.CreateInstance<T>();
                var props = type.GetProperties();
                foreach (var pi in props)
                {
                    pi.SetValue(t, Convert.ChangeType(row[pi.Name], pi.PropertyType), null);
                }
                list.Add(t);
            }

            return list;
        }

        public static DataTable ToDataTable<T>(List<T> list)
        {
            Type type = typeof(T);
            var tColumnAttr = (ColumnAttribute)Attribute.GetCustomAttribute(type, typeof(ColumnAttribute));
            DataTable data = new DataTable(tColumnAttr.Description);
            var props = type.GetProperties();

            foreach (var pi in props)
            {
                data.Columns.Add(pi.Name);
            }

            DataRow firstRow = data.NewRow();
            data.Rows.Add(firstRow);
            foreach (var pi in props)
            {
                ColumnAttribute attr = (ColumnAttribute)Attribute.GetCustomAttribute(pi, typeof(ColumnAttribute));
                if (attr != null)
                {
                    firstRow[pi.Name] = attr.Description;
                }
                else
                {
                    firstRow[pi.Name] = pi.Name;
                }
            }

            foreach (var t in list)
            {
                DataRow dataRow = data.NewRow();
                data.Rows.Add(dataRow);
                foreach (var pi in props)
                {
                    dataRow[pi.Name] = pi.GetValue(t, null);
                }
            }

            return data;
        }

        public static string Truncated(string str, int Length)
        {
            if (str.Length > Length)
            {
                return str.Substring(0, Length);
            }
            return str;
        }

        public static string RndNum(int VcodeNum)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9".Split(new char[] { ',' });
            string str2 = "";
            int num = -1;
            Random random = new Random();
            for (int i = 1; i < (VcodeNum + 1); i++)
            {
                if (num != -1)
                {
                    random = new Random((i * num) * ((int)DateTime.Now.Ticks));
                }
                int index = random.Next(9);
                if ((num != -1) && (num == index))
                {
                    return RndNum(VcodeNum);
                }
                num = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }

        public static int Length(string str)
        {
            int num = 0;
            foreach (char ch in str)
            {
                if (ch > '\x007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        public static bool ValidateNumber(decimal number, int precision, int scale)
        {
            if ((precision == 0) && (scale == 0))
            {
                return false;
            }
            string numberString = number.ToString();
            string pattern = @"(^\d{1," + (precision - scale) + "}";

            if (scale > 0)
            {

                pattern += @"\.\d{0," + scale + "}$)|" + pattern;

            }

            pattern += "$)";
            return Regex.IsMatch(numberString, pattern);
        }

        public static void DeleteEmptyRows(DataSet uploadDS)
        {
            for (int tableIndex = 0; tableIndex < uploadDS.Tables.Count; tableIndex++)
            {
                DataTable dtUpload = uploadDS.Tables[tableIndex];
                for (int i = 0; i < dtUpload.Rows.Count; i++)
                {
                    DataRow dwUpload = dtUpload.Rows[i];
                    bool flag = true;
                    for (int j = 0; j < dwUpload.ItemArray.Length; j++)
                    {
                        if (!dwUpload.IsNull(j) && !string.IsNullOrEmpty(dwUpload.ItemArray[j].ToString().Trim()))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        dtUpload.Rows.Remove(dwUpload);
                        i--;
                    }
                }
            }
        }
    }
}
