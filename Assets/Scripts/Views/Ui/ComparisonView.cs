using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Ui
{
    public class ComparisonView : MonoBehaviour
    {
        [SerializeField] private EComparisonStatus comparisonStatus;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Sprite flagOnSelect;
        [SerializeField] private Sprite flagOnInactive;
        [SerializeField] private Image flagImage;
        [SerializeField] private Image glowImage;
        
        public Sprite FlagOnSelect => flagOnSelect;
        public Sprite FlagOnInactive => flagOnInactive;
        public Image FlagImage => flagImage;
        public Image GlowImage => glowImage;
        public EComparisonStatus ComparisonStatus => comparisonStatus;
        public Toggle Toggle => toggle;
    }
}