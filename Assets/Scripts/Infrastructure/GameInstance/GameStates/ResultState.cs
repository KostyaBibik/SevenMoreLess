using System;
using Enums;
using Infrastructure.Signals;
using Infrastructure.StatesStructure;
using UnityEngine;
using Zenject;

namespace Infrastructure.GameInstance.GameStates
{
    public class ResultState : BaseState, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        public ResultState(
            GameInstance gameInstance,
            EGameState gameState,
            SignalBus signalBus
            ) : base(gameInstance, gameState)
        {
            _signalBus = signalBus;

            SubscribeToSignal();
        }

        private void SubscribeToSignal()
        {
            _signalBus.Subscribe<ResultSignal>(OnHandleResultSignal);
        }

        private void OnHandleResultSignal(ResultSignal signal)
        {
            Debug.Log($"handle result signal");
            foreach (var VARIABLE in signal.counters)
            {
                Debug.Log($"{VARIABLE}");
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ResultSignal>(OnHandleResultSignal);
        }
    }
}