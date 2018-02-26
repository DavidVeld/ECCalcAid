using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECCalcAidData;
using ECCalcAidData.Elements;

namespace ECCalcAidDataBuilder
{
    public partial class frm_ImportItem : Form
    {
        public IList<ECCASection> formattedSectionList;
        public IList<ECCAMaterial> formattedMaterialList;

        public bool Overwrite { get; set; }

        public frm_ImportItem()
        {
            InitializeComponent();
        }
        private void frm_ImportItem_Load(object sender, EventArgs e)
        {
            cbb_Type.Items.Add("Material");
            cbb_Type.Items.Add("Section");
            cbb_Type.Text = "Section";
            chk_Overwrite.Checked = false;
            Overwrite = false;
        }
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "cvs files (*.csv)|*.csv|All files (*.*)|*.*";

            var path = openFileDialog.ShowDialog();
            FileInfo finfo = new FileInfo(openFileDialog.FileName);

            txt_Path.Text = openFileDialog.FileName;
            txt_Category.Text = finfo.Name;
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            //Get the profile from a cvs:
            try
            {
                DataTable dt = Utils.LoadCSV(txt_Path.Text, txt_Category.Text);

                if (cbb_Type.Text == "Material")
                {
                    formattedMaterialList = tryParseMaterial(dt);
                    dgv_Data.DataSource = ECCalcAidData.Utils.ToDataTables(formattedMaterialList);

                }
                else if (cbb_Type.Text == "Section")
                {
                    formattedSectionList = tryParseSection(dt);
                    dgv_Data.DataSource = ECCalcAidData.Utils.ToDataTables(formattedSectionList);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private IList<ECCAMaterial> tryParseMaterial(DataTable dt)
        {
            //List<String> fieldNameList = new List<string>();
            List<ECCAMaterial> result = new List<ECCAMaterial>();
            //Reach row is one element
            //Loop through datatab;e
            foreach (DataRow dr in dt.Rows)
            {
                ECCAMaterial newMaterial = new ECCAMaterial();
                //set the properties of the element per column
                newMaterial.CategoryName = txt_Category.Text;
                newMaterial.SubCategory = txt_SubCategory.Text;

                foreach (DataColumn dc in dt.Columns)
                {

                    string columnName = dc.ToString();
                    string columnValue = dr[dc].ToString();

                    //check section fields columnName
                    FieldInfo[] fieldValues = newMaterial.GetType().GetFields();
                    for (int i = 0; i < fieldValues.Length; i++)
                    {
                        FieldInfo field = fieldValues[i];
                        Type fieldType = field.FieldType;

                        if (field.Name == columnName)
                        {

                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(newMaterial, columnValue);
                            }
                            else if (field.FieldType == typeof(double))
                            {
                                field.SetValue(newMaterial, Utils.ConvertMeToDouble(columnValue));
                            }
                        }
                    }

                }
                result.Add(newMaterial);

            }

            return result;
        }

        private IList<ECCASection> tryParseSection(DataTable dt)
        {

            //List<String> fieldNameList = new List<string>();
            List<ECCASection>  result = new List<ECCASection>();

            //Reach row is one element
            //Loop through datatab;e
            foreach (DataRow dr in dt.Rows)
            {
                ECCASection newSection = new ECCASection();
                //set the properties of the element per column
                newSection.CategoryName = txt_Category.Text;
                newSection.SubCategory = txt_SubCategory.Text;

                foreach (DataColumn dc in dt.Columns)
                {

                    string columnName = dc.ToString();
                    string columnValue = dr[dc].ToString();

                    //check section fields columnName
                    FieldInfo[] fieldValues = newSection.GetType().GetFields();
                    for (int i = 0; i < fieldValues.Length; i++)
                    {
                        FieldInfo field = fieldValues[i];
                        Type fieldType = field.FieldType;

                        if (field.Name == columnName)
                        {

                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(newSection, columnValue);
                            }
                            else if (field.FieldType == typeof(double))
                            {
                                field.SetValue(newSection, Utils.ConvertMeToDouble(columnValue));
                            }
                        }
                    }

                }
                result.Add(newSection);

            }

            return result;
            
        }


        bool tryParseSectionShort(DataTable dt)
        {
            bool result = false;
            List<String> fieldNameList = new List<string>();
            formattedSectionList = new List<ECCASection>();

            foreach (DataRow dr in dt.Rows)
            {

                ECCASection newSection = new ECCASection();
                //set the properties of the element per column
                newSection.CategoryName = txt_Category.Text;
                newSection.SubCategory = txt_SubCategory.Text;

                foreach (DataColumn dc in dt.Columns)
                {

                    string columnName = dc.ToString();
                    string columnValue = dr[dc].ToString();

                    //check section fields columnName
                    FieldInfo[] fieldValues = typeof(ECCASection).GetFields();
                    for (int i = 0; i < fieldValues.Length; i++)
                    {
                        FieldInfo field = fieldValues[i];
                        Type fieldType = field.FieldType;

                        if(field.Name == columnName)
                        {

                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(newSection, columnValue);
                            }
                            else if(field.FieldType == typeof(double))
                            {
                                field.SetValue(newSection, Utils.ConvertMeToDouble(columnValue));
                            }
                        }
                    }

                }
                formattedSectionList.Add(newSection);

            }

            return result;
        }

        public IList createList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            Overwrite = chk_Overwrite.Checked;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
