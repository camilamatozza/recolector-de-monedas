using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/PopupEvent")]
public class PopupEventSO : ScriptableObject
{
    public Action<string, string, ICommand, ICommand> OnShowPopup;
    public Action OnPopupClosed;

    // Cuántos suscriptores tiene el evento: sirve para detectar listeners duplicados
    public int ListenerCount => OnShowPopup?.GetInvocationList().Length ?? 0;

    public void Raise(string title, string message, ICommand acceptCommand, ICommand cancelCommand = null)
    {
        OnShowPopup?.Invoke(title, message, acceptCommand, cancelCommand);
    }

    public void NotifyClosed()
    {
        OnPopupClosed?.Invoke();
    }
}