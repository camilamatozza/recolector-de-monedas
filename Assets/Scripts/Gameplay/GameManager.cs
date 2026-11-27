using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, Menu }
    public GameState CurrentState { get; private set; } = GameState.Playing;

    private const string BestScoreKey = "bestScore";

    [SerializeField] private UIManager ui;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform coinsRoot;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private PopupEventSO popupEvent;

    private int score;
    private int collected;
    private int totalCoins;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private static void Log(string msg) => Debug.Log("[GameManager] " + msg);

    private void Awake()
    {
#if UNITY_ASSERTIONS
        Debug.Assert(ui != null && player != null && coinsRoot != null && popupEvent != null,
            "GameManager: faltan referencias en el Inspector", this);
        Debug.Assert(coinsRoot == null || coinsRoot.childCount > 0,
            "GameManager: no hay monedas bajo Coins", this);
#endif
    }

    private void OnEnable()
    {
        Collectible.Collected += OnCollected;
        popupEvent.OnShowPopup += OnPopupShown;
        popupEvent.OnPopupClosed += ResumeGame;
    }

    private void OnDisable()
    {
        Collectible.Collected -= OnCollected;
        popupEvent.OnShowPopup -= OnPopupShown;
        popupEvent.OnPopupClosed -= ResumeGame;
    }

    private void Start()
    {
        totalCoins = coinsRoot != null ? coinsRoot.childCount : 0;
        Time.timeScale = 1f;
        Refresh();
    }

    private void OnPopupShown(string title, string message, ICommand accept, ICommand cancel)
    {
        PauseGame();
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        Log("Estado: Paused");
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        Log("Estado: Playing");
    }

    private void OnCollected(int value)
    {
        score += value;
        collected++;
        Log("Moneda +" + value + " | " + collected + "/" + totalCoins);
        Refresh();
        if (collected >= totalCoins) Win();
    }

    private void Refresh()
    {
        if (scoreText != null) scoreText.text = "Puntos: " + score;
        if (coinsText != null) coinsText.text = "Monedas: " + collected + "/" + totalCoins;
    }

    private void Win()
    {
        if (player != null) player.enabled = false;

        int best = PlayerPrefs.GetInt(BestScoreKey, 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt(BestScoreKey, best);
            PlayerPrefs.Save();
        }

        if (finalScoreText != null) finalScoreText.text = "Puntaje final: " + score + "\nMejor puntaje: " + best;
        if (ui != null) ui.ShowPanel("Win");
    }
}