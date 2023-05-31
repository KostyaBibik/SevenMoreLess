using System.Collections.Generic;
using DataBase.Ui;
using Enums;
using Infrastructure.Commands;
using Infrastructure.Commands.Impl.UiCommands;
using Services.Game;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views.Ui;

namespace Infrastructure.Ui.Panels
{
    public class StartPanel : Panel
    {
        [SerializeField] private EPanelType panelType;
        [SerializeField] private TwistBtnView twistBtnView;
        [Space] [SerializeField] private ComparisonView[] comparisonViews;

        private readonly ReactiveProperty<EComparisonStatus> _selectedComparison = new ReactiveProperty<EComparisonStatus>();
        private readonly List<ComparisonBtnPresenter> _comparisonBtnPresenters = new List<ComparisonBtnPresenter>();
        private readonly CommandProcessor commandProcessor = new CommandProcessor();
        
        public override EPanelType PanelType => panelType;
        public Button TwistBtnView => twistBtnView.Button;

        private void Start()
        {
            InitComparison();
            CheckForActiveComparison();
        }

        private void InitComparison()
        {
            foreach (var comparisonView in comparisonViews)
            {
                var comparisonBtnModel = new ComparisonBtnModel();
                var comparisonBtnPresenter = new ComparisonBtnPresenter(comparisonView, comparisonBtnModel);
                _comparisonBtnPresenters.Add(comparisonBtnPresenter);
            }

            foreach (var comparisonBtnPresenter in _comparisonBtnPresenters)
            {
                var comparisonCommand = new ComparisonCommand(comparisonBtnPresenter, _selectedComparison, comparisonViews);
                comparisonBtnPresenter.onToggleChanged += delegate { commandProcessor.AddCommand(comparisonCommand); };
            }

            foreach (var comparisonBtnPresenter in _comparisonBtnPresenters)
            {
                var updateComparisonViewCommand = new UpdateComparisonViewCommand(comparisonBtnPresenter, _selectedComparison);
                _selectedComparison.Subscribe(_ =>
                {
                    commandProcessor.AddCommand(updateComparisonViewCommand);
                });
            }

            _selectedComparison.Subscribe(_ =>
            {
                CheckForActiveComparison();
                GameMatcher.ComparisonChoice = _selectedComparison.Value;
            });
        }
        
        private void CheckForActiveComparison()
        {
            twistBtnView.Button.interactable = _selectedComparison.Value != EComparisonStatus.None;
        }

        public void ResetToggleGroup()
        {
            _selectedComparison.Value = EComparisonStatus.None;
        }
    }
}