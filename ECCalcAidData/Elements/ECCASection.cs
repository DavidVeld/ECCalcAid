using System;
using System.Data;
using System.Reflection;

namespace ECCalcAidData.Elements
{
    //All section Properties
    [Serializable]
    public class ECCASection
    {
        public string Designation;
        public string CategoryName;
        public string SubCategory;
        public string Comment;


        //Dimensional
        public double G;

        public double h;
        public double b;
        public double d;
        public double tw;
        public double tf;
        public double r;
        public double r1;
        public double r2;

        public double m2_m1;
        public double A;

        //Structural
        public double Iy;
        public double Iz;
        public double iy;
        public double iz;
        public double IT;
        public double Iw;

        public double Wely;
        public double Welz;
        public double Wply;
        public double Wplz;
        
        //Specific / Rare

        public ECCASection()
        {
            Designation = "NewSection";
            CategoryName = "-";
        }

        public void ApplySettings(DataRow row)
        {

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