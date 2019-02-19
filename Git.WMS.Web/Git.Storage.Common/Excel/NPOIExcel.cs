/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-11 9:58:15
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-11 9:58:15       情缘
*********************************************************************************/

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;

namespace Git.Storage.Common.Excel
{
    public class NPOIExcel
    {
        private string _title;
        private string _sheetName;
        private string _filePath;

        public NPOIExcel(string title, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
        }
        public NPOIExcel()
        { 
        
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table)
        {
            FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = this._sheetName.IsEmpty() ? "sheet1" : this._sheetName;
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(this._title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
            row.Height = 500;

            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //处理表格列头
            row = sheet.CreateRow(1);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                row.Height = 350;
                sheet.AutoSizeColumn(i);
            }

            //处理数据内容
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = sheet.CreateRow(2 + i);
                row.Height = 250;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                    sheet.SetColumnWidth(j, 256 * 15);
                }
            }

            //写入数据流
            workBook.Write(fs);
            fs.Flush();
            fs.Close();

            return true;
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="title"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table, string title, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
            return ToExcel(table);
        }

        /// <summary>
        /// 读取Excel到DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable Read(string filePath,int sheetIndex)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate); 
            HSSFWorkbook workbook = new HSSFWorkbook(fs);
            ISheet sheet1 = workbook.GetSheetAt(sheetIndex);
            DataTable table = new DataTable();
            IRow row1 = sheet1.GetRow(0);
            int cellCount = row1.LastCellNum;
            for (int i = row1.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(row1.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            int rowCount = sheet1.LastRowNum; 
            for (int i = (sheet1.FirstRowNum+1); i <= sheet1.LastRowNum; i++)
            {
                IRow row = sheet1.GetRow(i);
                if (row != null)
                {
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    table.Rows.Add(dataRow);
                }
                
            }
            workbook = null; 
            sheet1 = null;
            return table;
        }
    }
}
