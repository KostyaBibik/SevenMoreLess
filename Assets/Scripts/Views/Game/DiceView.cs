using UnityEngine;

namespace Views.Game
{
    public class DiceView : MonoBehaviour, IEntityView
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }
}