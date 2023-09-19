using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private const string WelcomeText = "Приветствую, путник! Желаешь купить товары для путешествия?";

    [SerializeField] private TextMeshProUGUI _messageBox;
    [SerializeField] private float _typeTime = 0.1f;
    [SerializeField] private Canvas _dialogWindow;

    private WaitForSeconds _textCooldown;
    private Coroutine _textMessage;

    private void OnEnable()
    {
        PlayerDetector.PlayerCame += EnableMessageBox;
        PlayerDetector.PlayerGone += DisableMessageBox;
    }

    private void OnDisable()
    {
        PlayerDetector.PlayerCame -= EnableMessageBox;
        PlayerDetector.PlayerGone -= DisableMessageBox;
    }

    private void Awake()
    {
        _messageBox.text = "";
        _textCooldown = new WaitForSeconds(_typeTime);
    }

    private void EnableMessageBox()
    {
        _dialogWindow.gameObject.SetActive(true);
        _textMessage = StartCoroutine(ShowMessageText(WelcomeText));
    }

    private void DisableMessageBox()
    {
        StopCoroutine(_textMessage);
        _dialogWindow.gameObject.SetActive(false);
        _messageBox.text = "";

    }

    private IEnumerator ShowMessageText(string message)
    {
        char[] messageArray = message.ToCharArray();

        for (int i = 0; i < messageArray.Length; i++)
        {
            _messageBox.text += messageArray[i];
            yield return _textCooldown;
        }
    }
}
