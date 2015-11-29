using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientTest.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClientTest
{


    class Program
    {
        static Collection<string> sentMsgs = new Collection<string>();
       // static ClientSocketModel sendSocket = new ClientSocketModel();
       // static ServerSocketModel receiveSocket = new ServerSocketModel();
        static TcpIPServerModel sm;
        static string Host;
        static string IncomingPort;
        static string OutgoingPort;
        
        static void onReceive(object sender, MessageEventArgs e)
        {
           
            if(sentMsgs.Contains(e.Data))
            {
                sentMsgs.Remove(e.Data);
                
            }
            else
            {
                Console.WriteLine(e.Data);
                
                Send(Host, int.Parse(OutgoingPort),  e.Data);
            }
           // OpenPort(Host, int.Parse(IncomingPort));
            
        }

        public static void OpenPort(string Host, int Port)
        {
           
            sm = new TcpIPServerModel(Host, Port);
            sm.onMessageRecieved += onReceive;
            sm.OpenCommPort();
        }

        public static void Send(string host, int port, string msg)
        {
            sentMsgs.Add(msg);
            TcpIPClientModel cm = new TcpIPClientModel();
            cm.OpenCommPort(host, port);
            cm.SendMsg(msg);
            cm.close();
        }

        static void Main(string[] args)
        {

             Host = args[0];
             IncomingPort = args[1];
             OutgoingPort = args[2];
            string Name = args[3];


            
            int port = int.Parse(IncomingPort);
            OpenPort(Host, port);
            Console.WriteLine("Enter text or Exit");
            string userInput = Console.ReadLine(); ;
            while(userInput.Trim() != "Exit")
            {
                
                Send(Host, int.Parse(OutgoingPort), Name + ": " + userInput);
                userInput = Console.ReadLine();
            }
            
        }
    }
}
