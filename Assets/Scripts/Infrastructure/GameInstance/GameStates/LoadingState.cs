using System.Collections;
using Enums;
using Infrastructure.Commands.Impl;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;

namespace Infrastructure.GameInstance.GameStates
{
    public class LoadingState : BaseState
    {
        private readonly PanelsHandler _panelsHandler;
        
        public LoadingState(
            GameInstance gameInstance,
            EGameState gameState,
            PanelsHandler panelsHandler
        ) : base(gameInstance, gameState)
        {
            _panelsHandler = panelsHandler;
        }
        
        public override IEnumerator Enter()
        {
            _panelsHandler.ActivatePanel(EPanelType.StartPanel);
            
            yield return null;
            
            var changeStateCommand = new ChangeStateCommand(context.stateMachine, EGameState.Waiting);
            context.commandProcessor.AddCommand(changeStateCommand);
        }
    }
}