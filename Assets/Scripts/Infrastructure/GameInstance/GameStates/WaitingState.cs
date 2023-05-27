using System.Collections;
using Enums;
using Infrastructure.StatesStructure;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Views.Ui;

namespace Infrastructure.GameInstance.GameStates
{
    public class WaitingState : BaseState
    {
        private readonly Button _twistBtn;
        
        public WaitingState(GameInstance gameInstance, EGameState gameState, RollBtnView rollBtnView) : base(gameInstance, gameState)
        {
            _twistBtn = rollBtnView.Button;
        }
        
        public override IEnumerator Enter()
        {
            Debug.Log("WaitingState Enter");
            
            _twistBtn.onClick.AddListener(OnClick);
            
            yield break;
        }

        private void OnClick()
        {
            Debug.Log("OnClick");
            Observable.FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Preparation)).Subscribe();
        }

        public override IEnumerator Exit()
        {
            Debug.Log("WaitingState Exit");
            
            yield return null;
        }
    }
}