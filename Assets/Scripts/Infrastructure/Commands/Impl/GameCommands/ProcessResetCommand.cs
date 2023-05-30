using Enums;
using Infrastructure.Ui;
using Infrastructure.Ui.Panels;
using Services.Dice;

namespace Infrastructure.Commands.Impl.GameCommands
{
    public class ProcessResetCommand : BaseCommand
    {
        private readonly DiceService _diceService;
        private readonly PanelsHandler _panelsHandler;
        private readonly GameInstance.GameInstance _context;
        
        public ProcessResetCommand(
            DiceService diceService,
            PanelsHandler panelsHandler,
            GameInstance.GameInstance context
        )
        {
            _diceService = diceService;
            _panelsHandler = panelsHandler;
            _context = context;
        }
        
        public override void Execute()
        {
            _diceService.RemoveEntities();
            var startPanel = (StartPanel)_panelsHandler.GetPanel(EPanelType.StartPanel);
            startPanel.ResetToggleGroup();
            
            _context.commandProcessor.UndoAllCommands();
        }

        public override void Undo()
        {
            
        }
    }
}