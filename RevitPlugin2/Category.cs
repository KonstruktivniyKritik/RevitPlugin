using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitPlugin2
{
    public class Category
    {
        public string Name { get; set; }
        public int CountOfElements { get; set; }
        public List<string> Properties = new List<string>();
        public List<int> Ids { get; set; } = new List<int>();
        public bool Checked { get; set; } = false;
        public Category(string Name, int Id, Autodesk.Revit.DB.ParameterSet parameters) 
        { 
            this.Name = Name; 
            CountOfElements = 1; 
            Ids.Add(Id);
            foreach (Parameter parameter in parameters)
            {
                if (!parameter.IsReadOnly)
                    Properties.Add(parameter.Definition.Name);
            }
        }
    }
}
