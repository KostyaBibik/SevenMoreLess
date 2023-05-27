using System;
using Enums;
using UnityEngine;

namespace Infrastructure.Ui
{
    public abstract class Panel : MonoBehaviour
    {
        public virtual EPanelType PanelType { get; }
        public Action<EPanelType> onSwapPanel;
    }
}