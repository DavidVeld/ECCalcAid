using ECCalcAidData;
using ECCalcAidData.Elements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public frm_ECCAProgram()
        {
            InitializeComponent();
            Project = new ECCAProject();
        }

        public frm_ECCAProgram(ECCAProject myProject)
        {
            InitializeComponent();
            Project = myProject;
        }

        public frm_ECCAProgram(ECCAProject myProject, ECCABeam newBeam)
        {
            InitializeComponent();

            Project = myProject;
            Project.BeamCollection.AddBeam(newBeam);
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
            Project.BeamCollection.AddBeam(selectedBeam);
            RefreshScreen();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateLibrary();
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            dgv_Sections.ItemsSource = Utils.ToDataTables(Library.SteelSectionCollection.SectionList).DefaultView;
            dgv_Materials.ItemsSource = Utils.ToDataTables(Library.MaterialCollection.MaterialList).DefaultView;
            dgv_Beams.ItemsSource = Utils.ToDataTables(Project.BeamCollection.BeamCollectionList).DefaultView;
        }

        private void InitiateLibrary()
        {
            
            Library = new ECCALibrary();
            Library = Library.OpenLibraryFromFile("");

            Project.ProjectInfo.ProjectName = "A test Name";
            txt_ProjectName.Text = Project.ProjectInfo.ProjectName;

       }

        private void mnu_Save_Click(object sender, RoutedEventArgs e)
        {
            Project.SerializeProject();
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
    }
}
