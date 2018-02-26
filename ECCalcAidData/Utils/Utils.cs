using ECCalcAidData.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ECCalcAidData.Elements;
using System.Windows.Forms;

namespace ECCalcAidData
{
    public static class Utils
    {
        /// <summary>
        /// Loads a CSV (Comma Seperated Value) to a DataTable.
        /// </summary>
        public static DataTable LoadCSV(string strFilePath, string SheetName)
        {
            //Call up a table
            DataTable dt = new DataTable(Path.GetFileNameWithoutExtension(SheetName));

            //Variable used for headers
            int rowcount = 0;
            using (Csv.CsvFileReader reader = new CsvFileReader(strFilePath))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    List<string> RowData = new List<string>();

                    //Read a line to the list
                    foreach (string s in row)
                    {
                        RowData.Add(s);
                    }

                    if (rowcount == 0)
                    {
                        //this is a header
                        foreach (string header in RowData)
                        {
                            dt.Columns.Add(header);
                        }
                    }
                    else
                    {
                        //This is a DataRow
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < RowData.Count; i++)
                        {
                            dr[i] = RowData[i];
                        }

                        dt.Rows.Add(dr);
                    }
                    rowcount++;
                }
                return dt;
            }
        }

        public static double FeetTomm(double feetValue)
        {
            double result = feetValue;
            result = feetValue * 304.8;
            return result;
        }

        //Below old code to generate datatables these became generic later
        /*
        /// <summary>
        /// Returns a DataTable from a List for presentaiton purposes
        /// </summary>
        /// <param name="eccaSectionList"></param>
        /// <returns></returns>
        public static DataTable SectionListToDataTable(IList<ECCASection> eccaSectionList)
        {
            FieldInfo[] fieldValues = typeof(ECCASection).GetFields();

            DataTable table = new DataTable();
            for (int i = 0; i < fieldValues.Length; i++)
            {
                FieldInfo field = fieldValues[i];
                table.Columns.Add(field.Name, field.FieldType);
            }

            object[] values = new object[fieldValues.Length];

            foreach (ECCASection item in eccaSectionList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = fieldValues[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// Returns a DataTable from a List for presentaiton purposes
        /// </summary>
        /// <param name="eccaSectionList"></param>
        /// <returns></returns>
        public static DataTable MaterialListToDataTable(IList<ECCAMaterial> eccaMaterialList)
        {
            FieldInfo[] fieldValues = typeof(ECCAMaterial).GetFields();

            DataTable table = new DataTable();
            for (int i = 0; i < fieldValues.Length; i++)
            {
                FieldInfo field = fieldValues[i];
                table.Columns.Add(field.Name, field.FieldType);
            }

            object[] values = new object[fieldValues.Length];

            foreach (ECCAMaterial item in eccaMaterialList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = fieldValues[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        */

        /// <summary>
        /// Generic Convertion from List to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTables<T>(IList<T> data)
        {
            FieldInfo[] fieldValues = typeof(T).GetFields();

            DataTable table = new DataTable();
            try
            {
                for (int i = 0; i < fieldValues.Length; i++)
                {
                    FieldInfo field = fieldValues[i];
                    table.Columns.Add(field.Name, field.FieldType);
                }


                object[] values = new object[fieldValues.Length];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = fieldValues[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return table;
        }
    



    public static double ConvertMeToDouble(string value)
        {
            double result = 0;

            try
            {
                //result = Convert.ToDouble(value);
                bool ok = double.TryParse(value, out result);
            }
            catch
            {
                result = 0;
                return result;
            }
            return result;

        }
    }
}
