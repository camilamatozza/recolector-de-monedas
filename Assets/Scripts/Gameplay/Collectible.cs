using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private int value = 10;
    [SerializeField] private ParticleSystem pickupFx;

    public static event System.Action<int> Collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        Collected?.Invoke(value);

        if (pickupFx != null)
        {
            Instantiate(pickupFx, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (TryGetComponent(out CircleCollider2D circle))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, circle.radius * transform.lossyScale.x);
        }
    }
}