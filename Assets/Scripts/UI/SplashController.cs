using System.Collections;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float fadeIn = 0.8f;
    [SerializeField] private float hold = 1.2f;
    [SerializeField] private float fadeOut = 0.6f;
    [SerializeField] private string nextScene = "MainMenu";

    private IEnumerator Start()
    {
        group.alpha = 0f;
        yield return Fade(0f, 1f, fadeIn);
        yield return new WaitForSeconds(hold);
        yield return Fade(1f, 0f, fadeOut);
        SceneLoader.Load(nextScene);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        group.alpha = to;
    }
}