using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributedTestEnvironmentUI.MVVM_Tools;

namespace DistributedTestEnvironmentUI.Models
{
    public class TestEnvironmentModel : ViewModelBase
    {

        ObservableCollection<ComputerNodeModel> processNodes = new ObservableCollection<ComputerNodeModel>();

        public ObservableCollection<ComputerNodeModel> ProcessNodes
        {
            get { return processNodes; }
            set { processNodes = value;
            OnPropertyChanged("ProcessNodes");
            }
        }
        public void addExternalNode(string HostName)
        {
            //add linq to check for node
            if (findNode(HostName) == null)
            {
                ComputerNodeModel newNode = new ComputerNodeModel();
                newNode.FrameworkNodeName = HostName;
                newNode.NodeName = HostName;
                processNodes.Add(newNode);
            }
        }

        private ComputerNodeModel findNode(string HostName)
        {
            var compNodes = from compNode in processNodes where compNode.NodeName == HostName select compNode;
            if (compNodes.Count() > 0)
                return compNodes.First<ComputerNodeModel>();
            return null;
        }

        public DistributedProcessModel getProcess(string HostName, string ProcessName, int Port)
        {
            return findNode(HostName).getProcess(ProcessName, Port + 10000);
        }

        public void addExternalProcess(string ProcName, string HostName, int Port, string Path)
        {
            ComputerNodeModel tmpNode = findNode(HostName);
            if (tmpNode == null)
                return;
            tmpNode.addProcess(ProcName, HostName, Path, Port + 10000, false);
          
        }

        public void addNode(string HostName)
        {
            if (findNode(HostName) == null)
            {
                ComputerNodeModel newNode = new ComputerNodeModel();
                newNode.FrameworkNodeName = "127.0.0.1";
                newNode.NodeName = HostName;
                processNodes.Add(newNode);
            }


        }


        public void removeNode(string HostName) 
        {
            ComputerNodeModel tmpNode = findNode(HostName);
            if (tmpNode == null)
                return;
            processNodes.Remove(tmpNode);
        }
        public void addProcess(string ProcName, string HostName, int Port, string Path, string[] arguments) 
        {
            ComputerNodeModel tmpNode = findNode(HostName);
            if (tmpNode == null)
                return;
            tmpNode.addProcess(ProcName, tmpNode.FrameworkNodeName, Path, Port + 10000, true);
            foreach(string arg in arguments)
              tmpNode.getProcess(ProcName, Port + 10000).addArgurment(arg);
            
          
        }
        public void removeProcess(string ProcName, string HostName, int Port)
        {
            ComputerNodeModel tmpNode = findNode(HostName);
            if (tmpNode == null)
                return;
            tmpNode.removeProcess(tmpNode.getProcess(ProcName, Port + 10000));
            
        }
        public void startProcess(string HostName, int port, string ProcessName) 
        { 
            ComputerNodeModel tmpNode = findNode(HostName);
            
            if (tmpNode == null)
                return;
            DistributedProcessModel tmpProc = tmpNode.getProcess(ProcessName, port + 10000 );
            
            tmpProc.startProcess();
           
        }
        public void stopProcess(string HostName, int port, string ProcessName) 
        {
            ComputerNodeModel tmpNode = findNode(HostName);
            if (tmpNode == null)
                return;
            DistributedProcessModel tmpProc = tmpNode.getProcess(ProcessName, port + 10000);
            tmpProc.stopProcess();
        }
        
        public void setDelay(DistributedProcessModel proc, int delay) 
        {
            proc.Routing.faults.Delay_ms = delay;
        }

        public void enableDelay(DistributedProcessModel proc)
        {
            proc.Routing.faults.DelayMessage = true;
        }

        public void disableDelay(DistributedProcessModel proc)
        {
            proc.Routing.faults.DelayMessage = false;
        }

        public void disableProcess(DistributedProcessModel proc) 
        {
            proc.Routing.faults.Disable_process = true;
        }
        public void enableProcess(DistributedProcessModel proc) 
        {
            proc.Routing.faults.Disable_process = false;
        }

        public void duplicateMessage(DistributedProcessModel proc) 
        {
            proc.Routing.faults.DelayMessage = true;
        }

        public void stopDuplicate(DistributedProcessModel proc) 
        {
            proc.Routing.faults.DelayMessage = false;
        }

        public void corruptMessage(DistributedProcessModel proc)
        {
            proc.Routing.faults.CorruptMessage = true;
        }

        public void stopCorrupt(DistributedProcessModel proc)
        {
            proc.Routing.faults.CorruptMessage = false;
        }

        public void loseMessage(DistributedProcessModel proc)
        {
            proc.Routing.faults.LoseMessage = true;
        }

        public void disableLose(DistributedProcessModel proc)
        {
            proc.Routing.faults.LoseMessage = false;
        }

        public void outofOrder(DistributedProcessModel proc)
        {
            proc.Routing.faults.ReverseOrderMessage = true;
        }

        public void disableoutofOrder(DistributedProcessModel proc)
        {
            proc.Routing.faults.ReverseOrderMessage = false;
        }
    }
}
