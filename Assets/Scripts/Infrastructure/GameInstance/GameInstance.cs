using Enums;
using Infrastructure.GameInstance.GameStates;
using Infrastructure.Ui;
using Services.Dice;
using UniRx;
using Views.Game;
using Zenject;

namespace Infrastructure.GameInstance
{
    public class GameInstance
    {
        public readonly StatesStructure.StateMachine stateMachine;
        
        public GameInstance(
            SignalBus signalBus,
            SceneHandler sceneHandler,
            PanelsHandler panelsHandler,
            DiceService diceService
        )
        {
            stateMachine = new StatesStructure.StateMachine(
                new LoadingState(this, EGameState.Loading, panelsHandler),
                new WaitingState(this, EGameState.Waiting, panelsHandler),
                new PreparationState(this, EGameState.Preparation, signalBus, sceneHandler, panelsHandler),
                new TwistingState(this, EGameState.Twisting, signalBus),
                new ResultState(this, EGameState.Result, signalBus, panelsHandler),
                new ResetState(this, EGameState.Reset, diceService, panelsHandler)
            );

            Observable.FromCoroutine<EGameState>(_ => stateMachine.ChangeState(EGameState.Loading)).Subscribe();
        }
    }
}