using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private Vector2 halfBounds = new Vector2(7.5f, 4f);

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = ReadInput();
    }

    private void FixedUpdate()
    {
        Vector2 target = rb.position + input * speed * Time.fixedDeltaTime;
        target.x = Mathf.Clamp(target.x, -halfBounds.x, halfBounds.x);
        target.y = Mathf.Clamp(target.y, -halfBounds.y, halfBounds.y);
        rb.MovePosition(target);
    }

    private Vector2 ReadInput()
    {
        Vector2 v = Vector2.zero;
#if ENABLE_INPUT_SYSTEM
        Keyboard k = Keyboard.current;
        if (k == null) return v;
        if (k.aKey.isPressed || k.leftArrowKey.isPressed) v.x -= 1f;
        if (k.dKey.isPressed || k.rightArrowKey.isPressed) v.x += 1f;
        if (k.sKey.isPressed || k.downArrowKey.isPressed) v.y -= 1f;
        if (k.wKey.isPressed || k.upArrowKey.isPressed) v.y += 1f;
#else
        v.x = Input.GetAxisRaw("Horizontal");
        v.y = Input.GetAxisRaw("Vertical");
#endif
        return v.normalized;
    }
}