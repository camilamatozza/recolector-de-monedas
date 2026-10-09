using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class ScreenPanel
    {
        public string id;
        public GameObject root;
    }

    [SerializeField] private List<ScreenPanel> panels = new List<ScreenPanel>();
    [SerializeField] private string initialPanel = "Main";
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0.8f);
        AudioListener.volume = volume;
        if (volumeSlider != null)
            volumeSlider.SetValueWithoutNotify(volume);
    }

    private void Start()
    {
        ShowPanel(initialPanel);
    }

    public void ShowPanel(string id)
    {
        foreach (ScreenPanel p in panels)
            p.root.SetActive(p.id == id);
    }

    public void Play()        { SceneLoader.Load(SceneLoader.Gameplay); }
    public void BackToMenu()  { SceneLoader.Load(SceneLoader.MainMenu); }
    public void Restart()     { SceneLoader.Load(SceneLoader.Gameplay); }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}