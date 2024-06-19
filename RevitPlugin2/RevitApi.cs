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
    public static class RevitApi
    {
        public static UIApplication UiApplication { get; set; }
        public static UIDocument UIDocument { get => UiApplication.ActiveUIDocument; }
        public static Autodesk.Revit.DB.Document Document { get => UIDocument.Document; }

        public static void Initialise(ExternalCommandData commandData)
        {
            UiApplication = commandData.Application;
        }
    }
}
