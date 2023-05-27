using System;
using System.Collections;
using System.Linq;
using DataBase.Dice;
using Infrastructure.Signals;
using Services.Dice;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Systems.Game
{
    public class DiceTwistSystem : IInitializable, IDisposable
    {
        private readonly DiceService _diceService;
        private readonly SignalBus _signalBus;
        private readonly DiceTwistSettings _twistSettings;

        public DiceTwistSystem(
            SignalBus signalBus,
            DiceService diceService,
            DiceTwistSettings diceTwistSettings
        )
        {
            _signalBus     = signalBus;
            _diceService   = diceService;
            _twistSettings = diceTwistSettings;
        }

        private void TwistDices(TwistDicesSignal signal)
        {
            Observable.FromCoroutine(SimulateTwisting).Subscribe();
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
                timeTwisting += 1f;
                
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
            
            _signalBus.Fire(new ResultSignal
            {
                counters = counters
            });
        }

        public void Initialize()
        {
            _signalBus.Subscribe<TwistDicesSignal>(TwistDices);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<TwistDicesSignal>(TwistDices);
        }
    }
}