using DataBase.Ui;
using Enums;
using UniRx;
using Views.Ui;

namespace Infrastructure.Commands.Impl.UiCommands
{
    public class ComparisonCommand : BaseCommand
    {
        private bool _previousValue;
        private readonly ComparisonBtnPresenter _comparisonBtnPresenter;
        private readonly ReactiveProperty<EComparisonStatus> _selectedComparison;
        private readonly ComparisonView[] _comparisonViews;
        
        public ComparisonCommand(
            ComparisonBtnPresenter comparisonBtnPresenter,
            ReactiveProperty<EComparisonStatus> selectedComparison,
            ComparisonView[] comparisonViews
        )
        {
            _previousValue = comparisonBtnPresenter.GetToggleActivity();
            _comparisonBtnPresenter = comparisonBtnPresenter;
            _selectedComparison = selectedComparison;
            _comparisonViews = comparisonViews;
        }

        public override void Execute()
        {
            if (_comparisonBtnPresenter.GetToggleActivity())
            {
                foreach (var otherButton in _comparisonViews)
                {
                    if (otherButton != _comparisonBtnPresenter.GetComparisonView())
                    {
                        otherButton.Toggle.isOn = false;
                    }
                }
                _selectedComparison.Value = _comparisonBtnPresenter.GetComparisonStatus();
            }
            else if (_previousValue && _selectedComparison.Value == _comparisonBtnPresenter.GetComparisonStatus())
            {
                _comparisonBtnPresenter.SwitchToggleActivity(false);
                _selectedComparison.Value = EComparisonStatus.None;
            }

            _previousValue = _comparisonBtnPresenter.GetToggleActivity();
        }

        public override void Undo()
        {
        }
    }
}