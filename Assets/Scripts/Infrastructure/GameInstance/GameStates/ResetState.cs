using System.Collections;
using Enums;
using Infrastructure.Commands.Impl;
using Infrastructure.Commands.Impl.GameCommands;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using Services.Dice;

namespace Infrastructure.GameInstance.GameStates
{
    public class ResetState : BaseState
    {
        private readonly DiceService _diceService;
        private readonly PanelsHandler _panelsHandler;

        public ResetState(
            GameInstance gameInstance,
            EGameState gameState,
            DiceService diceService,
            PanelsHandler panelsHandler
        )
            : base(gameInstance, gameState)
        {
            _diceService = diceService;
            _panelsHandler = panelsHandler;
        }
        
        public override IEnumerator Enter()
        {
            var processResetCommand = new ProcessResetCommand(_diceService, _panelsHandler, context);
            context.commandProcessor.AddCommand(processResetCommand);
            
            var changeStateCommand = new ChangeStateCommand(context.stateMachine, EGameState.Loading);
            context.commandProcessor.AddCommand(changeStateCommand);
            
            yield break;
        }
    }
}