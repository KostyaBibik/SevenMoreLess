using System;
using System.Collections;
using Enums;
using Infrastructure.Commands.Impl;
using Infrastructure.Commands.Impl.GameCommands;
using Infrastructure.Factories.Impl;
using Infrastructure.StatesStructure;
using Infrastructure.Ui;
using Services.Dice;
using Views.Game;
using Random = UnityEngine.Random;

namespace Infrastructure.GameInstance.GameStates
{
    public class PreparationState : BaseState
    {
        private readonly SceneHandler _sceneHandler;
        private readonly PanelsHandler _panelsHandler;
        private readonly UnitFactory _unitFactory;
        private readonly DiceService _diceService;
        
        public PreparationState(
            GameInstance gameInstance,
            EGameState gameState,
            SceneHandler sceneHandler,
            PanelsHandler panelsHandler,
            UnitFactory factory,
            DiceService diceService
        )
            : base(gameInstance, gameState)
        {
            _sceneHandler = sceneHandler;
            _panelsHandler = panelsHandler;
            _unitFactory = factory;
            _diceService = diceService;
        }
        
        public override IEnumerator Enter()
        {
            var enumArray = Enum.GetValues(typeof(EDiceType));
            var randomType = (EDiceType)enumArray.GetValue(Random.Range(1, enumArray.Length));
            
            _panelsHandler.ActivatePanel(EPanelType.GamePanel);
            
            for (var i = 0; i < _sceneHandler.Markers.Length; i++)
            {
                var spawnDiceCommand = new SpawnDiceCommand(_unitFactory, randomType, _sceneHandler.Markers[i], _diceService);
                context.commandProcessor.AddCommand(spawnDiceCommand);
            }

            var changeStateCommand = new ChangeStateCommand(context.stateMachine, EGameState.Twisting);
            context.commandProcessor.AddCommand(changeStateCommand);
            
            yield return null;
        }
    }
}