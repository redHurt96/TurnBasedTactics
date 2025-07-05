using _Pathfinding.Common;
using UnityEngine;
using Zenject;
using static _Project.Constants;

namespace _Project.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private int _x = 6;
        [SerializeField] private int _y = 5;
        [SerializeField] private GridView _gridView;
        [SerializeField] private PlacesConfig _playersPlaces;
        [SerializeField] private PlacesConfig _enemiesPlaces;
        [SerializeField] private SkipButton _skipButton;
        [SerializeField] private GameOverUi _gameOverUi;

        public override void InstallBindings()
        {
            Container.Bind<GridGenerator>().AsSingle().WithArguments(_x, _y);
            Container.Bind<Grid>().FromIFactory(x => x.To<GridGenerator>().FromResolve()).AsSingle();
            Container.Bind<GridView>().FromInstance(_gridView).AsSingle();

            Container.Bind<PlacesConfig>().WithId(PLAYERS_PLACES_CONFIG).FromInstance(_playersPlaces).AsCached();
            Container.Bind<PlacesConfig>().WithId(ENEMIES_PLACES_CONFIG).FromInstance(_enemiesPlaces).AsCached();

            Container.Bind<CharactersRepository>().WithId(PLAYERS_REPOSITORY).AsCached();
            Container.Bind<CharactersRepository>().WithId(ENEMIES_REPOSITORY).AsCached();

            Container.Bind<CharactersFactory>().AsSingle();
            Container.Bind<ViewEventsQueue>().AsSingle();
            Container.Bind<CharactersViewMap>().AsSingle();
            Container.Bind<GameLoop>().AsSingle();
            Container.Bind<MessageExecutor>().AsSingle();
            Container.BindInterfacesTo<BreathFirstPathSolver>().AsSingle();
            Container.Bind<AttackClickAwaiter>().AsSingle();
            Container.BindInterfacesTo<ManualDecisionMaker>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ViewEventsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle();

            Container.BindInterfacesTo<SelectEventExecutor>().AsSingle();
            Container.BindInterfacesTo<DeselectEventExecutor>().AsSingle();
            Container.BindInterfacesTo<MoveEventExecutor>().AsSingle();
            Container.BindInterfacesTo<DieEventExecutor>().AsSingle();
            Container.BindInterfacesTo<AttackEventExecutor>().AsSingle();

            Container.Bind<SkipButton>().FromInstance(_skipButton).AsSingle();
            Container.Bind<GameOverUi>().FromInstance(_gameOverUi).AsSingle();
        }
    }
}