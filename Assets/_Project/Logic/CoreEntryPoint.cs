using _Pathfinding.Common;
using Zenject;
using static _Project.Constants;

namespace _Project
{
    public class CoreEntryPoint : IInitializable
    {
        [Inject(Id = PLAYERS_PLACES_CONFIG)] private PlacesConfig _playersPlaces;
        [Inject(Id = ENEMIES_PLACES_CONFIG)] private PlacesConfig _enemiesPlaces;
        [Inject] private CharactersFactory _factory;
        [Inject] private ViewEventsManager _viewEventsManager;
        [Inject] private GameLoop _gameLoop;
        [Inject] private GridView _gridView;
        [Inject] private Grid _grid;

        public void Initialize()
        {
            _gridView.Create(_grid);
            _factory.Place(_playersPlaces, _enemiesPlaces);
            _viewEventsManager.Initialize();
            _gameLoop.Run();
        }
    }
}