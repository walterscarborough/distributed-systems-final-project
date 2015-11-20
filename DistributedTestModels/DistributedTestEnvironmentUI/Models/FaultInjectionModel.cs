using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DistributedTestEnvironmentUI.Models
{
    public class FaultInjectionModel
    {
        bool duplicateMessage;

        public bool DuplicateMessage
        {
            get { return duplicateMessage; }
            set { duplicateMessage = value; }
        }
        bool loseMessage;

        public bool LoseMessage
        {
            get { return loseMessage; }
            set { loseMessage = value; }
        }
        bool corruptMessage;

        public bool CorruptMessage
        {
            get { return corruptMessage; }
            set { corruptMessage = value; }
        }
        bool delayMessage;

        public bool DelayMessage
        {
            get { return delayMessage; }
            set { delayMessage = value; }
        }
        bool reverseOrderMessage;

        public bool ReverseOrderMessage
        {
            get { return reverseOrderMessage; }
            set { reverseOrderMessage = value; }
        }
        string previousMessage = null;

        public string PreviousMessage
        {
            get { return previousMessage; }
            set { previousMessage = value; }
        }
        int delay_ms = 0;

        public int Delay_ms
        {
            get { return delay_ms; }
            set { delay_ms = value; }
        }
        bool disable_process;

        public bool Disable_process
        {
            get { return disable_process; }
            set { disable_process = value; }
        }

        private string OutOfOrderMessage(string msg)
        {
            if(previousMessage == null)
            {
                previousMessage = msg;
                return null;
                
            }
            return msg;
        }

        private string DelayTheMessage(string msg)
        {
            Thread.Sleep(delay_ms);
            return msg;
        }

        private string CorruptTheMessage(string msg)
        {
            byte[] bytedata = Encoding.ASCII.GetBytes(msg);
            int counter = 0;
            foreach(byte data in bytedata)
            {
                bytedata[counter] ^= 0x55;
                counter++;
            }
            return Encoding.ASCII.GetString(bytedata, 0, bytedata.Length);
        }
    
        public string applyFaults(string msg)
        {
            string tmpMsg = msg;
            if(Disable_process || LoseMessage)
            {
                return null;
            }
            if (ReverseOrderMessage)
            {
                tmpMsg = OutOfOrderMessage(tmpMsg);
                if (tmpMsg == null)
                    return null;
            }
            if(CorruptMessage)
            {
                tmpMsg = CorruptTheMessage(tmpMsg);
            }
            if(DelayMessage)
            {
                DelayTheMessage(tmpMsg);
            }


            return tmpMsg;
        }
    
    }
}
