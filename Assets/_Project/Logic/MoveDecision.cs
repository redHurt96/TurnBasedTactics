using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using static Cysharp.Threading.Tasks.UniTask;

namespace _Project
{
    public class MoveDecision : IDecision
    {
        private readonly Character _source;
        private readonly List<Node> _path;
        private readonly MessagesQueue _messages;

        public MoveDecision(Character source, List<Node> path, MessagesQueue messages)
        {
            _source = source;
            _path = path;
            _messages = messages;
        }

        public UniTask Execute()
        {
            _source.Move(_path.Last());

            _messages.Enqueue(new MoveEvent
            {
                Character = _source,
                Path = _path,
            });
            
            return CompletedTask;
        }
    }
}