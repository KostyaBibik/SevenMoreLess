using UniRx;

namespace Infrastructure.Commands
{
    public abstract class BaseCommand
    {
        public CompositeDisposable disposable = new CompositeDisposable();
        public abstract void Execute();
        public abstract void Undo();
    }
}