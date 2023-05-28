using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Ui.Panels
{
    public class ResultPanel : Panel
    {
        [SerializeField] private EPanelType panelType;
        [SerializeField] private TMP_Text sumCounter;
        [SerializeField] private TMP_Text labelResult;
        [SerializeField] private Button retryBtn;
         
        private const string winText = "You win!";
        private const string loseText = "You lose!";

        public override EPanelType PanelType => panelType;
        public Button RetryBtn => retryBtn;
        
        public void SetSumCount(int count)
        {
            sumCounter.text = count.ToString();
        }

        public void SetResultStatus(bool flag)
        {
            labelResult.text = flag ? winText : loseText;
        }
    }
}