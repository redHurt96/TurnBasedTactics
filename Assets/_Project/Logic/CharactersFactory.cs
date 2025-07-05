using _Pathfinding.Common;
using _Project.View;
using UnityEngine;
using Zenject;
using static _Project.Constants;
using static UnityEngine.Object;

namespace _Project
{
    public class CharactersFactory
    {
        private readonly Grid _grid;
        private readonly GridView _gridView;
        private readonly CharactersRepository _players;
        private readonly CharactersRepository _enemies;
        private readonly CharactersViewMap _map;
        private readonly IInstantiator _instantiator;

        public CharactersFactory(
            Grid grid, 
            GridView gridView, 
            [Inject(Id = PLAYERS_REPOSITORY)] CharactersRepository players,
            [Inject(Id = ENEMIES_REPOSITORY)] CharactersRepository enemies, 
            ViewEventsQueue viewEventsQueue,
            CharactersViewMap map,
            IInstantiator instantiator)
        {
            _grid = grid;
            _gridView = gridView;
            _players = players;
            _enemies = enemies;
            _map = map;
            _instantiator = instantiator;
        }

        public void Place(PlacesConfig players, PlacesConfig enemies)
        {
            foreach (CharacterPlace place in players.Places)
            {
                Character player = Create(place.Position, place.Character, players.Color, PLAYER_TEAM);
                _players.Register(player);
            }
            
            foreach (CharacterPlace place in enemies.Places)
            {
                Character enemy = Create(place.Position, place.Character, enemies.Color, ENEMIES_TEAM);
                _enemies.Register(enemy);
            }
        }

        private Character Create(
            Vector2Int position, 
            CharacterConfig characterConfig, 
            Color color, 
            int team)
        {
            if (_grid.IsOccupied(position))
                throw new($"Cell {position} already occupied");

            Character character = characterConfig.Character.Copy(team, _grid.GetNode(position));
            CharacterView view = _instantiator.InstantiatePrefabForComponent<CharacterView>(characterConfig.View);
            CharacterUiView uiView = view.GetComponentInChildren<CharacterUiView>();
            
            view.Setup(character, color, _gridView.GetPosition(position));
            uiView.Setup(character);
            view.gameObject.name = character.Name;

            _map.Register(character, view, uiView);
            
            return character;
        }
    }
}