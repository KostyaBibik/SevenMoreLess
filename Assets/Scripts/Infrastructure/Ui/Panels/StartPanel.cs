using Enums;
using UnityEngine;

namespace Infrastructure.Ui.Panels
{
    public class StartPanel : Panel
    {
        [SerializeField] private EPanelType panelType;
        public override EPanelType PanelType => panelType;
    }
}