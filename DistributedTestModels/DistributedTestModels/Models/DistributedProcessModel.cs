using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using DistributedTestModels.MVVM_Tools;

namespace DistributedTestModels 
{
    enum ProcessState{
        Stopped,
        Running,
    }
    public class DistributedProcessModel : ViewModelBase
    {
        string hostName;
        int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        int frameworkPort;

        public int FrameworkPort
        {
            get { return frameworkPort; }
            set { frameworkPort = value; }
        }

        RouteModel routing;

        public RouteModel Routing
        {
            get { return routing; }
            set { routing = value; }
        }

        Process proc;

        public Process Proc
        {
            get { return proc; }
            set { proc = value; }
        }
        string processName;

        public string ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }
        string processPath;

        public string ProcessPath
        {
            get { return processPath; }
            set { processPath = value; }
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
