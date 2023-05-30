using System;
using Enums;
using UnityEngine;

namespace DataBase.Dice
{
    [CreateAssetMenu(menuName = "Config/" + nameof(DiceConfig),
        fileName = nameof(DiceConfig))]
    public class DiceConfig : ScriptableObject
    {
        [SerializeField] private DiceVo[] diceVos;
        
        public DiceVo GetDiceVo(EDiceType type)
        {
            foreach (var diceModel in diceVos)
            {
                if (diceModel.Type == type)
                    return diceModel;
            }

            throw new Exception($"[DiceConfig] Can't find vo with type: {type}");
        }
    }
}