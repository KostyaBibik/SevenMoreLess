using System.Collections;
using Enums;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using Infrastructure.Ui.Panels;
using Services.Dice;
using UniRx;

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
            _diceService.RemoveEntities();
            var startPanel = (StartPanel)_panelsHandler.GetPanel(EPanelType.StartPanel);
            startPanel.ResetToggleGroup();
            
            Observable
                .FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Loading))
                .Subscribe()
                .AddTo(disposable);
            
            yield break;
        }

        public override IEnumerator Exit()
        {
            disposable.Clear();
            
            yield return null;
        }
    }
}