using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;

    public void Setup(string title, string message, ICommand accept, ICommand cancel, Action<PopupView> onClosed)
    {
        titleText.text = title;
        messageText.text = message;

        acceptButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        acceptButton.onClick.AddListener(() => Press(accept, onClosed));
        cancelButton.onClick.AddListener(() => Press(cancel, onClosed));
    }

    private void Press(ICommand command, Action<PopupView> onClosed)
    {
        onClosed?.Invoke(this);   // primero se cierra y se reanuda el juego
        command?.Execute();       // después se ejecuta la acción
    }
}