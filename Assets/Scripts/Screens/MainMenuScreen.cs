using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Screens
{
    public class MainMenuScreen : ScreenCanvas
    {
        public Text currentDifficultyLabel;
        public void PlayGame()
        {
            GameManager.Instance.StartGame();
        }

        public void SetDifficultyLevel(GameDifficultSetting setting)
        {
            GameManager.Instance.currentSettings = setting;
            currentDifficultyLabel.text = setting.ID;
        }
    }
}