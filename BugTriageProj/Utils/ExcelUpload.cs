using DataAccess.Models;
using DataAccess.Models.StronglyTypes;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BugTriage.Utils
{
    public class ExcelUpload<T>
    {
        public delegate void setColumnValueDel(string columnName, string columnValue, T obj);

        public List<T> ImportFromExcel(IFormFile file, string DestinationPath, string[] allowedColumns, setColumnValueDel setval)
        {


            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (file.Length == 0)
            {
                return null;
            }
            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
            NPOI.SS.UserModel.ISheet sheet;
            string fullPath = Path.Combine(DestinationPath, file.FileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Position = 0;
                if (sFileExtension == ".xls")//This will read the Excel 97-2000 formats    
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                    sheet = hssfwb.GetSheetAt(0);
                }
                else //This will read 2007 Excel format    
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                    sheet = hssfwb.GetSheetAt(0);
                }
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                // Start creating the html which would be displayed in tabular format on the screen  
                //sb.Append("<table class='table'><tr>");
                List<string> columnNames = new List<string>();
                List<int> indexList = new List<int>();

                for (int j = 0; j < cellCount; j++)
                {
                    NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    //sb.Append("<th>" + cell.ToString() + "</th>");
                    string actualCellName = cell.ToString().ToLower().Trim();
                    string cellName = Regex.Replace(actualCellName, @"\s+", "");
                    if (allowedColumns.Contains(cellName))
                    {
                        columnNames.Add(cellName);
                        indexList.Add(j);
                    }

                }

                List<T> bugs = new List<T>();
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    //QueryBuilder queryBuilder = new QueryBuilder("developer");

                    IRow row = sheet.GetRow(i);
                    T bug = (T)Activator.CreateInstance(typeof(T));
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {

                            // tanveer
                            int index = indexList.IndexOf(j);
                            var column = row.GetCell(j);
                            var celltype = column.CellType;
                            var columnValue = "";
                            if (celltype== CellType.Numeric && DateUtil.IsCellDateFormatted(column))
                            {
                                DateTime date = column.DateCellValue;
                                ICellStyle style = column.CellStyle;
                                // Excel uses lowercase m for month whereas .Net uses uppercase
                                string format = style.GetDataFormatString().Replace('m', 'M');
                                columnValue= date.ToString(format);
                            }
                            else
                            {
                                columnValue = column.ToString();
                            }
                            


                            if (index >= 0)
                               
                            {
                                string coulumnName = columnNames[index];
                                setval(coulumnName, columnValue, bug);

                                string colValue =
                                  columnValue.Replace("\'", string.Empty)
                                      .Replace("{", string.Empty)
                                      .Replace("}", string.Empty);
                                InsertIntoColumn(coulumnName, colValue, bug);
                            }
                           

                        }

                    }

                    bugs.Add(bug);
                    //sb.AppendLine("</tr>");
                }
                return bugs;

            }
        }

        public void InsertIntoColumn(string column, string value, T obj)
        {

            PropertyInfo propertyInfo = obj.GetType().GetProperty(column, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null) return;
            //Get type == Class;
            //get Prperty == property info
                Type propType = propertyInfo.PropertyType;
            if (propType == typeof(int))
            {
                int tmpVal = 0;
                int.TryParse(value, out tmpVal);
                propertyInfo.SetValue(obj, tmpVal);
            }
            else
            {
                propertyInfo.SetValue(obj, value);
            }

        }


    }
}
