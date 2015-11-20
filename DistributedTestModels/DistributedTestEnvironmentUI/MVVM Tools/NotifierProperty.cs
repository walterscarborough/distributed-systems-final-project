using System.ComponentModel;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DistributedTestEnvironmentUI.MVVM_Tools
{

    public class NotifierProperty<TPropType> : NotifierBase
    {

        private TPropType propValue;
        public NotifierProperty()
        {
        }

        public NotifierProperty(object value)
        {
            this.PropVal = (TPropType)value;
        }

       
        public TPropType PropVal
        {
            get
            {
                return propValue;
            }
            set
            {
                propValue = value;
                OnPropertyChanged("PropVal");
            }
        }



    }

    /// <summary>
    /// Provides common functionality for ViewModel classes
    /// </summary>
    [Serializable]
    public abstract class NotifierBase : INotifyPropertyChanged
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