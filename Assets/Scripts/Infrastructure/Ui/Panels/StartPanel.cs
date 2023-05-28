using Enums;
using Services.Game;
using UnityEngine;
using UnityEngine.UI;
using Views.Ui;

namespace Infrastructure.Ui.Panels
{
    public class StartPanel : Panel
    {
        [SerializeField] private EPanelType panelType;
        [SerializeField] private TwistBtnView twistBtnView;
        [Space] [SerializeField] private ComparisonView[] comparisonViews;

        public override EPanelType PanelType => panelType;
        public Button TwistBtnView => twistBtnView.Button;

        private void Start()
        {
            InitComparison();
            CheckForActiveComparison();
        }

        private void InitComparison()
        {
            for (var i = 0; i < comparisonViews.Length; i++)
            {
                var index = i;
                comparisonViews[index].Toggle.onValueChanged.AddListener(delegate(bool flag)
                {
                    CheckForActiveComparison();
                    
                    if(!flag) 
                        return;
                    
                    GameMatcher.ComparisonChoice = comparisonViews[index].ComparisonStatus;
                });
            }
        }

        private void CheckForActiveComparison()
        {
            var activeStatus = false;
            
            for (var i = 0; i < comparisonViews.Length; i++)
            {
                if (!comparisonViews[i].Toggle.isOn) 
                    continue;
                
                activeStatus = true;
                break;
            }
            
            twistBtnView.Button.interactable = activeStatus;
        }

        public void ResetToggleGroup()
        {
            for (var index = 0; index < comparisonViews.Length; index++)
            {
                comparisonViews[index].Toggle.isOn = false;
            }
        }
    }
}