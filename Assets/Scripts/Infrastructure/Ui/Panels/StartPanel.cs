using Enums;
using Infrastructure.Commands;
using Services.Game;
using UniRx;
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

        private ReactiveProperty<EComparisonStatus> _selectedComparison = new ReactiveProperty<EComparisonStatus>();
        private readonly CommandProcessor commandProcessor = new CommandProcessor();
        
        public override EPanelType PanelType => panelType;
        public Button TwistBtnView => twistBtnView.Button;

        private void Start()
        {
            InitComparison();
            CheckForActiveComparison();
        }

        private void InitComparison()
        {
            foreach (var button in comparisonViews)
            {
                button.Toggle.onValueChanged.AddListener(value =>
                {
                    if (value)
                    {
                        foreach (var otherButton in comparisonViews)
                        {
                            if (otherButton != button)
                            {
                                otherButton.Toggle.isOn = false;
                            }
                        }
                        _selectedComparison.Value = button.ComparisonStatus;
                    }
                    else if (button.Toggle.isOn && _selectedComparison.Value == button.ComparisonStatus)
                    {
                        button.Toggle.isOn = false;
                        _selectedComparison.Value = EComparisonStatus.None;
                    }
                });
            }

            foreach (var button in comparisonViews)
            {
                var index = button;
                _selectedComparison.Subscribe(_ =>
                {
                    index.Toggle.isOn = _selectedComparison.Value == index.ComparisonStatus;
                    index.FlagImage.sprite = index.Toggle.isOn ? index.FlagOnSelect : index.FlagOnInactive;
                    index.GlowImage.gameObject.SetActive(index.Toggle.isOn);
                });
            }

            _selectedComparison.Subscribe(_ =>
            {
                CheckForActiveComparison();
                GameMatcher.ComparisonChoice = _selectedComparison.Value;
            });
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
            _selectedComparison.Value = EComparisonStatus.None;
        }
    }
}