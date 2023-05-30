using System.Collections;
using DataBase.Dice;
using Enums;
using Infrastructure.Commands.Impl;
using Infrastructure.StatesStructure;
using Services.Dice;
using UniRx;

namespace Infrastructure.GameInstance.GameStates
{
    public class TwistingState : BaseState
    {
        private readonly DiceTwistSettings _twistSettings;
        private readonly DiceService _diceService;
        private readonly Subject<Unit> _twistCompletionSignal = new Subject<Unit>();
        private readonly ITwistingCompletedObserver _twistingCompletedObserver;

        public TwistingState(
            GameInstance gameInstance,
            EGameState gameState,
            DiceTwistSettings twistSettings,
            DiceService diceService,
            ITwistingCompletedObserver twistingCompletedObserver
        ) : base(gameInstance, gameState)
        {
            _diceService = diceService;
            _twistSettings = twistSettings;
            _twistingCompletedObserver = twistingCompletedObserver;
        }
        
        public override IEnumerator Enter()
        {
            var twistDicesCommand = new TwistDicesCommand(_diceService, _twistSettings);
            twistDicesCommand.CompletionSignal.Subscribe(_ => _twistCompletionSignal.OnNext(Unit.Default));
            context.commandProcessor.AddCommand(twistDicesCommand);
            
            _twistCompletionSignal.Subscribe(_ =>
            {
                var changeStateCommand = new ChangeStateCommand(context.stateMachine, EGameState.Result);
                context.commandProcessor.AddCommand(changeStateCommand);
                _twistingCompletedObserver.OnTwistingCompleted(twistDicesCommand.DiceSum.Value);
            });

            yield return null;
        }
    }
}