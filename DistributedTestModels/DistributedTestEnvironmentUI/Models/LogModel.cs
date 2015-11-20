using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedTestEnvironmentUI.Models
{
    public enum ELogflag
    {
        LOG,
        ENGINE,
        CRITICAL,
        CUSTOM
    }

    public class LogEventArgs : EventArgs
    {
        public string node;
        public string type;
        public string msg;

        public LogEventArgs(string Node, string Type, string Msg)
        {
            node = Node;
            type = Type;
            msg = Msg;
        }
    }

    public static class LogModel
    {
        public static string EngineName { get; set; }
        private static List<String> logdata = new List<string>();

        public static event EventHandler<LogEventArgs> onLogRecieved;

        public static void LogMessage( string msg, ELogflag flag, string title = "")
        {
            StringBuilder sb = new StringBuilder();
            string type = "";
            sb.Append("[" + EngineName + "]");
            switch (flag)
            {
                case ELogflag.LOG:
                    sb.Append("[LOG]");
                    type = "Log";
                    break;
                case ELogflag.ENGINE:
                    sb.Append("[ENGINE]");
                    type = "Engine";
                    break;
                case ELogflag.CRITICAL:
                    sb.Append("[CRITICAL]");
                    type = "Critical";
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
            EventForwarder.Forward<LogEventArgs>(null, onLogRecieved, new LogEventArgs(title, type, msg));
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
