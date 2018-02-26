using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;


namespace ECCalcAidRevit
{
    /// <summary>
    /// Creates the interface for the Revit addin
    /// Build: 
    /// </summary>
    public class ECCalcAidRevitMenu : IExternalApplication
    {
        static string _path = System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string imgpath = Path.GetDirectoryName(_path);

        public static FailureDefinitionId failureDefinitionId = new FailureDefinitionId(new Guid("9C94B935-2C1C-487C-AB14-41430303FFC4"));

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel ECCAPANel = application.CreateRibbonPanel("ECCalcAid");
            string HelpURL = Directory.GetParent(imgpath) + "\\NoteBuilder.html";
            ContextualHelp contextualHelp = new ContextualHelp(ContextualHelpType.Url, HelpURL);

            PushButton pB_NoteNew = ECCAPANel.AddItem(new PushButtonData("CalcBeam", "Analyse Beam", _path, "ECCalcAidRevit.Commands.CalcBeam")) as PushButton;
            //// Set the image
            //Uri img_NoteNew = new Uri(imgpath + @"\img\ico_new.jpg");
            //BitmapImage lgime_NoteNewd = new BitmapImage(img_NoteNew);
            //pB_NoteNew.LargeImage = lgime_NoteNewd;
            ////End Add button Code
            pB_NoteNew.SetContextualHelp(contextualHelp);
            pB_NoteNew.ToolTip = "Analyse the selected beam";

            registerAssemblies();

            return Result.Succeeded;

        }

        private void registerAssemblies()
        {
            Assembly.LoadFrom(imgpath + "//ECCalcAidData.dll");
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
