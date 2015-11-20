using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DistributedTestEnvironmentUI.MVVM_Tools;
using DistributedTestEnvironmentUI.Models;

namespace DistributedTestEnvironmentUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

       /* TestEnvironmentModel testEnvironment = new TestEnvironmentModel();

        public TestEnvironmentModel TestEnvironment
        {
            get { return testEnvironment; }
            set { 
                testEnvironment = value;
                OnPropertyChanged("TestEnvironment");
            }
        }*/

        public AsyncObservableCollection<LogCollection> LogDataGrid { get; set; }
        private DistributedProcessModel currentProcess;

        public DistributedProcessModel CurrentProcess
        {
            get { return currentProcess; }
            set
            {
                currentProcess = value;
            OnPropertyChanged("CurrentProcess");
            }
        }
        
        DistributedTestEnvironmentUI.Views.AddNode addNodeView;
        DistributedTestEnvironmentUI.Views.AddProcess addProcessView;

        public void OnLogReceived(object sender, LogEventArgs e)
        {
            LogCollection tmp = new LogCollection();
            tmp.Node = e.node;
            tmp.Type = e.type;
            tmp.Message = e.msg;
            LogDataGrid.Add(tmp);
           
        }


        public MainViewModel()
        {
            LogModel.onLogRecieved += OnLogReceived;
            LogDataGrid = new AsyncObservableCollection<LogCollection>();
            
            NodeName = new NotifierProperty<string>("Node Name");
            
            ProcessNodeName = new NotifierProperty<string>("Node Name");
            ProcessName = new NotifierProperty<string>("ClientTest.exe");
            ProcessPort = new NotifierProperty<int>(8000);
            ProcessArguments = new NotifierProperty<string>("8001 Walter");
            ProcessPath = new NotifierProperty<string>("");
            ProcPort = 8000;
            addNode("Node1");
            string[] processArgs = { "8001", "Walter" };
            this.addProcess("ClientTest.exe", "Node1", 8000, "",processArgs );
            
        }

        #region Commands

        private DelegateCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }
        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }


        private DelegateCommand dg_addNode;
        private DelegateCommand dg_removeNode;
        private DelegateCommand dg_updateNode;
        public ICommand AddNodeCmd
        {
            get
            {
                if (dg_addNode == null)
                {
                    dg_addNode = new DelegateCommand(AddNode);
                }
                return dg_addNode;
            }
        }
        private void AddNode()
        {
            addNodeView = null;
            addNodeView = new Views.AddNode();
            addNodeView.DataContext = this;
            addNodeView.Show();
        }

        public ICommand RemoveNodeCmd
        {
            get
            {
                if (dg_removeNode == null)
                {
                    dg_removeNode = new DelegateCommand(RemoveNode);
                }
                return dg_removeNode;
            }
        }
        private void RemoveNode()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ICommand UpdateNodeCmd
        {
            get
            {
                if (dg_updateNode == null)
                {
                    dg_updateNode = new DelegateCommand(UpdateNode);
                }
                return dg_updateNode;
            }
        }
        private void UpdateNode()
        {
            System.Windows.Application.Current.Shutdown();
        }


        private DelegateCommand dg_addProcess;
        private DelegateCommand dg_removeProcess;
        private DelegateCommand dg_updateProcess;
        private DelegateCommand dg_startProcess;
        private DelegateCommand dg_stopProcess;
        public ICommand AddProcessCmd
        {
            get
            {
                if (dg_addProcess == null)
                {
                    dg_addProcess = new DelegateCommand(AddProcess);
                }
                return dg_addProcess;
            }
        }
        private void AddProcess()
        {
            addProcessView = null;
            addProcessView = new Views.AddProcess();
            addProcessView.DataContext = this;
            addProcessView.Show();
        }
        public ICommand RemoveProcessCmd
        {
            get
            {
                if (dg_removeProcess == null)
                {
                    dg_removeProcess = new DelegateCommand(RemoveProcess);
                }
                return dg_removeProcess;
            }
        }
        private void RemoveProcess()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ICommand UpdateProcessCmd
        {
            get
            {
                if (dg_updateProcess == null)
                {
                    dg_updateProcess = new DelegateCommand(UpdateProcess);
                }
                return dg_updateProcess;
            }
        }
        private void UpdateProcess()
        {
            DistributedProcessModel proc = this.getProcess(ProcHostName, ProcName, ProcPort);
            if(ProcDelay)
            {
                this.enableDelay(proc);
                this.setDelay(proc, ProcDelayTime);
            }
            else
            {
                this.disableDelay(proc);
            }
            if (ProcCorrupt)
                this.corruptMessage(proc);
            else
                this.stopCorrupt(proc);
            if (ProcDuplicate)
                this.duplicateMessage(proc);
            else
                this.stopDuplicate(proc);
            if (ProcOutofOrder)
            {
                this.outofOrder(proc);
            }
            else
                this.disableoutofOrder(proc);
            if (ProcLose)
            {
                this.loseMessage(proc);
            }
            else
                this.disableLose(proc);
        }

        public ICommand StartProcessCmd
        {
            get
            {
                if (dg_startProcess == null)
                {
                    dg_startProcess = new DelegateCommand(StartProcess);
                }
                return dg_startProcess;
            }
        }
        private void StartProcess()
        {
            this.startProcess(ProcHostName, ProcPort, ProcName);
        }

        public ICommand StopProcessCmd
        {
            get
            {
                if (dg_stopProcess == null)
                {
                    dg_stopProcess = new DelegateCommand(StopProcess);
                }
                return dg_stopProcess;
            }
        }
        private void StopProcess()
        {
            this.startProcess(ProcHostName, ProcPort, ProcName);
        }
