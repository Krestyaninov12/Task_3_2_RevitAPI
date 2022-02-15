using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_2
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new PipeFilter(), "Выберите трубы");
           
            double Sum = 0;

            foreach (var selectedElement in selectedElementRefList)
            {
                Element element = doc.GetElement(selectedElement);
              
                Parameter lenghtParameter = element.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                if (lenghtParameter.StorageType == StorageType.Double)
                {
                    double lenghtValue = UnitUtils.ConvertFromInternalUnits(lenghtParameter.AsDouble(), UnitTypeId.Meters);
                    Sum += lenghtValue;
                }
            }

            TaskDialog.Show("Длина труб", $"Общая длина труб: {Sum} м");
            return Result.Succeeded;
        }
    }
}
