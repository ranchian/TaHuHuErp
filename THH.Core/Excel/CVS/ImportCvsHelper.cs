using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace THH.Core.Excel.CVS
{
    public class ImportCvsHelper
    {
        /// <summary>
        /// 导入Excel
        /// </summary>
        public static DataTable GetListFromCvs(string filePath, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            DataTable dt = new DataTable();
            try
            {
                FileStream fsStream = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                using (fsStream)
                {
                    using (StreamReader input = new StreamReader(fsStream, Encoding.GetEncoding("shift_jis")))
                    {
                        using (CsvReader csv = new CsvReader(input, false))
                        {

                            int columnCount = csv.FieldCount;
                            string[] heders = csv.First();
                            for (int i = 0; i < columnCount; i++)
                            {
                                dt.Columns.Add(heders[i].ToString().Replace("#", "").Replace(" ", "").Replace("$", ""));
                            }

                            while (csv.ReadNextRecord())
                            {
                                try
                                {
                                    DataRow dr = dt.NewRow();
                                    for (int i = 0; i < columnCount; i++)
                                    {
                                        if (!string.IsNullOrWhiteSpace(csv[i]))
                                        {
                                            dr[i] = csv[i].Replace("$", "");
                                        }
                                    }
                                    dt.Rows.Add(dr);
                                }
                                catch
                                {

                                }
                            }

                        }

                    }
                }
            }
            catch
            {

            }
            return dt;
        }

        /// <summary>  
        /// DataTable转化为List集合  
        /// </summary>  
        /// <typeparam name="T">实体对象</typeparam>  
        /// <param name="dt">datatable表</param>  
        /// <param name="isStoreDB">是否存入数据库datetime字段，date字段没事，取出不用判断</param>  
        /// <returns>返回list集合</returns>  
        public static List<T> TableToList<T>(DataTable dt, bool isStoreDB = true)
        {
            try
            {


                List<T> list = new List<T>();
                Type type = typeof(T);
                List<string> listColums = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    PropertyInfo[] pArray = type.GetProperties(); //集合属性数组  
                    T entity = Activator.CreateInstance<T>(); //新建对象实例  
                    foreach (PropertyInfo p in pArray)
                    {
                        if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                        {
                            continue;  //DataTable列中不存在集合属性或者字段内容为空则，跳出循环，进行下个循环  
                        }
                        if (isStoreDB && p.PropertyType == typeof(DateTime) && Convert.ToDateTime(row[p.Name]) < Convert.ToDateTime("1753-01-01"))
                        {
                            continue;
                        }
                        try
                        {
                            var obj = Convert.ChangeType(row[p.Name], p.PropertyType);//类型强转，将table字段类型转为集合字段类型  
                            p.SetValue(entity, obj, null);
                        }
                        catch (Exception)
                        {
                            // throw;  
                        }

                    }
                    list.Add(entity);
                }
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
