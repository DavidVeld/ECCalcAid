using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ECCalcAidData;
using ECCalcAidData.Elements;
using ECCalcAidData.CodeTables;
using System.IO;

namespace ECCalcAidDataBuilder
{
    public partial class frm_DataBuilder : Form
    {
        public ECCALibrary ECCalcAidData;

        public frm_DataBuilder()
        {
            InitializeComponent();
        }

        private void frm_DataBuilder_Load(object sender, EventArgs e)
        {
            ECCalcAidData = new ECCALibrary();
            RefreshInterface();
        }

        private void mnu_New_Click(object sender, EventArgs e)
        {
            //Create Empty Database
            ECCalcAidData = new ECCALibrary();
            ECCalcAidData.MaterialCollection.CreateNewMaterial();
            ECCalcAidData.SteelSectionCollection.CreateNewSection();
            ECCalcAidData.SteelSectionCollection.CreateDummySection();

            RefreshInterface();
        }

        private void RefreshInterface()
        {
            ///CLS
            trv_DataTree.Nodes.Clear();
            trv_DataTree.Nodes.Add(ECCalcAidData.AsTreeView());
            
            //dgv_Data.DataSource = ECCalcAidData.SteelSectionCollection.ToDataTable("*");
        }

        private void mnu_Compile_Click(object sender, EventArgs e)
        {
            if(ECCalcAidData != null)
            {
                ECCalcAidData.WriteLibraryToFile("Test");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECCalcAidData = ECCalcAidData.OpenLibraryFromFile("Test");
            RefreshInterface();
        }

        private void mnu_Import_Click(object sender, EventArgs e)
        {
            frm_ImportItem ImportItemForm = new frm_ImportItem();
            ImportItemForm.ShowDialog();
            if (ImportItemForm.DialogResult == DialogResult.OK)
            {
                //Import the elements;
                if (ImportItemForm.cbb_Type.Text == "Section")
                {
                    ECCalcAidData.SteelSectionCollection.AddCollection(ImportItemForm.formattedSectionList,ImportItemForm.Overwrite);
                }
                else if (ImportItemForm.cbb_Type.Text == "Material")
                {

                }
                else
                {

                }
                RefreshInterface();
            }
        }

        private void trv_DataTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            MessageBox.Show(selectedNode.FullPath);
            string[] splittedSelection = selectedNode.FullPath.Split('\\');

            string selectedElement = splittedSelection[splittedSelection.Length - 1];
            string selectedElementType = splittedSelection[1];

            if (selectedElementType == "Sections")
            {
                ECCASection selection = ECCalcAidData.GetSection(selectedElement) as ECCASection;
                dgv_Data.DataSource = selection.ToDataTable();

            }
            else if(selectedElementType == "Materials")
            {
                ECCAMaterial selection = ECCalcAidData.GetMAterial(selectedElement) as ECCAMaterial;
                dgv_Data.DataSource = selection.ToDataTable();
            }

        }

        private void mnu_LoadCodes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";

            var path = openFileDialog.ShowDialog();
            FileInfo finfo = new FileInfo(openFileDialog.FileName);

            ECCACodeTables myCodeTables = new ECCACodeTables(openFileDialog.FileName);

            //If successfull compile Data
            myCodeTables.Compile();
        }
    }
}
