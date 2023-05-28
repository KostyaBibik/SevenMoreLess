using Infrastructure.Ui;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private PanelsHandler panelsHandler;
        
        public override void InstallBindings()
        {
            Container.Bind<PanelsHandler>().FromInstance(panelsHandler).AsSingle();
        }
    }
}