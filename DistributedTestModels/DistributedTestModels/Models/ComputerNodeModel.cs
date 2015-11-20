using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributedTestModels.MVVM_Tools;

namespace DistributedTestModels
{
    public class ComputerNodeModel : ViewModelBase
    {
        private ObservableCollection<DistributedProcessModel> processList = new ObservableCollection<DistributedProcessModel>();
        private string nodeName;

        public string NodeName
        {
            get { return nodeName; }
            set { nodeName = value; }
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
