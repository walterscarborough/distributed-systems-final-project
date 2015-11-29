using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using DTEModels.MVVM_Tools;

namespace DTEModels.Models
{
    enum ProcessState{
        Stopped,
        Running,
    }
    public class DistributedProcessModel : ViewModelBase
    {
        string hostName;

        public string HostName
        {
            get { return hostName; }
            set { hostName = value;
            OnPropertyChanged("HostName");
            }
        }
        int port;

        public int Port
        {
            get { return port; }
            set { port = value;
            OnPropertyChanged("Port");
            }
        }

        int frameworkPort;

        public int FrameworkPort
        {
            get { return frameworkPort; }
            set { frameworkPort = value;
            OnPropertyChanged("FrameworkPort");
            }
        }
        string frameworkHost;
        public string FrameworkHost
        {
            get { return frameworkHost; }
            set
            {
                frameworkHost = value;
                OnPropertyChanged("FrameworkHost");
            }
        }

        RouteModel routing;

        public RouteModel Routing
        {
            get { return routing; }
            set { routing = value;
            OnPropertyChanged("Routing");
            }
        }

        Process proc;

        public Process Proc
        {
            get { return proc; }
            set { proc = value;
            OnPropertyChanged("Proc");
            }

        }
        string processName;

        public string ProcessName
        {
            get { return processName; }
            set { processName = value;
            OnPropertyChanged("ProcessName");
            }
        }
        string processPath;

        public string ProcessPath
        {
            get { return processPath; }
            set { processPath = value;
            OnPropertyChanged("ProcessPath");
            }
        }
        private List<string> arguments = new List<string>();
        ProcessState procState = new ProcessState();

        internal ProcessState ProcState
        {
            get { return procState; }
            set { procState = value; }
        }

        public DistributedProcessModel(string host, string process, string path, int portNum, bool local)
        {
            hostName = host;
            processName = process;
            processPath = path;
            port = portNum;
            if(processName == "node")
            {
                arguments.Add("--harmony");
                arguments.Add("app.js");
            }
            arguments.Add(host);
            arguments.Add(port.ToString());
            procState = ProcessState.Stopped;
            routing = new RouteModel(host, port - 10000, local);
            routing.openServerSocket();
        }

        public void addArgurment(string arg)
        {
            arguments.Add(arg);
        }

        public void startProcess()
        {
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            foreach (string arg in arguments)
            {
                start.Arguments += arg + " ";
            }
            start.Arguments.TrimEnd(" ".ToArray());
            Console.WriteLine(start.Arguments);
            // Enter the executable to run, including the complete path
            start.FileName = this.processPath + processName;
            

            proc = Process.Start(start);
            procState = ProcessState.Running;


        }

        public void stopProcess()
        {
            try
            {
                proc.CloseMainWindow();
                proc.Close();
                proc.Kill();
                procState = ProcessState.Stopped;
            }
            catch (Exception e)
            {
                //don't care
            }
        }
    
    
    }
}
