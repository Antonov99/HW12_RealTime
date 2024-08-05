using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    public class ChestView:MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI coinText;

        [SerializeField]
        private TextMeshProUGUI timerText;

        [SerializeField]
        private Button openButton;

        public event Action OnOpenButtonClicked;

        private void Awake()
        {
            openButton.onClick.AddListener(OnOpenButtonClick);
        }

        public void SetCoinText(string text)
        {
            coinText.SetText(text);
        }

        public void SetTimerText(string text)
        {
            timerText.SetText(text);
        }

        public void SetActiveOpenButton(bool value)
        {
            openButton.gameObject.SetActive(value);
        }
        
        public void SetOpenButtonInteractable(bool value)
        {
            openButton.interactable = value;
        }

        private void OnOpenButtonClick()
        {
            OnOpenButtonClicked?.Invoke();
        }
    }
}