using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    [Serializable]
    public class ChestAnimation
    {
        [SerializeField]
        private Sprite chestOpened;

        [SerializeField]
        private Sprite chestClosed;

        [SerializeField]
        private Image img;
        
        public void Open()
        {
            img.sprite = chestOpened;
        }

        public void Close()
        {
            img.sprite = chestClosed;
        }
    }
}