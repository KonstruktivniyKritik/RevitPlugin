using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace RevitPlugin2
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel(List<Element> elements)
        {
            CollectProperies(elements);
        }
        private List<Category> elementCategories = new List<Category>();
        public List<Category> ElementCategories 
        { 
            get { return elementCategories; } 
        }
        private void CollectProperies(List<Element> elements)
        {
            foreach (var element in elements)
            {
                if (element.Category != null)
                {
                    var selected = elementCategories.Where(p => p.Name == element.Category.Name);
                    if (selected.Count() > 0)
                    {
                        selected.First().CountOfElements++;
                        selected.First().Ids.Add(element.Id.IntegerValue);
                    }
                    else 
                    {
                        Category NewCategory = new Category(element.Category.Name, element.Id.IntegerValue, element.Parameters);
                        elementCategories.Add(NewCategory);
                    }
                }
            }


        }

        // Команда изменения свойств
        private RevitCommand properyCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public RevitCommand ProperyCommand(Func<object,bool> commandBinding_CanExecute)
        {
                return properyCommand ??
                  (properyCommand = new RevitCommand(obj =>
                  {
                      PropertyCommandParameters commandParameters = obj as PropertyCommandParameters;
                      using (Transaction t = new Transaction(RevitApi.Document, "parameter"))
                      {
                          t.Start();
                          int count = 0;
                          foreach (int Id in commandParameters.ElementsId)
                          {
                              var element = RevitApi.Document.GetElement(new ElementId(Id));
                              var parametr = element.LookupParameter(commandParameters.Property);
                              if (parametr == null)
                                  continue;
                              bool change = parametr.Set(commandParameters.Value);
                              if (change)
                                  count++;
                          }
                          MessageBox.Show($"Параметры были изменены в {count} элементах из {commandParameters.ElementsId.Count}");
                          t.Commit();
                      }
                          

                  }, commandBinding_CanExecute));
        }
    }
}
