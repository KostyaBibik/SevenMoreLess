using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Ui
{
    public class ComparisonView : MonoBehaviour
    {
        [SerializeField] private EComparisonStatus comparisonStatus;
        [SerializeField] private Toggle toggle;

        public EComparisonStatus ComparisonStatus => comparisonStatus;
        public Toggle Toggle => toggle;
    }
}