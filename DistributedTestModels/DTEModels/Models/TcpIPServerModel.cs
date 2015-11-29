using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace DTEModels.Models
{
    public class TcpIPServerModel
    {
        public event EventHandler<MessageEventArgs> onMessageRecieved;
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public ManualResetEvent receiveDone = new ManualResetEvent(false);
        private Thread readThread;
        private Thread connectThread;
        private bool monitorFlag = false;
        private NetworkStream stream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private Socket socket;
        
        private TcpListener listener;
        
        private int port;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private bool isServer = false;


        public TcpIPServerModel(string IP, int PortNumber)
        {
            IpAddress = IP;
            Port = PortNumber;
            
            connectThread = new Thread(WaitConnection);
        }

        private void WaitConnection()
        {
            try
            {
                while (true)
                {
                    socket = listener.AcceptSocket();
                    stream = new NetworkStream(socket);
                    streamReader = new StreamReader(stream);
                    streamWriter = new StreamWriter(stream);
                    streamWriter.AutoFlush = true;
                    allDone.Set();
                    receiveDone.Reset();
                    Receive();
                }
            }
            catch (Exception ex)
            {
                ex = ex;

            }
        }

        public void OpenCommPort()
        {
            
            
            
            allDone.Reset();
            try
            {
                LogModel.LogMessage("Opening server port on " + IpAddress + " port: " + Port.ToString(), this.IpAddress + ":" + Port.ToString(), ELogflag.LOG, "Socket Open");
                listener = TcpListener.Create(Port);
                listener.Start();
                connectThread.Start();
               
            }
            catch (Exception ex)
            {
                LogModel.LogMessage("Error trying to open socket " + IpAddress + " port: " + Port.ToString(), this.IpAddress + ":" + Port.ToString(), ELogflag.ERROR, "Socket Open Error");
                LogModel.LogMessage("Error trace: " + ex.Message, this.IpAddress + ":" + Port.ToString(), ELogflag.ERROR, "Error trace");

            }
        }

        public void Receive()
        {
            monitorFlag = true;
            readThread = new Thread(Read);
            readThread.Start();
            
        }

        public void close()
        {
            monitorFlag = false;
            readThread = null;
            connectThread = null;
        }

        public void Read()
        {
            while (monitorFlag)
            {
                try
                {
                    string msg = streamReader.ReadLine();
                    msgRecieved(msg);
                    monitorFlag = false;
                }
                catch (TimeoutException) { }
            }
        }

        public void msgRecieved(string msg)
        {
            LogModel.LogMessage(msg, this.IpAddress + ":" + Port.ToString(), ELogflag.LOG, "Message Received");
            EventForwarder.Forward<MessageEventArgs>(this, onMessageRecieved, new MessageEventArgs(msg));
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception ex)
            {
                LogModel.LogMessage("Socket shutdown twice", this.IpAddress + ":" + Port.ToString(), ELogflag.WARNING, "Framework");
            }
                receiveDone.Set();
            
        }


        public void SendMsg(string msg)
        {
            try
            {
                streamWriter.WriteLine(msg);

            }
            catch (Exception ex)
            {
                LogModel.LogMessage("Error writing message on socket " + IpAddress + " port: " + Port.ToString(), this.IpAddress + ":" + Port.ToString(), ELogflag.ERROR, "Socket Open Error");
                LogModel.LogMessage("Error trace: " + ex.Message, this.IpAddress + ":" + Port.ToString() ,ELogflag.ERROR, "Error trace");
            }
        }
    }
}
