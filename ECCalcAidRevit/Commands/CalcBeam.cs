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

            string expetedfilesavepath = "";
            if(doc.IsWorkshared)
            {
                expetedfilesavepath = ModelPathUtils.ConvertModelPathToUserVisiblePath(doc.GetWorksharingCentralModelPath());
            }
            else
            {
                expetedfilesavepath = doc.PathName;
            }

            string MyECCAFile = Path.GetDirectoryName(expetedfilesavepath) + "\\ECCalcAidFile.bin";



            ECCAProject myProject = new ECCAProject(MyECCAFile);

            try
            {
                IList<ElementId> selectedElements = uidoc.Selection.GetElementIds().ToList();
                if (selectedElements.Count > 0)
                {
                    Element selectedBeam = doc.GetElement(selectedElements[0]);
                    bool validStructuralBeam = ValidateStructuralBeam(selectedBeam);

                    if (validStructuralBeam == true)
                    {

                        ECCABeam newBeam = ConvertToECCABeam(selectedBeam);

                        
                        if(!(myProject.BeamCollection.IsUnique(newBeam)))
                        {
                            MessageBox.Show("Note this beam already exisits in the collection, do you want to reset its properties?","Warning ",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                        }
                        

                        frm_ECCAProgram ECCAMainProgram = new frm_ECCAProgram(myProject, newBeam);


                        ECCAMainProgram.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("The element you selected cannot be read as a structural beam");
                    }
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
            FamilyInstance selectedBeamAsfamInst = selectedBeam as FamilyInstance;
            
            ECCABeam result = new ECCABeam();

            BuiltInParameter lengthBIPar = BuiltInParameter.STRUCTURAL_FRAME_CUT_LENGTH;
            Parameter lengthPar = selectedBeam.get_Parameter(lengthBIPar);

            string MaterialName = selectedBeamAsfamInst.StructuralMaterialType.ToString();

            FamilySymbol sectionSymbol = selectedBeamAsfamInst.Symbol;

            result.Id = selectedBeam.Id.IntegerValue;
            result.RevitUId = selectedBeam.UniqueId;
            result.Length = Utils.FeetTomm(lengthPar.AsDouble());
            result.RevitSectionName = sectionSymbol.Name;
            result.RevitMaterialName = MaterialName;

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
