using System;
using Enums;
using UnityEngine;
using Views.Ui;

namespace DataBase.Ui
{
    public class ComparisonBtnPresenter : IPresenter
    {
        private readonly ComparisonView _view;
        private readonly ComparisonBtnModel _model;

        public event Action onToggleChanged;
        
        public ComparisonBtnPresenter(
            ComparisonView view,
            ComparisonBtnModel model
        )
        {
            _view = view;
            _model = model;

            InitSubscribes();
        }

        private void InitSubscribes()
        {
            _view.Toggle.onValueChanged.AddListener(delegate
                {
                    onToggleChanged?.Invoke();
                }
            );
        }

        public void HandleComparisonChanged(EComparisonStatus newComparisonStatus)
        {
            _view.Toggle.isOn = newComparisonStatus == _view.ComparisonStatus;
            _view.FlagImage.sprite = GetFlagSprite();
            _view.GlowImage.gameObject.SetActive(_view.Toggle.isOn);
            _view.CheckMark.gameObject.SetActive(_view.Toggle.isOn);
        }

        public void SwitchToggleActivity(bool isOn)
        {
            _view.Toggle.isOn = isOn;
        }

        public EComparisonStatus GetComparisonStatus()
        {
            return _view.ComparisonStatus;
        }

        public bool GetToggleActivity()
        {
            return _view.Toggle.isOn;
        }
        
        public ComparisonView GetComparisonView()
        {
            return _view;
        }

        private Sprite GetFlagSprite()
        {
            return _view.Toggle.isOn ? _view.FlagOnSelect : _view.FlagOnInactive;
        }
    }
}