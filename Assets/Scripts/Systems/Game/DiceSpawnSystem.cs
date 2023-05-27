using System;
using Infrastructure.Factories.Impl;
using Infrastructure.Signals;
using Zenject;

namespace Systems.Game
{
    public class DiceSpawnSystem : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly UnitFactory _unitFactory;
        
        public DiceSpawnSystem(
            SignalBus signalBus,
            UnitFactory unitFactory
        )
        {
            _signalBus   = signalBus;
            _unitFactory = unitFactory;
        }

        private void SpawnDice(SpawnDiceSignal signal)
        {
            var type = signal.type;
            var pos = signal.pos;
            
            _unitFactory.CreateDice(type, pos);
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<SpawnDiceSignal>(SpawnDice);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SpawnDiceSignal>(SpawnDice);
        }
    }
}