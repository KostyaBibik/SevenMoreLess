using Enums;
using UniRx;
using Views.Ui;

namespace Infrastructure.Commands.Impl.UiCommands
{
    public class ComparisonCommand : BaseCommand
    {
        private bool _previousValue;
        private readonly ComparisonView _button;
        private readonly ReactiveProperty<EComparisonStatus> _selectedComparison;
        private readonly ComparisonView[] _comparisonViews;
        
        public ComparisonCommand(
            ComparisonView button,
            ReactiveProperty<EComparisonStatus> selectedComparison,
            ComparisonView[] comparisonViews
        )
        {
            _previousValue = button.Toggle.isOn;
            _button = button;
            _selectedComparison = selectedComparison;
            _comparisonViews = comparisonViews;
        }

        public override void Execute()
        {
            if (_button.Toggle.isOn)
            {
                foreach (var otherButton in _comparisonViews)
                {
                    if (otherButton != _button)
                    {
                        otherButton.Toggle.isOn = false;
                    }
                }
                _selectedComparison.Value = _button.ComparisonStatus;
            }
            else if (_previousValue && _selectedComparison.Value == _button.ComparisonStatus)
            {
                _button.Toggle.isOn = false;
                _selectedComparison.Value = EComparisonStatus.None;
            }

            _previousValue = _button.Toggle.isOn;
        }

        public override void Undo()
        {
            _button.Toggle.isOn = _previousValue;

            if (_previousValue)
            {
                _selectedComparison.Value = _button.ComparisonStatus;
            }
            else if (_selectedComparison.Value == _button.ComparisonStatus)
            {
                _selectedComparison.Value = EComparisonStatus.None;
            }
        }
    }
}