using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChestButton : MonoBehaviour
{
    private Button _button;
    private UIManager _uiManager;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _uiManager = FindObjectOfType<UIManager>();
        _button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        _uiManager.ChestButtonPressed(gameObject.GetComponent<TextMeshProUGUI>().text);
    }
}
