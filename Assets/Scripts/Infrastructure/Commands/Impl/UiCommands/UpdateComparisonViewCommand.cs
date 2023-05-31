using DataBase.Ui;
using Enums;
using UniRx;

namespace Infrastructure.Commands.Impl.UiCommands
{
    public class UpdateComparisonViewCommand : BaseCommand
    {
        private readonly ComparisonBtnPresenter _comparisonBtnPresenter;
        private readonly ReactiveProperty<EComparisonStatus> _selectedComparison;
        
        public UpdateComparisonViewCommand(
            ComparisonBtnPresenter comparisonBtnPresenter,
            ReactiveProperty<EComparisonStatus> selectedComparison
        )
        {
            _comparisonBtnPresenter = comparisonBtnPresenter;
            _selectedComparison = selectedComparison;
        }

        public override void Execute()
        {
            _comparisonBtnPresenter.HandleComparisonChanged(_selectedComparison.Value);
        }

        public override void Undo() { }
    }
}