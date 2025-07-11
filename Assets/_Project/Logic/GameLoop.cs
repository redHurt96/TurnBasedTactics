﻿using System.Collections.Generic;
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
        [Inject] private IPathSolver _pathSolver;
        [Inject] private ViewEventsQueue _viewEventsQueue;
        [Inject] private IDecisionMaker _decisionMaker;
        
        private Character _current;

        public async UniTask<int> Run()
        {
            while (_players.Any() && _enemies.Any())
            {
                await PlayQueue(_players, _enemies);
                await PlayQueue(_enemies, _players);
            }
            
            return _players.Any() ? 0 : 1;
        }

        private async UniTask PlayQueue(CharactersRepository currentCharacters, CharactersRepository currentEnemies)
        {
            Queue<Character> queue = currentCharacters.GetInitiativeQueue();

            while (queue.Any() && currentEnemies.Any())
            {
                _current = queue.Dequeue();
                _current.RestoreStamina();

                IDecision decision;
                do
                {
                    Select(_current);
                    decision = await _decisionMaker.Execute(_current, currentEnemies);
                    decision.Execute();
                } while (_current.Stamina > 0 && !_current.IsDead && decision is not SkipDecision);

                Deselect(_current);
            }
        }

        private void Select(Character current)
        {
            _viewEventsQueue.Enqueue(new SelectEvent
            {
                Character = current,
            });
        }

        private void Deselect(Character current) =>
            _viewEventsQueue.Enqueue(new DeselectEvent
            {
                Character = current,
            });
    }
}