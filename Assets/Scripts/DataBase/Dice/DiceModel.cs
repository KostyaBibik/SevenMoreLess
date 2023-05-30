using Enums;
using UniRx;

namespace DataBase.Dice
{
    public class DiceModel
    {
        private readonly ReactiveProperty<EDiceType> _type = new ReactiveProperty<EDiceType>();
        private readonly ReactiveProperty<int> _count = new ReactiveProperty<int>();
        private readonly ReactiveCollection<SpriteIterator> _spriteIterators = new ReactiveCollection<SpriteIterator>();

        public IReadOnlyReactiveProperty<int> Counter => _count;
        public IReadOnlyReactiveCollection<SpriteIterator> SpriteIterators => _spriteIterators;

        public void SetType(EDiceType type)
        {
            _type.Value = type;
        }

        public void SetCountValue(int count)
        {
            _count.Value = count;
        }
        
        public void SetSpriteIterators(SpriteIterator[] spriteIterators)
        {
            for (var i = 0; i < spriteIterators.Length; i++)
            {
                _spriteIterators.Add(spriteIterators[i]);
            }
        }
    }
}