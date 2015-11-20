using System;

namespace DistributedTestModels
{
    public static class EventForwarder
    {
        public static void Forward<TEventType>(object sender, object handlerEvent, TEventType args)
        {

            EventHandler<TEventType> handler = (EventHandler<TEventType>)handlerEvent;
            if (handler != null)
            {
                handler(sender, (TEventType)args);
            }
        }
    }
}
