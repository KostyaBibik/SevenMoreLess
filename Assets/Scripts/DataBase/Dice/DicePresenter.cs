using System.Linq;
using UniRx;
using UnityEngine;
using Views.Game;

namespace DataBase.Dice
{
    public class DicePresenter : IPresenter
    {
        private readonly DiceView _view;
        private readonly DiceModel _model;
        
        public DiceView View => _view;
        public SpriteIterator[] SpriteIterators => _model.SpriteIterators.ToArray();

        public DicePresenter(
            DiceView view,
            DiceModel model
        )
        {
            _view = view;
            _model = model;
            
            BindChangeModelCounter();
        }
        
        public void ChangeCounter(int count)
        {
            _model.SetCountValue(count);
        }

        private void BindChangeModelCounter()
        {
            _model.Counter.Subscribe(counter =>
            {
                var matchingSprite = FindMatchingSprite(counter);
                if (matchingSprite != null)
                {
                    _view.SpriteRenderer.sprite = matchingSprite;
                }
            });
        }
        
        private Sprite FindMatchingSprite(int counter)
        {
            return (from iterator in SpriteIterators where iterator.counter == counter select iterator.sprite).FirstOrDefault();
        }
    }
}