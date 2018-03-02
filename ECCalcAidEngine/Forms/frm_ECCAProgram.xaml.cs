using ECCalcAidData;
using ECCalcAidData.Elements;
using ECCalcAidEngine.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECCalcAidEngine.Forms
{
    /// <summary>
    /// Interaction logic for frm_ECCAProgram.xaml
    /// </summary>
    public partial class frm_ECCAProgram : Window
    {

        public ECCAProject Project { get; private set; }
        public ECCALibrary Library { get; private set; }
        public ECCABeam selectedBeam { get; private set; }

        public frm_ECCAProgram()
        {
            InitializeComponent();
            Project = new ECCAProject();
            selectedBeam = new ECCABeam();
        }

        public frm_ECCAProgram(ECCAProject myProject)
        {
            InitializeComponent();
            Project = myProject;
            selectedBeam = new ECCABeam();

            RefreshScreen();
        }

        public frm_ECCAProgram(ECCAProject myProject, ECCABeam newBeam)
        {
            InitializeComponent();
            bool isunique;

            Project = myProject;
            Project.BeamCollection.AddBeam(newBeam, out isunique);

            if (isunique)
            {
                selectedBeam = newBeam;
            }
            else
            {
                selectedBeam = Project.BeamCollection.GetBeam(newBeam);
            }

            LoadBeamToInterface();

        }

        public frm_ECCAProgram(string myProjectPath)
        {
            InitializeComponent();
            this.Project = Project.DeSerializeProject(myProjectPath);
        }

        public frm_ECCAProgram(ECCABeam selectedBeam)
        {
            InitializeComponent();
            Project = new ECCAProject();
            bool isunique;

            Project.BeamCollection.AddBeam(selectedBeam, out isunique);
            RefreshScreen();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateLibrary();
            RefreshScreen();


        }

        #region InterfaceRefreshes
        private void RefreshScreen()
        {
            dgv_Sections.ItemsSource = Utils.ToDataTables(Library.SteelSectionCollection.SectionList).DefaultView;
            dgv_Materials.ItemsSource = Utils.ToDataTables(Library.MaterialCollection.MaterialList).DefaultView;
            dgv_Beams.ItemsSource = Utils.ToDataTables(Project.BeamCollection.BeamCollectionList).DefaultView;

            LoadBeamToInterface();
            LoadProjectSettings();
        }

        private void InitiateLibrary()
        {
            Library = new ECCALibrary();
            Library = Library.OpenLibraryFromFile("");
       }
        #endregion

        #region LoadAndStoreScrips
        private void LoadProjectSettings()
        {
            txt_ProjectName.Text = Project.ProjectInfo.ProjectName;
            txt_ProjectNr.Text = Project.ProjectInfo.ProjectNr;
            txt_ProjectDesc.Text = Project.ProjectInfo.Description;
            txt_ProjectAddress.Text = Project.ProjectInfo.ProjectAddress;
            txt_ProjectEngineer.Text = Project.ProjectInfo.Engineer;
        }

        private void LoadBeamToInterface()
        {
            txt_BeamName.Text = Utils.ConvertMeToString(selectedBeam.Name);
            txt_BeamId.Text = selectedBeam.Id.ToString();
            txt_BeamLength.Text = selectedBeam.Length.ToString();
            txt_BeamComment.Text = Utils.ConvertMeToString(selectedBeam.Comment);

            myCanvas.Children.Clear();

            foreach (UIElement uie in ImageGenerator.generateImage(this.myCanvas, selectedBeam))
            {
                myCanvas.Children.Add(uie);
            }
        }

        private void storeProjectSettings()
        {
            Project.ProjectInfo.ProjectNr = txt_ProjectNr.Text;
            Project.ProjectInfo.ProjectName = txt_ProjectName.Text;
        }

        private void storeBeamSettings()
        {
            selectedBeam.Name = txt_BeamName.Text;
            selectedBeam.Id = Convert.ToInt32(txt_BeamId.Text);
            selectedBeam.Length = Utils.ConvertMeToDouble(txt_BeamLength.Text);
            selectedBeam.Comment = txt_BeamComment.Text;
        }

        #endregion

        #region menubuttonCommands

        private void mnu_Save_Click(object sender, RoutedEventArgs e)
        {
            storeBeamSettings();
            storeProjectSettings();
            
            bool ok = Project.SerializeProject();
            if (ok == true)
            {
                System.Windows.Forms.MessageBox.Show("Project Saved");
            }
        }
        
        private void mnu_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            Project = Project.DeSerializeProject(openFileDialog.FileName);
            RefreshScreen();
        }

        private void mnu_SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgv_Beams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgv_Beams.SelectedItems[0];

                Int32 beamId = Convert.ToInt32(row["Id"]);

                ECCABeam selectedBeamBuffer = Project.BeamCollection.GetBeam(beamId);

                if(selectedBeamBuffer != null)
                {
                    selectedBeam = selectedBeamBuffer;
                }

                LoadBeamToInterface();
            }
            catch
            {

            }
        }

        #endregion
    }
}
