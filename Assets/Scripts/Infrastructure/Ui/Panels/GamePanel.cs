using Enums;
using UnityEngine;

namespace Infrastructure.Ui.Panels
{
    public class GamePanel : Panel
    {
        [SerializeField] private EPanelType panelType;
        public override EPanelType PanelType => panelType;
    }
}