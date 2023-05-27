using System.Collections.Generic;
using DataBase;
using DataBase.Dice;

namespace Services.Dice
{
    public class DiceService
    {
        public List<DicePresenter> DicePresenters { get; } = new();
        
        public void AddEntityOnService(IPresenter entityView)
        {
            DicePresenters.Add((DicePresenter) entityView);
        }
    }
}