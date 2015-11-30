using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTEModels.Models
{
    public class StatisticsMessageModel
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string timeStamp;

        public string TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string node;

        public string Node
        {
            get { return node; }
            set { node = value; }
        }




        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
