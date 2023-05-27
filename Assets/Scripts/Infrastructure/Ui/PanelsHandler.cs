using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace Infrastructure.Ui
{
    public class PanelsHandler : MonoBehaviour
    {
        [SerializeField] protected List<Panel> Panels;

        private void Start()
        {
            InitSwaps();
        }

        private void InitSwaps()
        {
            foreach (var panel in Panels)
                panel.onSwapPanel += ActivatePanel;
        }
        
        protected void ActivatePanel(EPanelType type)
        {
            foreach (var panel in Panels)
                panel.gameObject.SetActive(panel.PanelType == type);
        }

        protected Panel GetPanel(EPanelType type)
        {
            return Panels.FirstOrDefault(panel => panel.PanelType == type);
        }
    }
}