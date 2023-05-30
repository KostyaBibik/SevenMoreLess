using System;
using Enums;
using Infrastructure.Ui;
using Infrastructure.Ui.Panels;
using Services.Game;
using UniRx;

namespace Infrastructure.Commands.Impl.GameCommands
{
    public class ProcessResultCommand : BaseCommand
    {
        private readonly GameInstance.GameInstance _gameInstance;
        private readonly PanelsHandler _panelsHandler;
        private readonly ReactiveProperty<int> _diceSum;

        public ProcessResultCommand(GameInstance.GameInstance gameInstance, PanelsHandler panelsHandler, ReactiveProperty<int> diceSum)
        {
            _gameInstance = gameInstance;
            _panelsHandler = panelsHandler;
            _diceSum = diceSum;
        }
        
        public override void Execute()
        {
            var resultPanelType = EPanelType.ResultPanel;
            _panelsHandler.ActivatePanel(resultPanelType);
            var resultPanel = (ResultPanel)_panelsHandler.GetPanel(resultPanelType);
            var sum = _diceSum.Value;
            var correctStatus = false;
            resultPanel.SetSumCount(sum);

            switch (GameMatcher.ComparisonChoice)
            {
                case EComparisonStatus.EqualToSeven:
                    if (sum == 7)
                        correctStatus = true;
                    break;

                case EComparisonStatus.LessThanSeven:
                    if (sum < 7)
                        correctStatus = true;
                    break;
                case EComparisonStatus.GreaterThanSeven:
                    if (sum > 7)
                        correctStatus = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            resultPanel.SetResultStatus(correctStatus);
            resultPanel.RetryBtn.onClick.AddListener(OnRetry);
        }

        private void OnRetry()
        {
            var changeStateCommand = new ChangeStateCommand(_gameInstance.stateMachine, EGameState.Reset);
            _gameInstance.commandProcessor.AddCommand(changeStateCommand);
        }

        public override void Undo()
        {
            
        }
    }
}