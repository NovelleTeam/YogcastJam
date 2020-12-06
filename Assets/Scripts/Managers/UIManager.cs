using System.Collections;
using System.Collections.Generic;
using Controllers.Interactive;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        // panels
        [SerializeField] private CanvasGroup mainPanel;
        [SerializeField] private CanvasGroup fadePanel;
        [SerializeField] private CanvasGroup chestPanel;
        [SerializeField] private CanvasGroup pausePanel;

        // texts
        [SerializeField] private CanvasGroup onceAponATime;
        [SerializeField] private CanvasGroup inAGalaxyFarFarAway;
        [SerializeField] private CanvasGroup aUserHadToUse;

        // floats
        [SerializeField] private float chestDuration = 5;

        // private stuff :)
        private List<CanvasGroup> _panels;
        private PlayerManager _playerManager;

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPlayerPrefs();
            }
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
                if (panel.name != chestPanel.name) continue;
                panel.DOFade(0, .5f);
                StartCoroutine(WaitSetActive(panel.gameObject, .5f, false));
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
                //_playerManager.AddAtack();
            }
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
            var typeOfChestAddons = FindObjectOfType<InteractiveChest>().insideChest;
            chestPanel.DOFade(1, .5f);
            yield return new WaitForSeconds(.5f);
            var i = 0;
            foreach (var chestAddon in typeOfChestAddons)
            {
                var canvasGroup = chestPanel.transform.GetChild(i).GetComponent<CanvasGroup>();
                var rectTransform = chestPanel.transform.GetChild(i).GetComponent<RectTransform>();
                var textMeshProUGUI = chestPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                canvasGroup.alpha = 1;
                canvasGroup.DOFade(0, chestDuration);
                rectTransform.DOShakeRotation(chestDuration, 20);
                textMeshProUGUI.text = chestAddon;
                i += 1;
            }

            chestPanel.DOFade(0, chestDuration);
            yield return new WaitForSeconds(chestDuration);
        }
    }
}