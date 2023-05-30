using Enums;
using Infrastructure.Factories.Impl;
using Services.Dice;
using UnityEngine;

namespace Infrastructure.Commands.Impl
{
    public class SpawnDiceCommand : BaseCommand
    {
        private readonly UnitFactory _unitFactory;
        private readonly EDiceType _diceType;
        private readonly Vector3 _posSpawn;
        private readonly DiceService _diceService;

        public SpawnDiceCommand(
            UnitFactory unitFactory,
            EDiceType diceType,
            Vector3 pos,
            DiceService diceService
        )
        {
            _unitFactory = unitFactory;
            _diceType = diceType;
            _posSpawn = pos;
            _diceService = diceService;
        }
        
        public override void Execute()
        {
            _unitFactory.CreateDice(_diceType, _posSpawn);
        }

        public override void Undo()
        {
            _diceService.RemoveEntities();
        }
    }
}