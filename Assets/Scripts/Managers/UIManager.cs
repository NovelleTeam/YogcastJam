using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Reflection;
using Managers;

public class UIManager : MonoBehaviour
{
    // panels
    [SerializeField] private CanvasGroup mainPanel;
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private CanvasGroup chestPanel;
    [SerializeField] private CanvasGroup pausePanel;
    
    // texts
    [SerializeField] private CanvasGroup OnceAponATime;
    [SerializeField] private CanvasGroup inAGalaxyFarFarAway;
    [SerializeField] private CanvasGroup AUserHadToUse;
    
    // floats
    [SerializeField] private float chestDuration = 5;
    
    // private stuff :)
    private List<CanvasGroup> _panels;
    private PlayerManager _playerManager;

    // Start is called before the first frame update
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _panels = new List<CanvasGroup>();
        _panels.Add(mainPanel);
        _panels.Add(fadePanel);
        _panels.Add(chestPanel);
        _panels.Add(pausePanel);
        if (PlayerPrefs.GetInt("init") == 1)
        {
            foreach (CanvasGroup panel in _panels)
            {
                if (panel.name != mainPanel.name)
                {
                    panel.DOFade(0, .5f);
                    StartCoroutine(waitSetActive(panel.gameObject, .5f, false));
                }
            }
        }
        else
        {
            FirstFadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerPrefs();
        }
    }

    // make an intro the first time the player enters the play scene
    private void FirstFadeIn()
    {
        StartCoroutine(waitForFirstFade());
        PlayerPrefs.SetInt("init", 1);
    }

    // open the UI for the chest
    public void ChestOpen()
    {
        foreach (CanvasGroup panel in _panels)
        {
            if (panel.name != chestPanel.name)
            {
                panel.DOFade(0, .5f);
                StartCoroutine(waitSetActive(panel.gameObject, .5f, false));
            }
        }
        
        StartCoroutine(waitForChestFede());
    }
    
    // delete all saved data
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    
    // chest button function
    public void ChestButtonPressed(String name)
    {
        if (name == "ADD HEALTH")
        {
            _playerManager.AddHealth();
        }
        else if (name == "ADD JUMP")
        {
            _playerManager.AddJump();
        }
        else if (name == "ADD SPEED")
        {
            _playerManager.AddSpeed();
        }
        else if (name == "ADD ATACK")
        {
            //_playerManager.AddAtack();
        }
    }

    IEnumerator waitSetActive(GameObject go, float time, bool activate)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(activate);
    }

    IEnumerator waitForFirstFade()
    {
        OnceAponATime.DOFade(1, 2);
        yield return new WaitForSeconds(1);
        OnceAponATime.DOFade(0, 2);
        inAGalaxyFarFarAway.DOFade(1, 2);
        yield return new WaitForSeconds(1);
        inAGalaxyFarFarAway.DOFade(0, 2);
        yield return new WaitForSeconds(3);
        AUserHadToUse.DOFade(1, 1);
        yield return new WaitForSeconds(4);
        AUserHadToUse.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        fadePanel.DOFade(0, 1);
        mainPanel.DOFade(1, 1);
        yield return new WaitForSeconds(1);
    }

    IEnumerator waitForChestFede()
    {
        string[] TypeOfChestAddons = chestPanel.GetComponent<ChestManager>().insideChest;
        chestPanel.DOFade(1, .5f);
        yield return new WaitForSeconds(.5f);
        int i = 0;
        foreach (string chestAddon in TypeOfChestAddons)
        {
            chestPanel.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
            chestPanel.transform.GetChild(i).GetComponent<CanvasGroup>().DOFade(0, chestDuration);
            chestPanel.transform.GetChild(i).GetComponent<RectTransform>().DOShakeRotation(chestDuration, 20);
            chestPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = chestAddon;
            i += 1;
        }
        chestPanel.DOFade(0, chestDuration);
        yield return new WaitForSeconds(chestDuration);
    }    
}








