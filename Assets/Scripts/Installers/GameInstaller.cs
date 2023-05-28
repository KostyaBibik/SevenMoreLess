using Systems.Game;
using Infrastructure.Factories.Impl;
using Infrastructure.GameInstance;
using Infrastructure.Signals;
using Services.Dice;
using Services.Game;
using UnityEngine;
using Views.Game;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneHandler sceneHandler;
        
        public override void InstallBindings()
        {
            InstallGameInstance();
            InstallGameSystems();
            InstallFactories();
            InstallServices();
            InstallSignals();
            InstallSceneHelpers();
        }

        private void InstallGameInstance()
        {
            Container.Bind<GameInstance>().AsSingle().NonLazy();
            Container.Bind<GameMatcher>().AsSingle().NonLazy();
        }

        private void InstallGameSystems()
        {
            Container.BindInterfacesAndSelfTo<DiceTwistSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DiceSpawnSystem>().AsSingle().NonLazy();
        }

        private void InstallFactories()
        {
            Container.Bind<UnitFactory>().AsSingle().NonLazy();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<DiceService>().AsSingle().NonLazy();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SpawnDiceSignal>();
            Container.DeclareSignal<TwistDicesSignal>();
            Container.DeclareSignal<ResultSignal>();
        }

        private void InstallSceneHelpers()
        {
            Container.Bind<SceneHandler>().FromInstance(sceneHandler);
        }
    }
}