#endregion


        #region AddNode

        public NotifierProperty<string> NodeName { get; set; }
        private DelegateCommand addanode;
        public ICommand AddANodeCmd
        {
            get
            {
                if (addanode == null)
                {
                    addanode = new DelegateCommand(AddANode);
                }
                return addanode;
            }
        }
        private void AddANode()
        {
            this.addNode(NodeName.PropVal);
            addNodeView.Close();
        }

        #endregion

        #region AddProcess

        public NotifierProperty<string> ProcessNodeName { get; set; }
        public NotifierProperty<string> ProcessName { get; set; }
        public NotifierProperty<int> ProcessPort { get; set; }
        public NotifierProperty<string> ProcessPath { get; set; }
        public NotifierProperty<string> ProcessArguments { get; set; }


        private DelegateCommand addaprocess;
        public ICommand AddAProcessCmd
        {
            get
            {
                if (addaprocess == null)
                {
                    addaprocess = new DelegateCommand(AddAProcess);
                }
                return addaprocess;
            }
        }
        private void AddAProcess()
        {
            char del = ' ';
            string[] argus = ProcessArguments.PropVal.Split(del);
            this.addProcess(ProcessName.PropVal, ProcessNodeName.PropVal, ProcessPort.PropVal, ProcessPath.PropVal, argus);
            addProcessView.Close();
        }
        #endregion

        #region Process
        private bool procLose;
        public bool ProcLose
        {
            get { return procLose; }
            set
            {
                procLose = value;
                OnPropertyChanged("ProcLose");
            }
        }
        private bool procCorrupt;
        public bool ProcCorrupt
        {
            get { return procCorrupt; }
            set
            {
                procCorrupt = value;
                OnPropertyChanged("ProcCorrupt");
            }
        }
        private bool procDelay;
        public bool ProcDelay
        {
            get { return procDelay; }
            set
            {
                procDelay = value;
                OnPropertyChanged("ProcDelay");
            }
        }
        private bool procOutofOrder;
        public bool ProcOutofOrder
        {
            get { return procOutofOrder; }
            set
            {
                procOutofOrder = value;
                OnPropertyChanged("ProcOutofOrder");
            }
        }
        private bool procDuplicate;
        public bool ProcDuplicate
        {
            get { return procDuplicate; }
            set
            {
                procDuplicate = value;
                OnPropertyChanged("ProcDuplicate");
            }
        }
        private int procDelayTime;
        public int ProcDelayTime
        {
            get { return procDelayTime; }
            set
            {
                procDelayTime = value;
                OnPropertyChanged("ProcDelayTime");
            }
        }
        private int procPort;
        public int ProcPort
        {
            get { return procPort; }
            set
            {
                procPort = value;
                OnPropertyChanged("ProcPort");
            }
        }
        private string procName;
        public string ProcName
        {
            get { return procName; }
            set
            {
                procName = value;
                OnPropertyChanged("ProcName");
            }
        }



        private string procHostName;
        public string ProcHostName
        {
            get { return procHostName; }
            set
            {
                procHostName = value;
                OnPropertyChanged("ProcHostName");
            }
        }


       


        #endregion

        #region TestEnvironment

               AsyncObservableCollection<ComputerNodeModel> processNodes = new AsyncObservableCollection<ComputerNodeModel>();

        public AsyncObservableCollection<ComputerNodeModel> ProcessNodes
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

        #endregion


    }

