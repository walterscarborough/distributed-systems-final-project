using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DTEModels.Models
{
    public enum ELogflag
    {
        LOG,
        WARNING,
        ERROR,
        FATAL,
        CUSTOM
    }

    public class LogEventArgs : EventArgs
    {
        public string node;
        public string type;
        public string msg;
        public string dateTime;
        public string title;


        public LogEventArgs(string Node, DateTime logTime, string title, string Type, string Msg)
        {
            node = Node;
            type = Type;
            msg = Msg;
            dateTime = logTime.ToShortDateString() + "," + logTime.ToLongTimeString();
            this.title = title;
        }
    }

    
    public static class LogModel
    {
        public static string EngineName { get; set; }
        private static List<String> logdata = new List<string>();

        public static event EventHandler<LogEventArgs> onLogRecieved;
       // static object lockObj = new object();
        public static void LogMessage( string msg, string sender, ELogflag flag, string title)
        {
          
            StringBuilder sb = new StringBuilder();
            string type = "";
            
            DateTime dt = DateTime.Now;

            sb.Append("[" + dt.ToShortDateString() + "," + dt.ToLongTimeString() + "]");
            sb.Append("[" + sender + "]");
            switch (flag)
            {
                case ELogflag.LOG:
                    sb.Append("[LOG]");
                    type = "Log";
                    break;
                case ELogflag.WARNING:
                    sb.Append("[WARNING]");
                    type = "Warning";
                    break;
                case ELogflag.ERROR:
                    sb.Append("[ERROR]");
                    type = "Error";
                    break;
                case ELogflag.FATAL:
                    sb.Append("[FATAL]");
                    type = "FATAL";
                    break;
                case ELogflag.CUSTOM:
                    title = title.ToUpper();
                    sb.Append("[" + title + "]");
                    type = "Custom";
                    break;
                default:
                    sb.Append("[UNKNOWN]");
                    type = "Unknown";
                    break;
            }
            
            sb.Append(msg);
            Console.WriteLine(sb.ToString());
            logdata.Add(sb.ToString());
            
            EventForwarder.Forward<LogEventArgs>(null, onLogRecieved, new LogEventArgs(sender, dt, title, type, msg));
           
        }

        public static void ClearLogData()
        {
            logdata.Clear();
        }

        public static void PrintLog()
        {
            Console.WriteLine("===== LOG DATA =====\n");
            Console.WriteLine(new DateTime().ToString() + "\n");
            foreach (string s in logdata)
            {
                Console.WriteLine(s + "\n");
            }
        }

        public static void SaveLog(string path)
        {
            throw new NotSupportedException();
        }
    }
}
