using Enums;
using Infrastructure.GameInstance.GameStates;
using UniRx;
using Views.Game;
using Views.Ui;
using Zenject;

namespace Infrastructure.GameInstance
{
    public class GameInstance
    {
        public readonly StatesStructure.StateMachine stateMachine;
        
        public GameInstance(
            RollBtnView rollBtnView,
            SignalBus signalBus,
            SceneHandler sceneHandler
        )
        {
            stateMachine = new StatesStructure.StateMachine(
                new LoadingState(this, EGameState.Loading),
                new WaitingState(this, EGameState.Waiting, rollBtnView),
                new PreparationState(this, EGameState.Preparation, signalBus, sceneHandler),
                new TwistingState(this, EGameState.Twisting, signalBus),
                new ResultState(this, EGameState.Result, signalBus)
            );

            Observable.FromCoroutine<EGameState>(_ => stateMachine.ChangeState(EGameState.Loading)).Subscribe();
        }
    }
}