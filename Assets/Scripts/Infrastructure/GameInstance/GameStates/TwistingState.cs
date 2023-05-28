using System.Collections;
using Enums;
using Infrastructure.Signals;
using Infrastructure.StatesStructure;
using UniRx;
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
            
            yield return null;

            Observable
                .FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Result))
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