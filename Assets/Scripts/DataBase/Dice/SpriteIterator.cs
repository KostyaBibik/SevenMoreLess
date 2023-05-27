using System;
using UnityEngine;

namespace DataBase.Dice
{
    [Serializable]
    public struct SpriteIterator
    {
        public Sprite sprite;
        [Range(1, 6)] public int counter;
    }
}