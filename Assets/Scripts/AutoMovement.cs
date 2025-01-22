using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed = 10f;
    public bool movingUp;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movingUp) rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, speed);
        else rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, -speed);
    }
}
