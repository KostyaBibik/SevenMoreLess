using System.Collections;
using Enums;
using Infrastructure.StatesStructure;
using UniRx;
using UnityEngine;

namespace Infrastructure.GameInstance.GameStates
{
    public class LoadingState : BaseState
    {
        public LoadingState(GameInstance gameInstance, EGameState gameState) : base(gameInstance, gameState)
        {
        }
        
        public override IEnumerator Enter()
        {
            Debug.Log("LoadingState Enter");
            
            yield return null;
            
            Observable.FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Waiting)).Subscribe();
        }

        public override IEnumerator Exit()
        {
            Debug.Log("LoadingState Exit");
            
            yield return null;
        }
    }
}