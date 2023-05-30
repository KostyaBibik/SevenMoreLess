using System;
using Enums;
using UnityEngine;
using Views.Game;

namespace DataBase.Dice
{
    [Serializable]
    public struct DiceVo
    {
        [SerializeField] private EDiceType type;
        [SerializeField] private DiceView prefab;
        [SerializeField] private SpriteIterator[] spriteIterators;
            
        public EDiceType Type => type;
        public DiceView Prefab => prefab;
        public SpriteIterator[] SpriteIterators => spriteIterators;
    }
}