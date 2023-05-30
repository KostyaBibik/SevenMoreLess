using System.Collections.Generic;

namespace Infrastructure.Commands
{
    public class CommandProcessor
    {
        private readonly Stack<BaseCommand> _commandStack = new Stack<BaseCommand>();
        
        public void AddCommand(BaseCommand newCommand)
        {
            newCommand.Execute();
            _commandStack.Push(newCommand);
        }
        
        public void UndoCommand()
        {
            if (_commandStack.Count <= 0)
                return;
                    
            var latestCommand = _commandStack.Pop();
            latestCommand.Undo();
        }

        public void UndoAllCommands()
        {
            while (_commandStack.Count > 0)
            {
                UndoCommand();
            }
        }
    }
}