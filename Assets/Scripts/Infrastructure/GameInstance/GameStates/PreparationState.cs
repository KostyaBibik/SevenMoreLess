using System;
using System.Collections;
using Enums;
using Infrastructure.Signals;
using Infrastructure.StatesStructure;
using UniRx;
using UnityEngine;
using Views.Game;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.GameInstance.GameStates
{
    public class PreparationState : BaseState
    {
        private readonly SignalBus _signalBus;
        private readonly SceneHandler _sceneHandler;
        
        public PreparationState(
            GameInstance gameInstance,
            EGameState gameState,
            SignalBus signalBus,
            SceneHandler sceneHandler
        )
            : base(gameInstance, gameState)
        {
            _signalBus = signalBus;
            _sceneHandler = sceneHandler;
        }
        
        public override IEnumerator Enter()
        {
            var enumArray = Enum.GetValues(typeof(EDiceType));
            var randomType = (EDiceType)enumArray.GetValue(Random.Range(1, enumArray.Length));
            
            for (var i = 0; i < _sceneHandler.Markers.Length; i++)
            {
                _signalBus.Fire(new SpawnDiceSignal
                {
                    pos = _sceneHandler.Markers[i],
                    type = randomType
                });
            }

            Debug.Log("SimulatingState Enter");

            yield return null;
            
            Observable.FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Twisting)).Subscribe();
        }

        public override IEnumerator Exit()
        {
            Debug.Log("SimulatingState Exit");
            
            yield return null;
        }
    }
}