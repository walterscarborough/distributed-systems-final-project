using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTEModels.MVVM_Tools;

namespace DTEModels.Models
{
    public class ComputerNodeModel : ViewModelBase
    {
        private AsyncObservableCollection<DistributedProcessModel> processList = new AsyncObservableCollection<DistributedProcessModel>();

        public AsyncObservableCollection<DistributedProcessModel> ProcessList
        {
            get { return processList; }
            set { 
                processList = value;
                OnPropertyChanged("ProcessList");
            }
        }
        
        
        private string nodeName;

        public string NodeName
        {
            get { return nodeName; }
            set { nodeName = value;
            OnPropertyChanged("NodeName");
            }
        }

        private string frameworkNodeName;

        public string FrameworkNodeName
        {
            get { return frameworkNodeName; }
            set { frameworkNodeName = value; }
        }


        public void addProcess(string ProcessName, string hostName, string Path, int port, bool local)
        {
            DistributedProcessModel tmpProc = new DistributedProcessModel(hostName, ProcessName, Path, port,  local);
            processList.Add(tmpProc);
        }

        public void removeProcess(DistributedProcessModel tmpProc)
        {
            
            processList.Remove(tmpProc);
        }

        public DistributedProcessModel getProcess(string ProcessName, int Port)
        {
            foreach(DistributedProcessModel tmpProc in processList)
            {
                if(tmpProc.ProcessName == ProcessName && tmpProc.Port == Port)
                {
                    return tmpProc;
                }
            }
            return null;
        }
    }
}
