using ECCalcAidData;
using ECCalcAidEngine.Forms;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECCalcAid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ECCAProject myProject { get; private set; }
        public ECCALibrary myLibrary { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(ECCAProject myProject)
        {
            InitializeComponent();
        }

        public MainWindow(ECCAProject myProject, int ElementId)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frm_ECCAProgram ECCAProgramForm = new frm_ECCAProgram();
            ECCAProgramForm.ShowDialog();
            this.Close();
        }


    }
}
