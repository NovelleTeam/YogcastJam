using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Ping = UnityEngine.Ping;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioManager))]
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainPanel;
    [SerializeField] private CanvasGroup fadeOutPanel;
    [SerializeField] private CanvasGroup cover;
    [SerializeField] private CanvasGroup fadeOutPanel2;
    //[SerializeField] private CanvasGroup settingsPanel;
    
    [SerializeField] private TextMeshProUGUI changingText;
    [SerializeField] private TextMeshProUGUI surpriseText;
    
    //[SerializeField] private Transform settingsButton;
    
    private String[] startTalk = new []
    {
        "HI", "USER", "YOU SEE THE THING IS", "THERE IS NO SURPRISE", "I UNDERSTAND", "YOU WARE EXPECTING A SURPRISE", "IF YOU DONT BELIEVE ME", "HERE IS FROM SOMEONE ELSE", "THERE IS NO SURPRISE!!!", "THERE IS NO SURPRISE", "THERE IS REALLY NO A SURPRISE"
    };
    private String[] surpriseNotRuinedTalk = new []
    {
        "USER", "WHAT ARE YOU DOING", "USER"
    };
    private String[] youDiedTalk = new []
    {
        "I'M SORY BUT YOU HAVE DIED", "I'M SORY TO TELL YOU THAT", "BUT YOU HAVE BEEN MURDERED BY SURPRISE"
    };

    private String[] _currentTalk;
    private String _currentStage;
    
    private int _numOfClicks;
    private bool _canGo;
    private bool _whatUser;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GetComponent<AudioManager>();
        fadeOutPanel.alpha = 0;
        fadeOutPanel.gameObject.SetActive(false);
        fadeOutPanel2.gameObject.SetActive(false);
        
    }

    
    private void Start()
    {
        _numOfClicks = 0;
        _canGo = true;
        StartCoroutine(waitForTalk(startTalk));
    }

    
    private void Update()
    {
        
    }

    private void Talk(String[] talk)
    {
        StartCoroutine(waitForNextTalk(talk));
    }

    private bool canMoveOn()
    {
        return _canGo;
    }
    
    public void OpenSurprise()
    {
        if (_numOfClicks <= 12)
        {
            changingText.GetComponent<RectTransform>().position = changingText.GetComponent<RectTransform>().position + new Vector3(-5 * _numOfClicks, 0, 0);
            surpriseText.GetComponent<RectTransform>().position = surpriseText.GetComponent<RectTransform>().position + new Vector3(-5 * _numOfClicks, 0, 0);
            _numOfClicks += 1;
            if (!_whatUser)
            {
                Talk(surpriseNotRuinedTalk);
                _whatUser = true;
            }
        }
        else if (_numOfClicks == 13)
        {
            cover.GetComponent<CanvasGroup>().alpha = 0;
            changingText.GetComponent<RectTransform>().position = new Vector3(mainPanel.GetComponent<RectTransform>().rect.width/2, changingText.GetComponent<RectTransform>().position.y);
            surpriseText.GetComponent<RectTransform>().position = new Vector3(mainPanel.GetComponent<RectTransform>().rect.width/2, surpriseText.GetComponent<RectTransform>().position.y);
            surpriseText.GetComponent<RectTransform>().DOShakeRotation(10, 100f);
            surpriseText.GetComponent<RectTransform>().DOShakeScale(10, 3f);
            surpriseText.GetComponent<RectTransform>().DOShakePosition(10, 2f);
            _numOfClicks += 1;
            fadeOutPanel.gameObject.SetActive(true);
            fadeOutPanel.DOFade(1, 2);
            _audioManager.Play("WHITE");
            StartCoroutine(waitForPlayScene());
        }
    }

    public void PlaySceneGo()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator waitForTalk(String[] talkList)
    {
        _currentTalk = talkList;
        foreach (String line in talkList)
        {
            print(line);
            if (_currentTalk == talkList && _canGo)
            {
                _canGo = false;
                _audioManager.Play(line);
                changingText.text = line;
                yield return new WaitForSeconds(_audioManager.GetLength(line));
                changingText.text = "";
                _canGo = true;
                yield return new WaitForSeconds(.1f);
            }
            else if (_canGo)
            {
                
            }
            else
            {
                continue;
            }
        }
        yield break;
    }

    IEnumerator waitForNextTalk(String[] talkList)
    {
        yield return new WaitUntil(canMoveOn);
        StartCoroutine(waitForTalk(talkList));
    }

    IEnumerator waitForPlayScene()
    {
        yield return new WaitForSeconds(5);
        Talk(youDiedTalk);
        fadeOutPanel2.gameObject.SetActive(true);
        fadeOutPanel2.DOFade(1, 1);
        yield return new WaitForSeconds(8);
    }
}


















