using System;
using System.Collections;
using Enums;
using Infrastructure.Signals;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using UniRx;
using Views.Game;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.GameInstance.GameStates
{
    public class PreparationState : BaseState
    {
        private readonly SignalBus _signalBus;
        private readonly SceneHandler _sceneHandler;
        private readonly PanelsHandler _panelsHandler;
        
        public PreparationState(
            GameInstance gameInstance,
            EGameState gameState,
            SignalBus signalBus,
            SceneHandler sceneHandler,
            PanelsHandler panelsHandler
        )
            : base(gameInstance, gameState)
        {
            _signalBus = signalBus;
            _sceneHandler = sceneHandler;
            _panelsHandler = panelsHandler;
        }
        
        public override IEnumerator Enter()
        {
            var enumArray = Enum.GetValues(typeof(EDiceType));
            var randomType = (EDiceType)enumArray.GetValue(Random.Range(1, enumArray.Length));
            
            _panelsHandler.ActivatePanel(EPanelType.GamePanel);
            
            for (var i = 0; i < _sceneHandler.Markers.Length; i++)
            {
                _signalBus.Fire(new SpawnDiceSignal
                {
                    pos = _sceneHandler.Markers[i],
                    type = randomType
                });
            }

            yield return null;
            
            Observable
                .FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Twisting))
                .Subscribe()
                .AddTo(disposable);
        }

        public override IEnumerator Exit()
        {
            disposable.Clear();
            
            yield return null;
        }
    }
}