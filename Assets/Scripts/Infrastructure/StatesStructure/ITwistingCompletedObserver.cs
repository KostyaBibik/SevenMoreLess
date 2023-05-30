namespace Infrastructure.StatesStructure
{
    public interface ITwistingCompletedObserver
    {
        void OnTwistingCompleted(int diceSum);
    }
}