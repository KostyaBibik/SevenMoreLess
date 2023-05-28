using System.Collections;
using Enums;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using Infrastructure.Ui.Panels;
using UniRx;
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
            _twistBtn.onClick.AddListener(OnClick);
            
            yield break;
        }

        private void OnClick()
        {
            Observable
                .FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Preparation))
                .Subscribe()
                .AddTo(disposable);
        }

        public override IEnumerator Exit()
        {
            _twistBtn.onClick.RemoveListener(OnClick);
            disposable.Clear();
            
            yield return null;
        }
    }
}