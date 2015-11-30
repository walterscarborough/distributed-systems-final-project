using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTEModels.MVVM_Tools;

namespace DTEModels.Models
{
    public class StatisticsModel : ViewModelBase
    {
        private AsyncObservableCollection<StatisticsMessageModel> msgDataGrid = new AsyncObservableCollection<StatisticsMessageModel>();
        public AsyncObservableCollection<StatisticsMessageModel> MsgDataGrid
        {
            get { return msgDataGrid; }
            set
            {
                msgDataGrid = value;
                OnPropertyChanged("MsgDataGrid");
            }
        }

        private int messagesReceived;

        public int MessagesReceived
        {
            get { return messagesReceived; }
            set { 
                messagesReceived = value;
                OnPropertyChanged("MessagesReceived");
            }
        }

        private int bytesReceived;

        public int BytesReceived
        {
            get { return bytesReceived; }
            set { bytesReceived = value;
            OnPropertyChanged("BytesReceived");
            }
        }

        private float averageMessageSize;

        public float AverageMessageSize
        {
            get { return averageMessageSize; }
            set { 
                averageMessageSize = value;
                OnPropertyChanged("AverageMessageSize");
            }
        }

        public void AddMessage(LogEventArgs e)
        {
            messagesReceived++;
            bytesReceived += e.msg.Length;
            AverageMessageSize = bytesReceived / messagesReceived;
            StatisticsMessageModel tmpItem = new StatisticsMessageModel();
            tmpItem.Message = e.msg;
            tmpItem.Node = e.node;
            tmpItem.TimeStamp = e.dateTime;
            tmpItem.Title = e.title;
            tmpItem.Type = e.type;
            MsgDataGrid.Add(tmpItem);
        }

        public void clearStats()
        {
           msgDataGrid.Clear();
           messagesReceived = 0;
           bytesReceived = 0;
           averageMessageSize = 0;
        }


    }
}
