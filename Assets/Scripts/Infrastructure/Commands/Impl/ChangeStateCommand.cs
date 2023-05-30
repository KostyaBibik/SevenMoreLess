using Enums;
using UniRx;

namespace Infrastructure.Commands.Impl
{
    public class ChangeStateCommand : BaseCommand
    {
        private readonly StatesStructure.StateMachine _stateMachine;
        private readonly EGameState _newState;
        
        public ChangeStateCommand(StatesStructure.StateMachine stateMachine, EGameState newState)
        {
            _stateMachine = stateMachine;
            _newState = newState;
        }

        public override void Execute()
        {
            Observable
                .FromCoroutine<EGameState>(_ => _stateMachine.ChangeState(_newState))
                .Subscribe()
                .AddTo(disposable);
        }

        public override void Undo()
        {
            disposable.Clear();
        }
    }
}