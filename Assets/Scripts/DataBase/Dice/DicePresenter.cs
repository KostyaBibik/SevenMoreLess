using Enums;
using Views.Game;

namespace DataBase.Dice
{
    public class DicePresenter : IPresenter
    {
        private readonly SpriteIterator[] _spriteIterators;
        private readonly DiceView _view;
        
        public DiceView View => _view;
        public SpriteIterator[] SpriteIterators => _spriteIterators;

        public DicePresenter(
            DiceView view,
            EDiceType type,
            SpriteIterator[] spriteIterators
        )
        {
            _view = view;
            _spriteIterators = spriteIterators;
        }

        public void ChangeCounter(int count)
        {
            for (var i = 0; i < _spriteIterators.Length; i++)
            {
                if (count != _spriteIterators[i].counter)
                    continue;
                
                var sprite = _spriteIterators[i].sprite;
                _view.SpriteRenderer.sprite = sprite;
                return;
            }
        }
    }
}