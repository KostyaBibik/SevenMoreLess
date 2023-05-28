using System.Collections;
using Enums;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using UniRx;
using UnityEngine;

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
            
            Observable
                .FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Waiting))
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