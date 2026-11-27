using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PopupEventSO popupEvent;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float detectionRange = 4f;

    private Vector2 home;
    private bool wasChasing;

    private void Awake()
    {
        home = transform.position;
    }

    private void Update()
    {
        if (player == null || !player.enabled) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool chasing = distance <= detectionRange;

        if (chasing && !wasChasing)
        {
            Debug.LogWarning("[Enemy] Detectó al jugador a " + distance.ToString("F1") + " unidades", this);
        }
        wasChasing = chasing;

        Vector2 destination = chasing ? (Vector2)player.transform.position : home;
        float currentSpeed = chasing ? speed : speed * 0.5f;
        transform.position = Vector2.MoveTowards(transform.position, destination, currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerController p) || !p.enabled) return;
        popupEvent.Raise("Game Over", "¿Reintentar?", new RetryCommand(), new BackToMenuCommand());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}