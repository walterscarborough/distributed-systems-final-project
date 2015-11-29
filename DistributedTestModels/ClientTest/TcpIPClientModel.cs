using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
namespace ClientTest.Models
{
    public class TcpIPClientModel
    {
        public event EventHandler<MessageEventArgs> onMessageRecieved;
        public ManualResetEvent allDone = new ManualResetEvent(false);
        private Thread readThread;
       
        private bool monitorFlag = false;
        private NetworkStream stream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private Socket socket;
       
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
        

        public TcpIPClientModel()
        {
            readThread = new Thread(Read);
           

        }
        public void SendMsg(string msg)
        {
            try
            {
                streamWriter.WriteLine(msg);

            }
            catch (Exception ex)
            {
              //  LogModel.LogMessage("Error writing message on socket " + IpAddress + " port: " + Port.ToString(), ELogflag.CRITICAL, "Socket Open Error");
              //  LogModel.LogMessage("Error trace: " + ex.Message, ELogflag.CRITICAL, "Error trace");
            }
        }
        
        public void OpenCommPort(string IP, int PortNumber)
        {
            IpAddress = IP;
            Port = PortNumber;
          //  LogModel.LogMessage("Opening client port on " + IpAddress + " port: " + Port.ToString(), ELogflag.LOG, "Socket Open");
            allDone.Reset();
            try
            {
                  IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(IpAddress), Port);
                  socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                  socket.Connect(remoteEP);
                  stream = new NetworkStream(socket);

                  streamReader = new StreamReader(stream);
                  streamWriter = new StreamWriter(stream);
                  streamWriter.AutoFlush = true;
                  allDone.Set();
               
            }
            catch (Exception ex)
            {
            //    LogModel.LogMessage("Error trying to open socket " + IpAddress + " port: " + Port.ToString(), ELogflag.CRITICAL, "Socket Open Error");
           //     LogModel.LogMessage("Error trace: " + ex.Message, ELogflag.CRITICAL, "Error trace");

            }
        }

        public void Receive()
        {
            monitorFlag = true;         
            readThread.Start();
            
        }

        public void close()
        {
            monitorFlag = false;
            readThread.Abort();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

        }

        public void Read()
        {
            while (monitorFlag)
            {
                try
                {
                    string msg = streamReader.ReadLine();
                    msgRecieved(msg);
                }
                catch (TimeoutException) { }
            }
        }

        public void msgRecieved(string msg)
        {
            EventForwarder.Forward<MessageEventArgs>(this, onMessageRecieved, new MessageEventArgs(msg));
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
