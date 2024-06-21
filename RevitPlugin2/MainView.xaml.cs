using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitPlugin2
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public ObservableCollection<Category> ElementCategories = new ObservableCollection<Category>();
        public ObservableCollection<string> CategoriesProperties = new ObservableCollection<string>();
        PropertyCommandParameters CommandParameters;
        public MainView(ViewModel viewModel)
        {
            InitializeComponent();
            foreach (Category category in viewModel.ElementCategories)
                ElementCategories.Add(category);
            ViewCategories.ItemsSource = ElementCategories;
            ViewCategories.SelectedIndex = 0;
            ViewPropertyName.ItemsSource = CategoriesProperties;
            PropertyButton.Command = viewModel.ProperyCommand(CommandBinding_CanExecute);
        }

        private bool CommandBinding_CanExecute(object sender)
        {
            if (CommandParameters != null)
                return true;
            else return false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check)
            {
                if (check.IsChecked == true)
                {
                    foreach (string property in ((Category)check.DataContext).Properties)
                        if (!CategoriesProperties.Contains(property))
                            CategoriesProperties.Add(property);
                }
                else
                {
                    CategoriesProperties.Clear();
                    foreach (Category category in ElementCategories)
                    {
                        if (category.Checked)
                        {
                            foreach (string property in category.Properties)
                                if (!CategoriesProperties.Contains(property))
                                    CategoriesProperties.Add(property);
                        }
                    }
                }

                PrepareParams(null,null);
            }
        }

        private void PrepareParams(object sender, object e)
        {
            if (ViewPropertyName.SelectedItem != null || ViewPropertyName.Text != "")
            {
                List<Category> SelectedCategories = new List<Category>();
                foreach (Category category in ElementCategories)
                    if (category.Checked)
                        SelectedCategories.Add(category);
                if (SelectedCategories.Count > 0)
                    CommandParameters = new PropertyCommandParameters(SelectedCategories, ViewPropertyName.Text == "" ? ViewPropertyName.SelectedItem.ToString() : ViewPropertyName.Text , ViewPropertyValue.Text);
                else CommandParameters = null;
                PropertyButton.CommandParameter = CommandParameters;
            }
            else CommandParameters = null;
        }
    }

    public class PropertyCommandParameters
    {
        public List<int> ElementsId = new List<int>();
        public string Property;
        public string Value;
        public PropertyCommandParameters(List<Category> elementCategories, string property, string value)
        {
            foreach (Category category in elementCategories)
                ElementsId.AddRange(category.Ids);
            Property = property;
            Value = value;
        }
    }
}
