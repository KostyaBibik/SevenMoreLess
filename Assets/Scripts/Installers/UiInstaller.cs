using UnityEngine;
using Views.Ui;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private RollBtnView rollBtnView;
    
        public override void InstallBindings()
        {
            Container.Bind<RollBtnView>().FromInstance(rollBtnView).AsSingle();
        }
    }
}