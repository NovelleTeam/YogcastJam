using System.Collections;
using System.Collections.Generic;
using Controllers.Interactive;
using Controllers.Player.Upgrades;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        private PlayerInput _controls;
        
        // panels
        [SerializeField] private CanvasGroup mainPanel;
        [SerializeField] private CanvasGroup fadePanel;
        [SerializeField] private CanvasGroup chestPanel;
        [SerializeField] private CanvasGroup pausePanel;
        [SerializeField] private CanvasGroup deathPanel;

        [SerializeField] private GameObject harts;
        [SerializeField] private GameObject hart;
        [SerializeField] private GameObject hartPlaceHolder;

        // texts
        [SerializeField] private CanvasGroup onceAponATime;
        [SerializeField] private CanvasGroup inAGalaxyFarFarAway;
        [SerializeField] private CanvasGroup aUserHadToUse;
        
        // main panel stuff
        [SerializeField] private Slider healthSlider;

        // floats
        [SerializeField] private float chestDuration = 5;

        // private stuff :)
        private List<CanvasGroup> _panels;

        private GameObject _player;
        private PlayerManager _playerManager;
        private PlayerLives _playerLives;
        private int _currentPlayerHarts;
        private InteractiveChest _currentInteractiveChest;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _controls = new PlayerInput();
            _controls.Player.ResetPreferences.performed += ctx => ResetPlayerPrefs();
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Start()
        {
            _player = GameObject.Find("Player");
            _playerLives = _player.GetComponent<PlayerLives>();
            _playerManager = _player.GetComponent<PlayerManager>();
            
            _panels = new List<CanvasGroup> {mainPanel, fadePanel, chestPanel, pausePanel};
            if (PlayerPrefs.GetInt("init") == 1)
            {
                foreach (var panel in _panels)
                {
                    if (panel.name == mainPanel.name) continue;
                    panel.DOFade(0, .5f);
                    StartCoroutine(WaitSetActive(panel.gameObject, .5f, false));
                }
            }
            else
            {
                FirstFadeIn();
            }
        }

        private void Update()
        {
            healthSlider.value = (float) _playerManager.vitals.currentHealth / _playerManager.vitals.maxHealth;
            var curLives = _playerLives.currentLives;
            
            if (curLives != _currentPlayerHarts)
            {
                foreach (Transform t in harts.transform)
                {
                    Destroy(t.gameObject);
                }

                CreateHearts(curLives);

                _currentPlayerHarts = curLives;
            }
        }

        private void CreateHearts(int hearts)
        {
            for (var i = 0; i < hearts; i++)
            {
                var position = hartPlaceHolder.transform.position;
                // ReSharper disable once UnusedVariable
                var instantiate = Instantiate(
                    hart,
                    new Vector3(position.x + 20 * i,
                        position.y),
                    Quaternion.identity,
                    harts.transform
                );
            }
        }
        
        // pause panel
        private void PauseGame()
        {
            
        }

        // make an intro the first time the player enters the play scene
        private void FirstFadeIn()
        {
            StartCoroutine(WaitForFirstFade());
            PlayerPrefs.SetInt("init", 1);
        }

        // open the UI for the chest
        public void ChestOpen()
        {
            foreach (var panel in _panels)
            {
                if (panel.name != chestPanel.name)
                {
                    panel.DOFade(0, .5f);
                    StartCoroutine(WaitSetActive(panel.gameObject, .5f, false));
                }
            }

            StartCoroutine(WaitForChestFede());
        }

        // delete all saved data
        private static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        // chest button function
        public void ChestButtonPressed(string upgrade)
        {
            if (upgrade == "ADD HEALTH")
            {
                _playerManager.AddLife(1);
            }
            else if (upgrade == "ADD JUMP")
            {
                _playerManager.AddJump();
            }
            else if (upgrade == "ADD SPEED")
            {
                _playerManager.AddSpeed();
            }
            else if (upgrade == "ADD ATTACK")
            {
                _playerManager.AddAttack();
            }
            DOTween.CompleteAll();
            chestPanel.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _playerManager.EnableLookAndMovement(true);
            mainPanel.gameObject.SetActive(true);
            mainPanel.DOFade(1, 0.5f);
            StartCoroutine(_currentInteractiveChest.CloseChest());
        }

        private static IEnumerator WaitSetActive(GameObject go, float time, bool activate)
        {
            yield return new WaitForSeconds(time);
            go.SetActive(activate);
        }

        private IEnumerator WaitForFirstFade()
        {
            onceAponATime.DOFade(1, 2);
            yield return new WaitForSeconds(1);
            onceAponATime.DOFade(0, 2);
            inAGalaxyFarFarAway.DOFade(1, 2);
            yield return new WaitForSeconds(1);
            inAGalaxyFarFarAway.DOFade(0, 2);
            yield return new WaitForSeconds(3);
            aUserHadToUse.DOFade(1, 1);
            yield return new WaitForSeconds(4);
            aUserHadToUse.DOFade(0, 1);
            yield return new WaitForSeconds(1);
            fadePanel.DOFade(0, 1);
            mainPanel.DOFade(1, 1);
            yield return new WaitForSeconds(1);
        }

        private IEnumerator WaitForChestFede()
        {
            _currentInteractiveChest = FindObjectOfType<InteractiveChest>();
            var typeOfChestAddons = _currentInteractiveChest.insideChest;
            chestPanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerManager.EnableLookAndMovement(false);
            chestPanel.DOFade(1, .5f);
            yield return new WaitForSeconds(.5f);
            var i = 0;
            foreach (var chestAddon in typeOfChestAddons)
            {
                chestPanel.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
                chestPanel.transform.GetChild(i).GetComponent<CanvasGroup>().DOFade(0, chestDuration).SetEase(Ease.InExpo);
                chestPanel.transform.GetChild(i).GetComponent<RectTransform>().DOShakeRotation(chestDuration, 20);
                chestPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = chestAddon;
                i += 1;
            }

            chestPanel.DOFade(0, chestDuration).SetEase(Ease.InExpo);
            yield return new WaitForSeconds(chestDuration);
            if (chestPanel.gameObject.activeSelf)
            {
                chestPanel.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _playerManager.EnableLookAndMovement(true);
                mainPanel.gameObject.SetActive(true);
                mainPanel.DOFade(1, 0.5f);
                StartCoroutine(_currentInteractiveChest.CloseChest());
            }
        }
    }
}