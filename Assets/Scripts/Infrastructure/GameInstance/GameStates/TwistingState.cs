using System.Collections;
using Enums;
using Infrastructure.Signals;
using Infrastructure.StatesStructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Infrastructure.GameInstance.GameStates
{
    public class TwistingState : BaseState
    {
        private readonly SignalBus _signalBus;
        
        public TwistingState(
            GameInstance gameInstance,
            EGameState gameState,
            SignalBus signalBus
        ) : base(gameInstance, gameState)
        {
            _signalBus = signalBus;
        }
        
        public override IEnumerator Enter()
        {
            _signalBus.Fire(new TwistDicesSignal());

            Debug.Log("TwistingState Enter");
            
            yield return null;
            
            Observable.FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Result)).Subscribe();
        }

        public override IEnumerator Exit()
        {
            Debug.Log("TwistingState Exit");
            
            yield return null;
        }
    }
}