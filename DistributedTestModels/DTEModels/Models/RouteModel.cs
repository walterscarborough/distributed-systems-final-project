using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DTEModels.MVVM_Tools;

namespace DTEModels.Models
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
        private FaultInjectionModel faults = new FaultInjectionModel();
        public FaultInjectionModel Faults
        {
            get { return faults; }
            set
            {
                faults = value;
                OnPropertyChanged("Faults");
            }
        }

        private StatisticsModel stats = new StatisticsModel();

        public StatisticsModel Stats
        {
            get { return stats; }
            set { stats = value;
            OnPropertyChanged("Stats");
            }
        }


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
            string tmp = Faults.applyFaults(data);
            return tmp;
        }

        public void sendMessage(string msg)
        {
            TcpIPClientModel cm = new TcpIPClientModel();
            cm.OpenCommPort(physicalHost, physicalPort);
            cm.SendMsg(msg);
            cm.close();
        }

        public void forwardMessage(object sender, MessageEventArgs e) 
        {


            LogModel.LogMessage("Forwarding: " + e.Data, this.frameworkHost + ":" + this.frameworkPort ,ELogflag.LOG, "Forward Message");



            Stats.AddMessage(new LogEventArgs(this.frameworkHost + ":" + this.frameworkPort, DateTime.Now, "Message Received", "Log", e.Data));
            string msg = applyFaults(e.Data);
            if (msg != null)
            {
                TcpIPClientModel cm = new TcpIPClientModel();
                cm.OpenCommPort(physicalHost, physicalPort);
                cm.SendMsg(msg);
                cm.close();
                if (Faults.DuplicateMessage)
                {
                    cm = new TcpIPClientModel();
                    cm.OpenCommPort(physicalHost, physicalPort);
                    cm.SendMsg(msg);
                    cm.close();
                }
                if(Faults.ReverseOrderMessage)
                {
                    cm = new TcpIPClientModel();
                    cm.OpenCommPort(physicalHost, physicalPort);
                    cm.SendMsg(Faults.PreviousMessage);
                    cm.close();
                    if (Faults.DuplicateMessage)
                    {
                        cm = new TcpIPClientModel();
                        cm.OpenCommPort(physicalHost, physicalPort);
                        cm.SendMsg(Faults.PreviousMessage);
                        cm.close();
                    }
                    Faults.PreviousMessage = null;
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
