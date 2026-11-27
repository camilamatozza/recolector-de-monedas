using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupEventSO popupEvent;
    [SerializeField] private PopupView popupPrefab;
    [SerializeField] private Transform popupParent;
    [SerializeField] private int initialPoolSize = 1;

    private readonly List<PopupView> pool = new List<PopupView>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++) CreateNew();
    }

    private void OnEnable()
    {
        popupEvent.OnShowPopup += Show;
        Debug.Log("[PopupManager] suscripto. Listeners: " + popupEvent.ListenerCount, this);
    }

    private void OnDisable()
    {
        popupEvent.OnShowPopup -= Show;
    }

    private PopupView CreateNew()
    {
        PopupView view = Instantiate(popupPrefab, popupParent);
        view.gameObject.SetActive(false);
        pool.Add(view);
        return view;
    }

    private PopupView GetFromPool()
    {
        foreach (PopupView view in pool)
            if (!view.gameObject.activeSelf) return view;
        return CreateNew();   // si todos están en uso, el pool crece
    }

    private void Show(string title, string message, ICommand accept, ICommand cancel)
    {
        PopupView view = GetFromPool();
        view.transform.SetAsLastSibling();   // que quede por encima del HUD
        view.gameObject.SetActive(true);
        view.Setup(title, message, accept, cancel, ReturnToPool);
    }

    private void ReturnToPool(PopupView view)
    {
        view.gameObject.SetActive(false);
        popupEvent.NotifyClosed();
    }
}
