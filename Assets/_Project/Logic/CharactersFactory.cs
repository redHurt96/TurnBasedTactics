using _Pathfinding.Common;
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
        private readonly MessagesQueue _messagesQueue;
        private readonly CharactersViewMap _map;

        public CharactersFactory(
            Grid grid, 
            GridView gridView, 
            [Inject(Id = PLAYERS_REPOSITORY)] CharactersRepository players,
            [Inject(Id = ENEMIES_REPOSITORY)] CharactersRepository enemies, 
            MessagesQueue messagesQueue,
            CharactersViewMap map)
        {
            _grid = grid;
            _gridView = gridView;
            _players = players;
            _enemies = enemies;
            _messagesQueue = messagesQueue;
            _map = map;
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

            Character character = characterConfig.Character.Copy(_messagesQueue);
            character.Position = position;
            character.Team = team;
            
            CharacterView view = Instantiate(characterConfig.View, _gridView.GetPosition(position), Quaternion.identity);
            
            view.SetColor(color);
            view.gameObject.name = character.Name;

            _map.Register(character, view);
            
            return character;
        }
    }
}