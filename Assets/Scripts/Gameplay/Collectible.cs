using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private int value = 10;

    public static event System.Action<int> Collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        Collected?.Invoke(value);
        Destroy(gameObject);
    }
}