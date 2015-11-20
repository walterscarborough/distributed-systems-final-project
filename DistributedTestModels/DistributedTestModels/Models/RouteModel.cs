using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DistributedTestModels.MVVM_Tools;

namespace DistributedTestModels
{

        public class MessageEventArgs : EventArgs
    {
        private string data;
        public string Data
        {
            get { return data; }
        }

        public MessageEventArgs(string indata)
        {
            data = indata;
        }
    }


    public class RouteModel : ViewModelBase
    {
        
        public string frameworkHost;
        public int frameworkPort;
        private string physicalHost;
        private int physicalPort;
       
        TcpIPServerModel sm;
        public FaultInjectionModel faults = new FaultInjectionModel();
     
        


        public RouteModel(string host, int port, bool local)
        {
            frameworkHost = host;
            frameworkPort = port;
            physicalPort = port + 10000;
            if (local)
            {
                physicalHost = "127.0.0.1";
            }
            else
            {
                physicalHost = host;
            }
            sm = new TcpIPServerModel(physicalHost, frameworkPort);
            sm.onMessageRecieved += forwardMessage;
            //faults.ReverseOrderMessage = true;
        }

        private void serverConnect()
        {

            try
            {
                
                sm.OpenCommPort();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
            
           
        }

        public string applyFaults(string data) 
        {
            string tmp = faults.applyFaults(data);
            return tmp;
        }

        public void forwardMessage(object sender, MessageEventArgs e) 
        {
            LogModel.LogMessage("Forwarding: " + e.Data, ELogflag.LOG, "Whatever");
            string msg = applyFaults(e.Data);
            if (msg != null)
            {
                TcpIPClientModel cm = new TcpIPClientModel();
                cm.OpenCommPort(physicalHost, physicalPort);
                cm.SendMsg(msg);
                cm.close();
                if (faults.DuplicateMessage)
                {
                    cm = new TcpIPClientModel();
                    cm.OpenCommPort(physicalHost, physicalPort);
                    cm.SendMsg(msg);
                    cm.close();
                }
                if(faults.ReverseOrderMessage)
                {
                    cm = new TcpIPClientModel();
                    cm.OpenCommPort(physicalHost, physicalPort);
                    cm.SendMsg(faults.PreviousMessage);
                    cm.close();
                    if (faults.DuplicateMessage)
                    {
                        cm = new TcpIPClientModel();
                        cm.OpenCommPort(physicalHost, physicalPort);
                        cm.SendMsg(faults.PreviousMessage);
                        cm.close();
                    }
                    faults.PreviousMessage = null;
                }
               
            }
            
           
        }

        

        public void openServerSocket() 
        {
            serverConnect();
        }

        public void closeIncomingSocket() 
        {
            //sm.close();
        }

        
    }
}
