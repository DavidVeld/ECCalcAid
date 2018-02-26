using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ECCalcAidData.CodeTables
{
    class ExcelReader
    {
        public DataSet getExcelFile(string path)
        {
            DataSet result = new DataSet("ECCACodeTables");

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);

            int numSheets = xlWorkbook.Sheets.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!

            for (int a = 1; a <= numSheets; a++)
            {
                try
                {
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[a];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    int rowCount = xlRange.Rows.Count;
                    int colCount = xlRange.Columns.Count;

                    DataTable dt = new DataTable(xlWorksheet.Name);
                    //First find all the column names
                    for (int c = 1; c <= colCount; c++)
                    {
                        dt.Columns.Add(xlRange.Cells[1, c].Value2.ToString());
                    }

                    for (int i = 1; i <= rowCount; i++)
                    {
                        DataRow dr = dt.NewRow();

                        for (int j = 1; j <= colCount; j++)
                        {
                            /*
                                                    //new line
                                                    if (j == 1)
                                                        Console.Write("\r\n");

                                                    //write the value to the console
                                                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                                                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                            */
                            //ToDatatable
                            dr[j - 1] = xlRange.Cells[i, j].Value2.ToString();


                        }
                        dt.Rows.Add(dr);
                    }
                    result.Tables.Add(dt);

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return result;
        }
    }
}
