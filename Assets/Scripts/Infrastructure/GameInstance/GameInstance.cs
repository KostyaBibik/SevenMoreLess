using DataBase.Dice;
using Enums;
using Infrastructure.Commands;
using Infrastructure.Commands.Impl;
using Infrastructure.Factories.Impl;
using Infrastructure.GameInstance.GameStates;
using Infrastructure.Ui;
using Services.Dice;
using Views.Game;

namespace Infrastructure.GameInstance
{
    public class GameInstance
    {
        public readonly StatesStructure.StateMachine stateMachine;
        public readonly CommandProcessor commandProcessor;
        
        public GameInstance(
            SceneHandler sceneHandler,
            PanelsHandler panelsHandler,
            DiceService diceService,
            UnitFactory unitFactory, 
            DiceTwistSettings twistSettings
        )
        {
            commandProcessor = new CommandProcessor();
            
            var loadingState = new LoadingState(this, EGameState.Loading, panelsHandler);
            var waitingState = new WaitingState(this, EGameState.Waiting, panelsHandler);
            var preparationState = new PreparationState(this, EGameState.Preparation, sceneHandler, panelsHandler, unitFactory, diceService);
            var resultState = new ResultState(this, EGameState.Result, panelsHandler);
            var twistingState = new TwistingState(this, EGameState.Twisting, twistSettings, diceService, resultState);
            var resetState =  new ResetState(this, EGameState.Reset, diceService, panelsHandler);
            
            stateMachine = new StatesStructure.StateMachine(
                loadingState,
                waitingState,
                preparationState,
                resultState,
                twistingState,
                resetState
            );

            var changeStateCommand = new ChangeStateCommand(stateMachine, EGameState.Loading);
            commandProcessor.AddCommand(changeStateCommand);
        }
    }
}