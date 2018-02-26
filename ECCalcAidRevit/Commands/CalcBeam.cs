using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ECCalcAidData;
using ECCalcAidData.Elements;
using ECCalcAidEngine;
using ECCalcAidEngine.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECCalcAidRevit.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class CalcBeam : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
           ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            
            string MyRevitProjectLodcation = doc.PathName;
            string MyECCAFile = Path.GetDirectoryName(MyRevitProjectLodcation) + "\\ECCalcAidFile.bin";

            ECCAProject myProject = new ECCAProject(MyECCAFile);

            try
            {
                IList<ElementId> selectedElements = uidoc.Selection.GetElementIds().ToList();
                if (selectedElements.Count > 0)
                {
                    Element selectedBeam = doc.GetElement(selectedElements[0]);
                    bool validStructuralBeam = ValidateStructuralBeam(selectedBeam);
                    if(validStructuralBeam == true)
                        MessageBox.Show("You selected a Valid beam");

                    ECCABeam newBeam = ConvertToECCABeam(selectedBeam);

                    frm_ECCAProgram ECCAMainProgram = new frm_ECCAProgram(myProject, newBeam);


                    ECCAMainProgram.ShowDialog();
                }

                //serialisation test

            }
            catch (Exception ex)
            {
            }

            return Result.Succeeded;
        }

        private ECCABeam ConvertToECCABeam(Element selectedBeam)
        {
            ECCABeam result = new ECCABeam();
            result.Id = selectedBeam.Id.IntegerValue;
            result.RevitUId = selectedBeam.UniqueId;


            BuiltInParameter paraIndex = BuiltInParameter.STRUCTURAL_FRAME_CUT_LENGTH;
            Parameter parameter = selectedBeam.get_Parameter(paraIndex);
            result.Length = Utils.FeetTomm(parameter.AsDouble());


            return result;
        }

        private bool ValidateStructuralBeam(Element selectedBeam)
        {
            bool result = false;

            // Get the category instance from the Category property
            Category category = selectedBeam.Category;
            BuiltInCategory enumCategory = (BuiltInCategory)category.Id.IntegerValue;
            
            if (enumCategory == BuiltInCategory.OST_StructuralFraming)
            {

            FamilyInstance beamAsInstance = selectedBeam as FamilyInstance;
            Parameter beamcutLength = beamAsInstance.LookupParameter("Cut Length");
                
                if (beamAsInstance != null && beamcutLength != null)
                {
                    result = true;
                }

            }

            return result;
        }
    }
}
