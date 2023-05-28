using System;
using System.Linq;
using Enums;
using Infrastructure.Signals;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using Infrastructure.Ui.Panels;
using Services.Game;
using UniRx;
using Zenject;

namespace Infrastructure.GameInstance.GameStates
{
    public class ResultState : BaseState, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly PanelsHandler _panelsHandler;
        
        public ResultState(
            GameInstance gameInstance,
            EGameState gameState,
            SignalBus signalBus,
            PanelsHandler panelsHandler
            ) : base(gameInstance, gameState)
        {
            _signalBus = signalBus;
            _panelsHandler = panelsHandler;

            SubscribeToSignal();
        }

        private void SubscribeToSignal()
        {
            _signalBus.Subscribe<ResultSignal>(OnHandleResultSignal);
        }

        private void OnHandleResultSignal(ResultSignal signal)
        {
            var resultPanelType = EPanelType.ResultPanel;
            _panelsHandler.ActivatePanel(resultPanelType);
            var resultPanel = (ResultPanel)_panelsHandler.GetPanel(resultPanelType);
            var sum = signal.counters.Sum();
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
            Observable.FromCoroutine<EGameState>(_ => context.stateMachine.ChangeState(EGameState.Reset)).Subscribe();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ResultSignal>(OnHandleResultSignal);
        }
    }
}