using Enums;
using UnityEngine.UI;

namespace Infrastructure.Commands.Impl.GameCommands
{
    public class TwistButtonCommand : BaseCommand
    {
        private readonly Button _twistBtn;
        private readonly GameInstance.GameInstance _context;

        public TwistButtonCommand(Button twistBtn, GameInstance.GameInstance context)
        {
            _twistBtn = twistBtn;
            _context = context;
        }
        
        public override void Execute()
        {
            _twistBtn.onClick.AddListener(OnClick);
        }

        public override void Undo()
        {
            _twistBtn.onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            var spawnDiceCommand = new ChangeStateCommand(_context.stateMachine, EGameState.Preparation);
            _context.commandProcessor.AddCommand(spawnDiceCommand);
        }
    }
}