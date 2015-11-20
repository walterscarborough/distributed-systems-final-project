using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Dynamic;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Reflection;
using System.Linq.Expressions;
using System.Xml;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Linq;



namespace DistributedTestEnvironmentUI.MVVM_Tools
{


    /// <summary>
    /// Provides common functionality for ViewModel classes
    /// </summary>  
    [Serializable]
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }




    }
}