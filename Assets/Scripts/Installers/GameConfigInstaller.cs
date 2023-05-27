using DataBase.Dice;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(GameConfigInstaller),
        menuName = "Installers/" + nameof(GameConfigInstaller))]
    public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
    {
        [SerializeField] private DiceModelConfig diceModelConfig;
        [SerializeField] private DiceTwistSettings DiceTwistSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(diceModelConfig); 
            Container.BindInstance(DiceTwistSettings);
        }
    }
}