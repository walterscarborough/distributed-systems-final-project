using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DistributedTestEnvironmentUI.Models;

namespace DistributedTestEnvironmentUI.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DistributedTestEnvironmentUI.ViewModels.MainViewModel tmp = (DistributedTestEnvironmentUI.ViewModels.MainViewModel)this.DataContext;
            Type nodeType = e.NewValue.GetType();
            if (nodeType.Name == "DistributedProcessModel")
                tmp.CurrentProcess = (DistributedProcessModel)e.NewValue;
        }
    }

    


}
