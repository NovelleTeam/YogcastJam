using Controllers.UI;
using UnityEngine;

namespace Managers.UI
{
  public class MainMenuManager : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup _mainMenuButtons;
    [Range(0.5f, 10.0f), SerializeField]
    private float _targetAlpha = 1.0f;
    [Range(0.5f, 10.0f), SerializeField]
    private float _fadeDuration = 1.0f;

    private void Start()
    {
      DOTweenController.CanvasGroupAlpha(_mainMenuButtons, _targetAlpha, _fadeDuration);
    }

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