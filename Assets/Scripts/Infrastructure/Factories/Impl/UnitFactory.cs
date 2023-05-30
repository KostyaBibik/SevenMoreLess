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
        private readonly DiceConfig _diceConfig;
        private readonly DiceService _diceService;

        public UnitFactory(
            DiceConfig diceConfig,
            DiceService diceService
        )
        {
            _diceConfig = diceConfig;
            _diceService = diceService;
        }
        
        public void CreateDice(
            EDiceType type,
            Vector3 pos
        )
        {
            var diceVo = _diceConfig.GetDiceVo(type);
            var diceView = DiContainerRef.Container.InstantiatePrefabForComponent<DiceView>(diceVo.Prefab);
            var diceModel = new DiceModel();
            diceModel.SetSpriteIterators(diceVo.SpriteIterators);
            diceModel.SetType(type);
            var dicePresenter = new DicePresenter(diceView, diceModel);
            var diceTransform = diceView.transform;
            diceTransform.position = pos;
            _diceService.AddEntityOnService(dicePresenter);
        }
    }
}