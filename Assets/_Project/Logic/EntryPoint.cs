using Zenject;
using static _Project.Constants;

namespace _Project
{
    public class EntryPoint : IInitializable
    {
        [Inject(Id = PLAYERS_PLACES_CONFIG)] private PlacesConfig _playersPlaces;
        [Inject(Id = ENEMIES_PLACES_CONFIG)] private PlacesConfig _enemiesPlaces;
        [Inject] private CharactersFactory _factory;
        [Inject] private ViewEventsManager _viewEventsManager;
        [Inject] private GameLoop _gameLoop;
        [Inject] private GridView _gridView;
        [Inject] private Grid _grid;
        [Inject] private GameOverUi _gameOverUi;

        public async void Initialize()
        {
            _gameOverUi.Hide();
            _gridView.Create(_grid);
            _factory.Place(_playersPlaces, _enemiesPlaces);
            _viewEventsManager.Initialize();
            int winnerTeam = await _gameLoop.Run();
            
            _gameOverUi.Show(winnerTeam);
        }
    }
}