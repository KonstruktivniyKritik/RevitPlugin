using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitPlugin2
{
    public class RevitApi
    {
        public RevitApi(ExternalCommandData commandData) { UiApplication = commandData.Application; }
        public UIApplication UiApplication { get; set; }
        public UIDocument UIDocument { get => UiApplication.ActiveUIDocument; }
        public Autodesk.Revit.DB.Document Document { get => UIDocument.Document; }

    }
}
