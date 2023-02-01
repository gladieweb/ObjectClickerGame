using System;
using MoreMountains.Tools;
using UnityEngine.UI;

namespace DefaultNamespace.Screens
{
    public class HudScreen : ScreenCanvas
    {
        public Text pointsText;
        public MMCountdown Countdown;
        private void Start()
        {
            GameManager.OnPointsChanged += OnPointsChanged;
            RewardManager.OnTimeAdded += AddTime;
            RewardManager.OnTimeSpeedChanged += MultiplyTime;
        }

        private void OnDestroy()
        {
            GameManager.OnPointsChanged -= OnPointsChanged;
            RewardManager.OnTimeAdded -= AddTime;
            RewardManager.OnTimeSpeedChanged -= MultiplyTime;
        }

        private void OnPointsChanged(int points)
        {
            pointsText.text = points.ToString();
        }

        public void EndGame()
        {
            GameManager.Instance.EndGame();
        }

        public void AddTime(float time)
        {
            Countdown.CurrentTime += time;
        }

        public void MultiplyTime(float factor)
        {
            Countdown.CountdownSpeed = Countdown.CountdownSpeed * factor;
        }
        
    }
}