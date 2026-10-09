using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager ui;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform coinsRoot;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text finalScoreText;

    private int score;
    private int collected;
    private int totalCoins;

    private void OnEnable()
    {
        Collectible.Collected += OnCollected;
    }

    private void OnDisable()
    {
        Collectible.Collected -= OnCollected;
    }

    private void Start()
    {
        totalCoins = coinsRoot != null ? coinsRoot.childCount : 0;
        Refresh();
    }

    private void OnCollected(int value)
    {
        score += value;
        collected++;
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
        if (finalScoreText != null) finalScoreText.text = "Puntaje final: " + score;
        if (ui != null) ui.ShowPanel("Win");
    }
}