using Controllers.UI;
using UnityEngine;

namespace Managers.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneController.SwitchScene("PlayScene");
        }

        public void Settings()
        {
            SceneController.SwitchScene("SettingsMenuScene");
        }

        public void Quit()
        {
            SceneController.Quit();
        }
    }
}