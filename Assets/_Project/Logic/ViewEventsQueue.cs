using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project
{
    public class ViewEventsQueue
    {
        public event Action Added;
        public bool IsEmpty => !Messages.Any();
        
        public readonly Queue<(Type, object)> Messages = new();

        public void Enqueue<T>(T message)
        {
            Messages.Enqueue((typeof(T), message));
            Added?.Invoke();
        }
    }
}