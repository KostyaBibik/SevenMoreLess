using DataBase.Dice;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(GameConfigInstaller),
        menuName = "Installers/" + nameof(GameConfigInstaller))]
    public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
    {
        [SerializeField] private DiceConfig diceConfig;
        [SerializeField] private DiceTwistSettings DiceTwistSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(diceConfig); 
            Container.BindInstance(DiceTwistSettings);
        }
    }
}