using System;
using Enums;
using UnityEngine;

namespace DataBase.Dice
{
    [CreateAssetMenu(menuName = "Config/" + nameof(DiceModelConfig),
        fileName = nameof(DiceModelConfig))]
    public class DiceModelConfig : ScriptableObject
    {
        [SerializeField] private DiceModel[] models;
        
        public DiceModel GetModel(EDiceType type)
        {
            foreach (var diceModel in models)
            {
                if (diceModel.Type == type)
                    return diceModel;
            }

            throw new Exception($"[DiceModelConfig] Can't find model with type: {type}");
        }
    }
}