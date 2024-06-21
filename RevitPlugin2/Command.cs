using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Windows.Controls;
namespace RevitPlugin2
{
    [Autodesk.Revit.Attributes.Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        static AddInId AddInId = new AddInId(new Guid("0EDB62D4-93E6-427A-8052-B90079791900"));
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RevitApi revitApi = new RevitApi(commandData);
            var viewModel = new ViewModel(revitApi);
            var view = new MainView(viewModel);
            view.ShowDialog();
            return Result.Succeeded;
        }
    }
}
