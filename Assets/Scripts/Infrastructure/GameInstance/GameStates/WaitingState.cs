using System.Collections;
using Enums;
using Infrastructure.Commands.Impl;
using Infrastructure.Commands.Impl.GameCommands;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using Infrastructure.Ui.Panels;
using UnityEngine.UI;

namespace Infrastructure.GameInstance.GameStates
{
    public class WaitingState : BaseState
    {
        private readonly Button _twistBtn;
        
        public WaitingState(
            GameInstance gameInstance,
            EGameState gameState,
            PanelsHandler panelsHandler
        ) : base(gameInstance, gameState)
        {
            var startPanel = (StartPanel)panelsHandler.GetPanel(EPanelType.StartPanel);
            
            _twistBtn = startPanel.TwistBtnView;
        }
        
        public override IEnumerator Enter()
        {
            var twistButtonCommand = new TwistButtonCommand(_twistBtn, context);
            context.commandProcessor.AddCommand(twistButtonCommand);
            
            yield break;
        }
    }
}