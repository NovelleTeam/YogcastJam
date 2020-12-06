using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
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

        private void TaskOnClick()
        {
            _uiManager.ChestButtonPressed(gameObject.GetComponent<TextMeshProUGUI>().text);
        }
    }
}