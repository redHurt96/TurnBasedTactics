using System.Collections.Generic;
using System.Linq;
using _Pathfinding.Common;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project
{
    public class GameLoop
    {
        [Inject(Id = Constants.PLAYERS_REPOSITORY)] private CharactersRepository _players;
        [Inject(Id = Constants.ENEMIES_REPOSITORY)] private CharactersRepository _enemies;
        [Inject] private Grid _grid;
        [Inject] private BreathFirstPathSolver _pathSolver;
        [Inject] private MessagesQueue _messagesQueue;
        [Inject] private IDecisionMaker _decisionMaker;
        
        private Character _current;

        public async UniTask Run()
        {
            while (_players.Any() || _enemies.Any())
            {
                await PlayQueue(_players, _enemies);
                await PlayQueue(_enemies, _players);
            }
        }

        private async UniTask PlayQueue(CharactersRepository currentCharacters, CharactersRepository currentEnemies)
        {
            Queue<Character> queue = currentCharacters.GetInitiativeQueue();

            while (queue.Any())
            {
                _current = queue.Dequeue();
                Select(_current);

                IDecision move = await _decisionMaker.Execute(_current, currentEnemies);
                await move.Execute();

                Deselect(_current);
            }
        }

        private void Select(Character current) =>
            _messagesQueue.Enqueue(new SelectEvent
            {
                Character = current,
            });
        
        private void Deselect(Character current) =>
            _messagesQueue.Enqueue(new DeselectEvent
            {
                Character = current,
            });
    }
}