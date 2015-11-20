using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedTestEnvironmentUI.ViewModels
{
    public class LogCollection
    {
        private string node;

        public string Node
        {
            get { return node; }
            set { node = value; }
        }
        
        
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
