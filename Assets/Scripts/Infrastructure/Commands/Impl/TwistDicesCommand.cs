using System;
using System.Collections;
using System.Linq;
using DataBase.Dice;
using Services.Dice;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Commands.Impl
{
    public class TwistDicesCommand : BaseCommand
    {
        public readonly ReactiveProperty<int> _diceSum;
        private readonly DiceService _diceService;
        private readonly DiceTwistSettings _twistSettings;
        private readonly Subject<Unit> _completionSignal;
        public ReactiveProperty<int> DiceSum => _diceSum;
        public IObservable<Unit> CompletionSignal => _completionSignal;
        
        public TwistDicesCommand(
            DiceService diceService,
            DiceTwistSettings twistSettings
        )
        {
            _diceService = diceService;
            _twistSettings = twistSettings;
            _completionSignal = new Subject<Unit>();
            _diceSum = new ReactiveProperty<int>();
        }
        
        public override void Execute()
        {
            Observable.FromCoroutine(SimulateTwisting).Subscribe(_ =>
            {
                _completionSignal.OnNext(Unit.Default);
                _completionSignal.OnCompleted();
            });
        }

        private IEnumerator SimulateTwisting()
        {
            var dicePresenters = _diceService.DicePresenters;
            var timeTwisting = 0f;
            var targetTime = _twistSettings.TimeTwisting;
            var counters = new int[dicePresenters.Count];
            var interval = _twistSettings.IntervalSwapCounter;
            var yieldInterval = new WaitForSeconds(interval);

            do
            {
                timeTwisting += interval;

                for (var i = 0; i < dicePresenters.Count; i++)
                {
                    int GetCounter(int skipValue)
                    {
                        var newCounter = 0;
                        do
                        {
                            newCounter = Random.Range(0, dicePresenters[i].SpriteIterators.Length);
                        }
                        while (dicePresenters[i].SpriteIterators[newCounter].counter == skipValue);

                        return newCounter;
                    }

                    var oldValue = dicePresenters[i].SpriteIterators
                        .Where(iterator => iterator.counter == counters[i])
                        .Select(iterator => iterator.counter)
                        .FirstOrDefault();

                    var randomIterator = GetCounter(oldValue);
                    var counter = dicePresenters[i].SpriteIterators[randomIterator].counter;
                    dicePresenters[i].ChangeCounter(counter);
                    counters[i] = counter;
                }

                yield return yieldInterval;

            } while (targetTime > timeTwisting);
            
            _diceSum.Value = counters.Sum();
        }
        
        public override void Undo() { }
    }
}