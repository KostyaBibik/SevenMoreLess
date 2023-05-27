using UnityEngine;

namespace DataBase.Dice
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DiceTwistSettings),
        fileName = nameof(DiceTwistSettings))]
    public class DiceTwistSettings : ScriptableObject
    {
        [SerializeField] private float timeTwisting;
        [SerializeField] private float intervalSwapCounter;

        public float TimeTwisting => timeTwisting;
        public float IntervalSwapCounter => intervalSwapCounter;
    }
}