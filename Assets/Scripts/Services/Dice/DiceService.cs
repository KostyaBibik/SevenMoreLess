using System.Collections.Generic;
using DataBase;
using DataBase.Dice;
using UnityEngine;

namespace Services.Dice
{
    public class DiceService
    {
        public List<DicePresenter> DicePresenters { get; } = new();
        
        public void AddEntityOnService(IPresenter entityView)
        {
            DicePresenters.Add((DicePresenter) entityView);
        }

        public void RemoveEntities()
        {
            for (var i = 0; i < DicePresenters.Count; i++)
            {
                Object.Destroy(DicePresenters[i].View.gameObject);
            }
            DicePresenters.Clear();
        }
    }
}