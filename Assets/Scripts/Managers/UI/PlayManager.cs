using UnityEngine;

public class PlayManager : MonoBehaviour
{
  [SerializeField]
  private GameObject playPanel;
  [SerializeField]
  private GameObject pausePanel;
  [SerializeField]
  private GameObject settingsPanel;

  private PlayerInput _playerInput;

  private void Awake()
  {
    _playerInput = new PlayerInput();
    _playerInput.Player.Pause.performed += ctx => Pause();
  }

  private void OnEnable()
  {
    _playerInput.Enable();
  }

  private void OnDisable()
  {
    _playerInput.Disable();
  }

  public void Settings()
  {
    //hide pause panel
    pausePanel.SetActive(false);
    //show settings panel
    settingsPanel.SetActive(true);
  }

  public void SettingsBack()
  {
    //hide settings panel
    settingsPanel.SetActive(false);
    //show pause panel
    pausePanel.SetActive(true);
  }

  public void Pause()
  {
    if (pausePanel.activeSelf)
    {
      Resume();
    }
    else if (settingsPanel.activeSelf)
    {
      SettingsBack();
    }
    else
    {
      //pause game
      //hide play panel
      playPanel.SetActive(false);
      //show pause panel
      pausePanel.SetActive(true);
    }
  }

  public void Resume()
  {
    //hide pause menu
    pausePanel.SetActive(false);
    //show play panel
    playPanel.SetActive(true);
    //resume game
  }
}
