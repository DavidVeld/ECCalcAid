using System;
using System.Data;
using System.Reflection;

namespace ECCalcAidData.Elements
{
    [Serializable]
    public class ECCAMaterial
    {
        public string MaterialName;
        public string CategoryName;
        public string MaterialColour;

        public double Grade;
        /// <summary>
        /// N/mm²
        /// </summary>
        public double E_pl;
        /// <summary>
        /// N/mm²
        /// </summary>
        public double Yield;
        /// <summary>
        /// N/mm²
        /// </summary>
        public double Tensile;
        /// <summary>
        /// mm/°C
        /// </summary>
        public double thermalExpansion;

        //Advanced

        public double yf;
        public double yu;

        public double yf2;
        public double yu2;
        public string SubCategory;

        public ECCAMaterial()
        {
            this.CategoryName = "General";
            this.MaterialName = "MaterialName";
        }

        public DataTable ToDataTable()
        {
            FieldInfo[] fieldValues = this.GetType().GetFields();

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Field")); //0
            table.Columns.Add(new DataColumn("Value")); //1

            for (int i = 0; i < fieldValues.Length; i++)
            {
                FieldInfo field = fieldValues[i];

                DataRow dr = table.NewRow();
                dr[0] = field.Name;
                dr[1] = field.GetValue(this);

                table.Rows.Add(dr);
            }

            return table;
        }
    }
}