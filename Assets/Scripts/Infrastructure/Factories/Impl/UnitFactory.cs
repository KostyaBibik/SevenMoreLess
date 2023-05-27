using DataBase.Dice;
using Enums;
using Infrastructure.GameInstance;
using Services.Dice;
using UnityEngine;
using Views.Game;

namespace Infrastructure.Factories.Impl
{
    public class UnitFactory : IFactory
    {
        private readonly DiceModelConfig _diceModelConfig;
        private readonly DiceService _diceService;

        public UnitFactory(
            DiceModelConfig diceModelConfig,
            DiceService diceService
        )
        {
            _diceModelConfig = diceModelConfig;
            _diceService = diceService;
        }
        
        public void CreateDice(
            EDiceType type,
            Vector3 pos
        )
        {
            var model = _diceModelConfig.GetModel(type);
            var diceView = DiContainerRef.Container.InstantiatePrefabForComponent<DiceView>(model.Prefab);
            var dicePresenter = new DicePresenter(diceView, type, model.SpriteIterators);
            var diceTransform = diceView.transform;
            diceTransform.position = pos;
            
            _diceService.AddEntityOnService(dicePresenter);
        }
    }
}