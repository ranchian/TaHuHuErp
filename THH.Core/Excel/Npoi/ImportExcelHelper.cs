using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace THH.Core.Excel.Npoi
{
    /// <summary>
    /// 导入
    /// </summary>
    public class ImportExcelHelper
    {
        /// <summary>
        /// 导入Excel
        /// </summary>
        public static List<T> GetListFromExcel<T>(Stream fileStream, string type, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            try
            {
                if (sheetIndex < 0) sheetIndex = 0;
                if (headerRowIndex < 0) headerRowIndex = 0;

                IWorkbook workbook = null;
                if (type == "XSSF")
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (type == "HSSF")
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
                if (workbook == null || workbook.NumberOfSheets == 0)
                    throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                ISheet sheet = workbook.GetSheetAt(sheetIndex);// as HSSFSheet;
                if (sheet == null) throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                IRow headerRow = sheet.GetRow(headerRowIndex);// as HSSFRow;
                if (headerRow == null || headerRow.PhysicalNumberOfCells == 0) throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                //验证标题列
                PropertyInfo[] properties = (Activator.CreateInstance<T>()).GetType().GetProperties();
                foreach (PropertyInfo property in properties) //每个属性都有对应列
                {
                    if (headerRow.Cells.FindIndex(c => property.Name.Equals(getHeadName(c.StringCellValue), StringComparison.CurrentCultureIgnoreCase)) < 0)
                        throw new Exception(string.Format("没有对应的列{0}", property.Name));
                }
                if (!startRowIndex.HasValue || startRowIndex.Value < 0) startRowIndex = sheet.FirstRowNum + headerRowIndex + 1;
                if (!endRowIndex.HasValue || endRowIndex.Value < 0) endRowIndex = sheet.LastRowNum;
                List<T> lists = new List<T>();
                for (int i = startRowIndex.Value; i <= endRowIndex; i++)
                {
                    IRow row = sheet.GetRow(i);// as HSSFRow;
                    if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;
                    T entity = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            int cellIndex = headerRow.Cells.FindIndex(c => property.Name.Equals(getHeadName(c.StringCellValue), StringComparison.CurrentCultureIgnoreCase));
                            if (cellIndex >= 0 && row.GetCell(cellIndex) != null)
                            {
                                object value = getTypeValue(property.PropertyType, row.GetCell(cellIndex));
                                property.SetValue(entity, value, null);
                            }
                        }
                        catch (Exception e)
                        {
                            string error = string.Format("[列：{0}  行：{1}] ", property.Name, i + 1) + e.Message;
                            throw new Exception(error);
                        }
                    }
                    lists.Add(entity);
                }
                return lists;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        public static List<T> GetListFromExcel2<T>(Stream fileStream, string type, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            try
            {
                if (sheetIndex < 0) sheetIndex = 0;
                if (headerRowIndex < 0) headerRowIndex = 0;

                IWorkbook workbook = null;
                if (type == "XSSF")
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (type == "HSSF")
                {
                    workbook = new HSSFWorkbook(fileStream);
                }

                if (workbook == null || workbook.NumberOfSheets == 0)
                    throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                ISheet sheet = workbook.GetSheetAt(sheetIndex);// as HSSFSheet;
                if (sheet == null) throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                IRow headerRow = sheet.GetRow(headerRowIndex);// as HSSFRow;
                if (headerRow == null || headerRow.PhysicalNumberOfCells == 0) throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                ////验证标题列
                PropertyInfo[] properties = (Activator.CreateInstance<T>()).GetType().GetProperties();
                if (!startRowIndex.HasValue || startRowIndex.Value < 0) startRowIndex = sheet.FirstRowNum + headerRowIndex + 1;
                if (!endRowIndex.HasValue || endRowIndex.Value < 0) endRowIndex = sheet.LastRowNum;
                List<T> lists = new List<T>();
                for (int i = startRowIndex.Value; i <= endRowIndex; i++)
                {
                    IRow row = sheet.GetRow(i);// as HSSFRow;
                    if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;

                    T entity = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            int cellIndex = headerRow.Cells.FindIndex(c => property.Name.Equals(getHeadName(c.StringCellValue), StringComparison.CurrentCultureIgnoreCase));
                            if (cellIndex >= 0 && row.GetCell(cellIndex) != null)
                            {
                                object value = getTypeValue(property.PropertyType, row.GetCell(cellIndex));
                                property.SetValue(entity, value, null);
                            }
                        }
                        catch (Exception e)
                        {
                            string error = string.Format("[列：{0}  行：{1}] ", property.Name, i + 1) + e.Message;
                            throw new Exception(error);
                        }
                    }
                    lists.Add(entity);
                }
                return lists;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileStream"></param>
        /// <param name="type"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headerRowIndex"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <returns></returns>
        public static List<T> GetListFromExcel3<T>(Stream fileStream, string type, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            try
            {
                if (sheetIndex < 0) sheetIndex = 0;
                if (headerRowIndex < 0) headerRowIndex = 0;

                IWorkbook workbook = null;
                if (type == "XSSF")
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (type == "HSSF")
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
                if (workbook == null || workbook.NumberOfSheets == 0)
                    throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                ISheet sheet = workbook.GetSheetAt(sheetIndex);// as HSSFSheet;
                if (sheet == null) throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                IRow headerRow = sheet.GetRow(headerRowIndex);// as HSSFRow;
                if (headerRow == null || headerRow.PhysicalNumberOfCells == 0) throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
                ////验证标题列
                PropertyInfo[] properties = (Activator.CreateInstance<T>()).GetType().GetProperties();
                if (!startRowIndex.HasValue || startRowIndex.Value < 0) startRowIndex = sheet.FirstRowNum + headerRowIndex + 1;
                if (!endRowIndex.HasValue || endRowIndex.Value < 0) endRowIndex = sheet.LastRowNum;
                List<T> lists = new List<T>();
                for (int i = startRowIndex.Value; i <= endRowIndex; i++)
                {
                    IRow row = sheet.GetRow(i);// as HSSFRow;
                    if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;

                    T entity = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            int cellIndex = headerRow.Cells.FindIndex(c => property.Name.Equals(getHeadName(c.StringCellValue), StringComparison.CurrentCultureIgnoreCase));
                            if (cellIndex >= 0 && row.GetCell(cellIndex) != null)
                            {
                                object value = getTypeValue(property.PropertyType, row.GetCell(cellIndex));
                                //特殊字符处理：2017-06-26 hfx
                                value = Regex.Replace(value.ToString(), @"(-{2,})", "—");
                                value = Regex.Replace(value.ToString(), @"(')", "’");
                                value = Regex.Replace(value.ToString(), @"(&)", "＆");
                                value = Regex.Replace(value.ToString(), @"(/)", "／");
                                property.SetValue(entity, value, null);
                            }
                        }
                        catch (Exception e)
                        {
                            string error = string.Format("[列：{0}  行：{1}] ", property.Name, i + 1) + e.Message;
                            throw new Exception(error);
                        }
                    }
                    lists.Add(entity);
                }
                return lists;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="filePath">Excel所在路径</param>
        public static List<T> GetListFromExcel<T>(string filePath, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            try
            {
                if (!System.IO.File.Exists(filePath)) throw new Exception(string.Format("文件不存在{0}", filePath));
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                {
                    if (filePath.Contains(".xlsx"))
                    {
                        return GetListFromExcel<T>(fileStream, "XSSF", sheetIndex, headerRowIndex, startRowIndex, endRowIndex);
                    }
                    else
                    {
                        return GetListFromExcel<T>(fileStream, "HSSF", sheetIndex, headerRowIndex, startRowIndex, endRowIndex);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<T> GetListFromExcel2<T>(string filePath, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            try
            {
                if (!System.IO.File.Exists(filePath)) throw new Exception(string.Format("文件不存在{0}", filePath));
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                {
                    if (filePath.Contains(".xlsx"))
                    {
                        return GetListFromExcel2<T>(fileStream, "XSSF", sheetIndex, headerRowIndex, startRowIndex, endRowIndex);
                    }
                    else
                    {
                        return GetListFromExcel2<T>(fileStream, "HSSF", sheetIndex, headerRowIndex, startRowIndex, endRowIndex);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<T> GetListFromExcelByMappingCols<T>(string filePath, Dictionary<string, string> dictColMapping, int sheetIndex = 0, int headerRowIndex = 0, int? startRowIndex = null, int? endRowIndex = null)
        {
            IWorkbook workBook = getWorkBook(filePath);
            ISheet sheet = workBook.GetSheetAt(sheetIndex);
            IRow headerRow = sheet.GetRow(headerRowIndex);
            if (headerRow == null || headerRow.PhysicalNumberOfCells == 0)
            {
                throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
            }
            //验证标题列
            PropertyInfo[] properties = (Activator.CreateInstance<T>()).GetType().GetProperties();

            if (!startRowIndex.HasValue || startRowIndex.Value < 0) startRowIndex = sheet.FirstRowNum + headerRowIndex + 1;
            if (!endRowIndex.HasValue || endRowIndex.Value < 0) endRowIndex = sheet.LastRowNum;
            List<T> lists = new List<T>();
            for (int i = startRowIndex.Value; i <= endRowIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;

                T entity = Activator.CreateInstance<T>();
                foreach (PropertyInfo property in properties) //
                {
                    try
                    {
                        if (!dictColMapping.ContainsKey(property.Name))
                        {
                            continue;
                        }
                        int cellIndex = headerRow.Cells.FindIndex(c => dictColMapping[property.Name].Equals(c.StringCellValue));
                        if (cellIndex >= 0 && row.GetCell(cellIndex) != null)
                        {
                            object value = getTypeValue(property.PropertyType, row.GetCell(cellIndex));
                            property.SetValue(entity, value, null);
                        }
                    }
                    catch (Exception e)
                    {
                        string error = string.Format("[列：{0}  行：{1}] ", dictColMapping[property.Name], i + 1) + e.Message;
                        throw new Exception(error);
                    }
                }
                lists.Add(entity);
            }
            return lists;
        }

        private static object getTypeValue(Type type, ICell value)
        {
            try
            {
                object result = null;
                string typeName = type.Name.ToLower();
                if (typeName == "nullable`1")
                {
                    if (!string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return getTypeValue(type.GetGenericArguments()[0], value);
                    }
                }
                else
                {
                    switch (typeName)
                    {

                        case "short":
                        case "int16":
                            try { result = (short)(value.NumericCellValue); }
                            catch (Exception) { result = Convert.ToInt16(value.ToString()); }
                            break;
                        case "int":
                        case "int32":
                            try { result = (int)(value.NumericCellValue); }
                            catch (Exception) { result = Convert.ToInt32(value.ToString()); }
                            break;
                        case "long":
                        case "int64":
                            try { result = (long)(value.NumericCellValue); }
                            catch (Exception) { result = Convert.ToInt64(value.ToString()); }
                            break;
                        case "decimal":
                            try { result = (decimal)(value.NumericCellValue); }
                            catch (Exception) { result = Convert.ToDecimal(value.ToString()); }
                            break;
                        case "float":
                        case "single":
                            try { result = (float)(value.NumericCellValue); }
                            catch (Exception) { result = Convert.ToSingle(value.ToString()); }
                            break;
                        case "datetime":
                            try { result = value.DateCellValue; }
                            catch (Exception)
                            {
                                try
                                {
                                    result = Convert.ToDateTime(value.ToString());
                                }
                                catch (Exception)
                                {
                                    DateTime output;
                                    bool isTrue = false;
                                    isTrue = DateTime.TryParseExact(value.ToString(), "MM/dd/yy", null, DateTimeStyles.None, out output);
                                    if (isTrue)
                                    {
                                        result = output;
                                    }

                                }
                            }
                            break;
                        case "bool":
                        case "boolean":
                            try { result = value.BooleanCellValue; }
                            catch (Exception) { result = Convert.ToBoolean(value.ToString()); }
                            break;
                        default:
                            try { result = value.StringCellValue; }
                            catch (Exception) { result = value.ToString(); }
                            break;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("导入数据格式异常", value));
            }
        }
        /// <summary>
        /// 转化为列头名，把空格等特殊字符替代为下划线
        /// </summary>
        private static string getHeadName(string cellValue)
        {
            if (string.IsNullOrWhiteSpace(cellValue)) return null;
            return cellValue.Trim().Replace(" & ", " ").Replace(" # ", " ").Replace(" #", " ").Trim()
                .Replace(' ', '_').Replace('/', '_').Replace('-', '_').Replace('&', '_').Replace('#', '_').Replace('(', '_').Replace(')', '_').Replace('（', '_').Replace('）', '_');//中文括号
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
        /// <param name="sheetName">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTableFromExcel(string excelFilePath, int sheetIndex = 0, int headerRowIndex = 0)
        {
            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                return getDataTableFromExcel(stream, sheetIndex, headerRowIndex);
            }
        }
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        private static DataTable getDataTableFromExcel(Stream excelFileStream, int sheetIndex = 0, int headerRowIndex = 0)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            HSSFSheet sheet = workbook.GetSheetAt(sheetIndex) as HSSFSheet;
            DataTable table = new DataTable();
            HSSFRow headerRow = sheet.GetRow(headerRowIndex) as HSSFRow;
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i + 1;
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + headerRowIndex + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;

                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    dataRow[j] = row.GetCell(j);
                }
                table.Rows.Add(dataRow);
            }
            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable ReadFileByImportFile(string Path)
        {
            FileStream fs = System.IO.File.OpenRead(Path);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(fs);
            //获取excel的第一个sheet
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(0);
            DataTable table = new DataTable();
            int startRowIndex = 0, endRowIndex = 0;
            //获取所有行集合
            IEnumerator rowsCount = sheet.GetRowEnumerator();
            int cellCount = 0;
            int rowCount = 0;
            while (rowsCount.MoveNext())
            {
                rowCount++;
                int temp = ((HSSFRow)rowsCount.Current).LastCellNum;
                if (temp > cellCount)
                    cellCount = temp;
            }
            //未指定结束行索引 默认最大行索引
            if (endRowIndex == 0)
                endRowIndex = rowCount;
            IEnumerator rows = sheet.GetRowEnumerator();
            //如指定起始行索引 "指针"往后移
            int count = 1;
            while (count <= startRowIndex)
            {
                rows.MoveNext();
                count++;
            }
            count -= 1;
            bool isOK = false;
            int j = -1;
            while (rows.MoveNext())
            {
                if (count <= endRowIndex)
                {
                    HSSFRow row = (HSSFRow)rows.Current;
                    //确定列数 根据表头指定 表单数据可为空 （主单和子单列数不一致）
                    if (!isOK && !(startRowIndex == 0 && endRowIndex == 0))
                    {
                        cellCount = row.LastCellNum;
                        isOK = true;
                    }
                    DataRow dataRow = table.NewRow();
                    for (int i = 0; i < cellCount; i++)
                    {

                        HSSFCell cell = (HSSFCell)row.GetCell(i);
                        if (cell != null)
                        {
                            if (count <= 0)
                            {
                                DataColumn column = new DataColumn(GetStringByType(cell));
                                table.Columns.Add(column);
                            }
                            if (j >= 0)
                            {
                                dataRow[i] = GetStringByType(cell);
                            }

                        }
                    }
                    if (j >= 0)
                        table.Rows.Add(dataRow);
                    j++;
                    count++;
                }
            }
            workbook = null;
            sheet = null;
            fs.Close();
            return table;
        }

        /// <summary>
        /// 根据单元格类型来取值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetStringByType(HSSFCell cell)
        {
            string type = "";
            switch (cell.CellType)
            {
                case CellType.Blank:
                    type = "";
                    break;
                case CellType.Numeric:
                    type = cell.ToString();
                    break;
                case CellType.String:
                    type = cell.StringCellValue;
                    break;
                default:
                    type = "";
                    break;
            }
            return type;
        }

        private static IWorkbook getWorkBook(string filePath)
        {
            using (FileStream fileStream = System.IO.File.OpenRead(filePath))
            {
                IWorkbook workbook = null;
                if (filePath.Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
                return workbook;
            }
        }

        /// <summary>
        /// 获取表头
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headerRowIndex"></param>
        /// <returns></returns>
        public static List<string> GetHeaderList(string filePath, int sheetIndex = 0, int headerRowIndex = 0)
        {
            if (sheetIndex < 0) sheetIndex = 0;
            if (headerRowIndex < 0) headerRowIndex = 0;
            IWorkbook workbook = getWorkBook(filePath);
            if (workbook == null || workbook.NumberOfSheets == 0)
            {
                throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
            }
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            {
                if (sheet == null) throw new Exception(string.Format
                    ("导入失败，无数据{0}", sheetIndex));
            }
            IRow headerRow = sheet.GetRow(headerRowIndex);// as HSSFRow;
            if (headerRow == null || headerRow.PhysicalNumberOfCells == 0)
            {
                throw new Exception(string.Format("导入失败，无数据{0}", sheetIndex));
            }
            return headerRow.Cells.Select(zw => zw.StringCellValue).ToList();
        }

        public static DataTable GetDataTableByExcel(string excelFilePath, int sheetIndex = 0, int headerRowIndex = 0)
        {
            using (FileStream stream = File.OpenRead(excelFilePath))
            {
                if (Path.GetExtension(excelFilePath).ToLower() == ".xlsx")
                    return getDaTableBy07Excel(stream, sheetIndex, headerRowIndex);
                else
                    return getDaTableBy03Excel(stream, sheetIndex, headerRowIndex);
            }
        }
        /// <summary>  
        /// 将excel导入到DataSet  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
        public static DataSet ExcelToDataSet(string filePath, bool isColumnName)
        {
            DataSet ds = null;//要返回的数据集
            FileStream fs = null;
            IWorkbook workbook = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook == null) return ds;
                    ds = new DataSet();
                    var sheetsCount = workbook.NumberOfSheets;
                    for (int s = 0; s < sheetsCount; s++)
                    {
                        var sheet = workbook.GetSheetAt(s);
                        var dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum; //总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0); //第一行  
                                int cellCount = firstRow.LastCellNum; //列数  

                                //构建datatable的列  
                                DataColumn column = null;
                                ICell cell = null;
                                if (isColumnName)
                                {
                                    startRow = 1; //如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    var row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    var dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                        ds.Tables.Add(dataTable);
                    }
                }
                return ds;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }
        private static DataTable getDaTableBy07Excel(Stream excelFileStream, int sheetIndex = 0, int headerRowIndex = 0)
        {
            DataTable table = new DataTable();
            XSSFWorkbook workbook = new XSSFWorkbook(excelFileStream);
            XSSFSheet sheet = workbook.GetSheetAt(sheetIndex) as XSSFSheet;

            XSSFRow headerRow = sheet.GetRow(headerRowIndex) as XSSFRow;    //表头行的索引
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                XSSFRow row = sheet.GetRow(i) as XSSFRow;
                if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    dataRow[j] = row.GetCell(j) != null ? row.GetCell(j) + "" : "";
                }
                table.Rows.Add(dataRow);
            }
            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
        private static DataTable getDaTableBy03Excel(Stream excelFileStream, int sheetIndex = 0, int headerRowIndex = 0)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            HSSFSheet sheet = workbook.GetSheetAt(sheetIndex) as HSSFSheet;
            DataTable table = new DataTable();

            HSSFRow headerRow = sheet.GetRow(headerRowIndex) as HSSFRow;    //表头行的索引
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                if (row == null || row.Cells == null || row.Cells.TrueForAll(p => string.IsNullOrWhiteSpace(p.ToString()))) break;
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    dataRow[j] = row.GetCell(j) != null ? row.GetCell(j) + "" : "";
                }
                table.Rows.Add(dataRow);
            }
            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
    }
}
