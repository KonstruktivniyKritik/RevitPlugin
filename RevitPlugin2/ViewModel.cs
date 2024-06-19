using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitPlugin2
{
    public class ViewModel
    {
        public ViewModel(List<Element> elements)
        {
            CollectProperies(elements);
        }

        public Dictionary<string,int> ElementCategories = new Dictionary<string,int>();
        public int SelectedCategory;
        public string Property {  get; set; }
        public string PropertyValue { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();

        public Parameter SelectedParameter;
        private void CollectProperies(List<Element> elements)
        {
            foreach (var element in elements)
            {
                if (element.Category != null)
                {
                    if (ElementCategories.ContainsKey(element.Category.Name))
                        ElementCategories[element.Category.Name]++;
                    else ElementCategories[element.Category.Name] = 1;
                }
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
