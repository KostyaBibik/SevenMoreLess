using System.Collections;
using Enums;
using Infrastructure.Commands.Impl;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using UniRx;

namespace Infrastructure.GameInstance.GameStates
{
    public class ResultState : BaseState, ITwistingCompletedObserver
    {
        private readonly PanelsHandler _panelsHandler;
        private readonly ReactiveProperty<int> _diceSum;
        
        public ResultState(
            GameInstance gameInstance,
            EGameState gameState,
            PanelsHandler panelsHandler
            ) : base(gameInstance, gameState)
        {
            _panelsHandler = panelsHandler;
            _diceSum = new ReactiveProperty<int>();
        }

        public void OnTwistingCompleted(int diceSum)
        {
            _diceSum.Value = diceSum;
        }
        
        public override IEnumerator Enter()
        {
            var processResultCommand = new ProcessResultCommand(context, _panelsHandler, _diceSum);
            context.commandProcessor.AddCommand(processResultCommand);
            
            yield return null;
        }
        
        public override IEnumerator Exit()
        {
            yield return null;
        }
    }
}