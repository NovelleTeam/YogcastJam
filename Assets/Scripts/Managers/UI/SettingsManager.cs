using Controllers.UI;
using TMPro;
using UnityEngine;

namespace Managers.UI
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeText;

        public void Start()
        {
            volumeSlider.value = 1;
        }

        public void MainMenu()
        {
            SceneController.SwitchScene("MainMenuScene");
        }

        public void ChangeVolume()
        {
            volumeText.text = ((int) (volumeSlider.value * 100)).ToString();
        }
    }
}