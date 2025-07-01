using System;
using System.Collections.Generic;

namespace _Project
{
    public class MessagesQueue
    {
        public event Action Added;
        public readonly Queue<(Type, object)> Messages = new();
        
		public void Enqueue<T>(T message)
        {
            Messages.Enqueue((typeof(T), message));
            Added?.Invoke();
        }
    }
}